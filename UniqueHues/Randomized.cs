using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Threading;
using OL490_SDK_Dll;
using UniqueHues;


namespace StaircaseProgram
{
    public class Randomized
    {
        public static int wavelength_upper;
        public static int wavelength_lower;
        public static double current_wavelength;
        List<int> wavelengths = new List<int>();

        private const double bandwidth = 10;
        private const double intensity = 100;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        private static bool s_ShutterOpen;

        private static string data_record;

        public static string subject_name;
        public static string hue;
        public static int pause_time = 1000;

        public int trial;
        public int end_wavelength;
        
        public Randomized()
        {
            subject_name = "name";
            hue = "none";
            data_record = "";
            trial = 1;

            if (s_OL490.GetOL490ShutterState() == 1)
            {
                s_ShutterOpen = true;
            }
            else if (s_OL490.GetOL490ShutterState() == 0)
            {
                s_ShutterOpen = false;
            }
        }

        public void gen_wavelengths()
        {
            for (int wvlen=wavelength_lower; wvlen < wavelength_upper + 1; wvlen += 2)
            {
                for (int i = 1; i < 11; i++ )
                {
                    wavelengths.Add(wvlen);
                }
            }
            Shuffle(wavelengths);
        }

        public void RunForcedChoice(string uniqueHue, string sub_name)
        {
            // log subject name
            subject_name = sub_name;

            // initialize the gooch;
            initializeGooch();
            clearGooch();

            // get wavelength bounds based on hue
            selectStartingWavelength(uniqueHue);

            // generate list of randomized wavelengths
            gen_wavelengths();

            // set first wavelength
            current_wavelength = wavelengths[trial];

            // make sure end_staircase is false;
            end_wavelength = wavelengths.Count;

            updateGooch();

            openShutter();

        }

        public void continueRandomized(int button_choice)
        {
            record_data(button_choice);
            trial++;
            if (trial != end_wavelength)
            {
                // update current wavelength
                current_wavelength = wavelengths[trial];
                // close the shutter and pause 1.5sec
                closeShutter();
                Thread.Sleep(pause_time);
                // update gooch and then open shutter
                updateGooch();
                openShutter();
            }
            else
            {
                endRandomized();
            }

        }

        private static void initializeGooch()
        {
            eErrorCodes connect = s_OL490.ConnectToOL490(0);

            if (connect == eErrorCodes.Success)
            {
                s_OL490.EnableLinearLightReduction(-1);

                s_OL490.SetGrayScaleValue(0);

                //openShutter();

                eErrorCodes calibration = s_OL490.LoadAndUseStoredCalibration(0);
            }
        }

        private static void openShutter()
        {
            s_OL490.OpenShutter();
            s_ShutterOpen = true;
        }

        private static void closeShutter()
        {
            s_OL490.CloseShutter();
            s_ShutterOpen = false;
        }

        private static void updateGooch()
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(current_wavelength, bandwidth, intensity);
            //print("Clearing Spectrum");
            processErrorCode("ClrSpectrum()", errCode);

        }

        private void selectStartingWavelength(string uniqueHue)
        {
            if (uniqueHue == "yellow") { wavelength_upper = 590; wavelength_lower = 560; }
            if (uniqueHue == "blue") { wavelength_upper = 500; wavelength_lower = 450; }
            if (uniqueHue == "green") { wavelength_upper = 540; wavelength_lower = 500; }

            setCurrentWavelength();
            hue = uniqueHue;

        }


        public static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void record_data(int button_choice)
        {
            string data = current_wavelength.ToString() + "\t" + button_choice.ToString() + "\r\n";
            data_record += data;
        }

        public void setCurrentWavelength()
        {

        }

        private static void clearGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() == 1) { s_OL490.ResetLiveSpectrum(); }

        }

        public void endRandomized()
        {
            clearGooch();
            closeShutter();

            // save results to file
            string name;
            string dir = "C:/Users/Jay/Desktop/hues/data/rand/" + subject_name + "/";

            // create directory for subject if it doesn't already exist
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // get date
            DateTime date = DateTime.Today;

            string basename = (dir + subject_name + "_" + hue.Substring(0, 1).ToUpper() +
                "_" + date.ToString("Mddy")); // month day year
            int trial = 1;
            while (File.Exists(basename + "_" + trial.ToString() + ".txt"))
            {
                trial += 1;
            }
            name = basename + "_" + trial + ".txt";

            TextWriter tw = new StreamWriter(name);
            tw.WriteLine(data_record);
            tw.Close();

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
