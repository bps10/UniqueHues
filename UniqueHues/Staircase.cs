using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using OL490_SDK_Dll;
using UniqueHues;


namespace StaircaseProgram
{
    public class Staircase
    {
        public static double wavelength_upper;
        public static double wavelength_lower;
        public static double current_wavelength;

        private const double bandwidth = 10;
        private const double intensity = 100;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        private static bool s_ShutterOpen;

        private static Random random = new Random();
        private static int randomNumber;
        private static bool lowWavelength;

        private static string high_data_record;
        private static string low_data_record;

        public static string subject_name;
        public static string active_wavelength;
        public static int high_reversals;
        public static int low_reversals;
        public static int max_reversals = 10;

        public bool end_staircase;

        public Staircase()
        {
            subject_name = "name";
            active_wavelength = "none";
            high_reversals = 0;
            low_reversals = 0;
            high_data_record = "";
            low_data_record = "";
            s_ShutterOpen = false;
        }

        public void RunStaircase(string uniqueHue, string sub_name)
        {
            // log subject name
            subject_name = sub_name;
            // initialize the gooch;
            initializeGooch();

            // make sure end_staircase is false;
            end_staircase = false;

            selectStartingWavelength(uniqueHue);
            
            updateGooch();

        }

        public void continueStaircase(int button_choice)
        {
            // record hue decision:
            if (active_wavelength == "high")
            {
                string data = current_wavelength.ToString() + "\t" + button_choice.ToString() + "\r\n";
                high_data_record += data;
            }
            if (active_wavelength == "low")
            {
                string data = current_wavelength.ToString() + "\t" + button_choice.ToString() + "\r\n";
                low_data_record += data;
            }
          
            updateWavelength(button_choice);
            updateGooch();

        }

        private static void initializeGooch()
        {
            eErrorCodes connect = s_OL490.ConnectToOL490(0);

            if (connect == eErrorCodes.Success)
            {
                s_OL490.EnableLinearLightReduction(-1);

                s_OL490.SetGrayScaleValue(0);

                openShutter();

                eErrorCodes calibration = s_OL490.LoadAndUseStoredCalibration(0);
            }
        }

        private static void openShutter()
        {
            if (!s_ShutterOpen) { s_OL490.OpenShutter(); s_ShutterOpen = true; }
        }

        private static void updateGooch()
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(current_wavelength, bandwidth, intensity);
            print("Clearing Spectrum");
            processErrorCode("ClrSpectrum()", errCode);

        }

        private void selectStartingWavelength(string uniqueHue)
        {
            if (uniqueHue == "yellow") { wavelength_upper = 630; wavelength_lower = 550; }
            if (uniqueHue == "blue") { wavelength_upper = 500; wavelength_lower = 400; }
            if (uniqueHue == "green") { wavelength_upper = 580; wavelength_lower = 480; }

            choose_high_or_low();
            choose_high_or_low();
            if (active_wavelength == "high") { current_wavelength = wavelength_upper; }
            if (active_wavelength == "low") { current_wavelength = wavelength_lower; }

        }

        private void choose_high_or_low()
        {
            // return a 0 or 1
            randomNumber = random.Next(2);

            // set decision to update low wavelength to true or false for this trial
            lowWavelength = randomNumber == 0 ? true : false;
        }

        private void updateWavelength(int button_choice)
        {
            int step_size = 1;
            int step = 0;

            if (active_wavelength == "high")
            {
                if (high_reversals < 1) { step_size = 8; }
                else if (high_reversals >= 1 && high_reversals < 4) { step_size = 3; }
                else { step_size = 1; }

                if (button_choice == 2)
                {
                    //this is NOT a reversal;

                    step = step_size;
                    wavelength_upper = wavelength_upper - step;
                }
                if (button_choice == 1)
                {
                    //this is a reversal;
                    step = step_size;
                    wavelength_upper = wavelength_upper + step;
                    high_reversals += 1;
                }
            }
            if (active_wavelength == "low")
            {
                if (low_reversals < 1) { step_size = 8; }
                else if (low_reversals >= 1 && low_reversals < 4) { step_size = 3; }
                else { step_size = 1; }

                if (button_choice == 1)
                {
                    //this is NOT a reversal;
                    step = step_size;
                    wavelength_lower = wavelength_lower + step;
                }
                if (button_choice == 2)
                {
                    //this is a reversal;
                    step = step_size;
                    wavelength_lower = wavelength_lower - step;
                    low_reversals += 1;
                }
            }

            setCurrentWavelength();
        }

        public void setCurrentWavelength()
        {
            if (high_reversals < max_reversals && low_reversals < max_reversals)
            {
                // decide whether to show the high or low wavelength
                choose_high_or_low();

                // update current wavelength
                if (lowWavelength == true)
                {
                    current_wavelength = wavelength_lower;
                    active_wavelength = "low";
                }
                else
                {
                    current_wavelength = wavelength_upper;
                    active_wavelength = "high";
                }
            }
            if (high_reversals == max_reversals && low_reversals < max_reversals)
            {
                current_wavelength = wavelength_lower;
                active_wavelength = "low";
            }
            if (low_reversals == max_reversals && high_reversals < max_reversals)
            {
                current_wavelength = wavelength_upper;
                active_wavelength = "high";
            }
            if (low_reversals == max_reversals && high_reversals == max_reversals)
            {
                endStaircase();
                end_staircase = true;
            }
        }

        private static void clearGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() == 1) { s_OL490.ResetLiveSpectrum(); }

        }

        public static void endStaircase()
        {
            string name;
            if (File.Exists("./data/" + subject_name + ".txt"))
            {
                name = "./data/" + subject_name + "_1.txt"; 
            }
            else { name = "./data/" + subject_name + ".txt"; }

            TextWriter tw = new StreamWriter(name);
            tw.WriteLine(high_data_record + "\r\n\r\n" + low_data_record);
            tw.Close();

            s_OL490.CloseShutter();
            s_ShutterOpen = false;

        }

        private static void print(string message)
        {
            StringBuilder Message = new StringBuilder();
            Message.Append(": ");
            Message.Append(message);
            //stdout.print(Message.ToString());
        }

        private static void processErrorCode(string function, eErrorCodes errCode)
        {
            switch (errCode)
            {
                case eErrorCodes.Success: break;
                case eErrorCodes.NoAction: break;
                default:
                    {
                        StringBuilder Message = new StringBuilder(function);
                        Message.Append(": ");
                        Message.Append(errCode.ToString());
                        print(Message.ToString());
                        throw new OL490DLLException(Message.ToString());
                    }
            }
        }
        public class OL490DLLException : Exception
        {
            public OL490DLLException() { }
            public OL490DLLException(string message) : base(message) { }
            public OL490DLLException(string message, System.Exception inner) : base(message, inner) { }
            protected OL490DLLException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) { }
        }
    }
}
