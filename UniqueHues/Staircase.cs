﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Threading;
using OL490_SDK_Dll;
using UniqueHues;


namespace StaircaseProgram
{
    public class Staircase
    {
        public static double wavelength_upper;
        public static double wavelength_lower;
        public static double current_wavelength;

        private double BANDWIDTH;
        private double INTENSITY;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        private static bool s_ShutterOpen;

        private static Random random = new Random();
        private static int randomNumber;
        private static bool lowWavelength;

        private static string high_data_record;
        private static string low_data_record;

        public static string subject_name;
        public static string hue;
        public static string active_wavelength;
        public static int high_reversals;
        public static int low_reversals;
        public static int max_reversals = 10;
        public static int pause_time = 1000;

        public bool end_staircase;

        public Staircase(float bandwidth=10, float intensity=100)
        {   
            // set parameters
            BANDWIDTH = bandwidth;
            INTENSITY = intensity;
            subject_name = "name";
            hue = "none";
            active_wavelength = "none";
            high_reversals = 0;
            low_reversals = 0;
            high_data_record = "";
            low_data_record = "";
            if (s_OL490.GetOL490ShutterState() == 1)
            { 
                s_ShutterOpen = true;
            }
            else if (s_OL490.GetOL490ShutterState() == 0)
            { 
                s_ShutterOpen = false;
            }
        }

        public void RunStaircase(string uniqueHue, string sub_name)
        {
            // log subject name
            subject_name = sub_name;

            // initialize the gooch;
            initializeGooch();
            clearGooch();

            // make sure end_staircase is false;
            end_staircase = false;

            selectStartingWavelength(uniqueHue);
            
            updateGooch();

            openShutter();

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
            if (!end_staircase)
            {
                // close the shutter and pause 1.5sec
                closeShutter();
                Thread.Sleep(pause_time);
                // update gooch and then open shutter
                updateGooch();
                openShutter();
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

        public void closeShutter()
        {
            s_OL490.CloseShutter(); 
            s_ShutterOpen = false;
        }

        private void updateGooch()
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(current_wavelength, BANDWIDTH, INTENSITY);
            //print("Clearing Spectrum");
            processErrorCode("ClrSpectrum()", errCode);

        }

        private void selectStartingWavelength(string uniqueHue)
        {
            if (uniqueHue == "yellow") { wavelength_upper = 630; wavelength_lower = 550; }
            if (uniqueHue == "blue") { wavelength_upper = 500; wavelength_lower = 430; }
            if (uniqueHue == "green") { wavelength_upper = 580; wavelength_lower = 480; }

            setCurrentWavelength();
            hue = uniqueHue;

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
            // handle case when max reached for high reversals
            if (high_reversals == max_reversals && low_reversals < max_reversals)
            {
                current_wavelength = wavelength_lower;
                active_wavelength = "low";
            }
            // handle case when max reached for low reversals
            if (low_reversals == max_reversals && high_reversals < max_reversals)
            {
                current_wavelength = wavelength_upper;
                active_wavelength = "high";
            }
            // handle case when max reached for both low and high reversals 
            if (low_reversals == max_reversals && high_reversals == max_reversals)
            {
                endStaircase();
                end_staircase = true;
            }
        }

        public void clearGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() > 0) { s_OL490.ResetLiveSpectrum(); }
        }

        public void endStaircase()
        {
            clearGooch();
            closeShutter();

            // save results to file
            string name;
            string dir = "C:/Users/Jay/Desktop/hues/data/" + subject_name + "/";
            
            // create directory for subject if it doesn't already exist
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // get date
            DateTime date = DateTime.Today;

            string basename = (dir + subject_name + "_" + hue.Substring(0,1).ToUpper() + 
                "_" + date.ToString("Mddy")); // month day year
            int trial = 1;
            while (File.Exists(basename + "_" + trial.ToString() + ".txt"))
            {
                trial += 1;
            }
            name = basename + "_" + trial + ".txt";

            TextWriter tw = new StreamWriter(name);
            tw.WriteLine(high_data_record + "\r\n\r\n" + low_data_record);
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
