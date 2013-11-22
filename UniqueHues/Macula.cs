using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Threading;
using OL490_SDK_Dll;
using System.Timers;
using UniqueHues;


namespace UniqueHues
{
    public class Macula
    {

        private static double BANDWIDTH;
        private static double INTENSITY;
        private static double FLICKER_SPEED = 13.0;
        private double TIME_STEP;
        private static System.Timers.Timer flickerTimer;

        public static double current_wavelength;

        private double short_light_intensity;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        private static bool s_ShutterOpen;

        private static string data_record;

        public static string subject_name;


        public Macula(float bandwidth=10, float intensity=100)
        {   
            // set parameters
            BANDWIDTH = bandwidth;
            INTENSITY = intensity;
            subject_name = "name";

            if (s_OL490.GetOL490ShutterState() == 1)
            { 
                s_ShutterOpen = true;
            }
            else if (s_OL490.GetOL490ShutterState() == 0)
            { 
                s_ShutterOpen = false;
            }
        }

        public void Set_Flicker_Speed(float speed)
        {
            // in hertz
            FLICKER_SPEED = speed;
        }

        public void RunMacula(string uniqueHue, string sub_name)
        {
            // log subject name
            subject_name = sub_name;

            // initialize the gooch;
            initializeGooch();
            clearGooch();

            _updateMacula();

        }

        public void _updateMacula()
        {

            TIME_STEP = 60 / FLICKER_SPEED / 2; // 1 sec / Hz / cycle
            // continue running until user inputs new value

            flickerTimer = new System.Timers.Timer(30000);

            // while (run) {
            // Create a timer with a specified interval.

            // Hook up the Elapsed event for the timer.
            flickerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            flickerTimer.Interval = TIME_STEP;
            flickerTimer.Enabled = true;


            // If the timer is declared in a long-running method, use 
            // KeepAlive to prevent garbage collection from occurring 
            // before the method ends. 
            GC.KeepAlive(flickerTimer);

        }

        // Specify what you want to happen when the Elapsed event is raised. 
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (current_wavelength == 456) {
                current_wavelength = 570;
            }
            else {
                current_wavelength = 456;
            }

            updateGooch(current_wavelength, BANDWIDTH, INTENSITY);
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

        private static void updateGooch(double wavelength, double bandwidth, double intensity)
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(wavelength, bandwidth, intensity);
            //print("Clearing Spectrum");
            processErrorCode("ClrSpectrum()", errCode);

        }
        private void record_data()
        {
            string data = short_light_intensity.ToString() + "\t" + FLICKER_SPEED.ToString() + "\t";
            data_record += data;
        }

        public static void clearGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() > 0) { s_OL490.ResetLiveSpectrum(); }

        }


        public void endFlicker()
        {
            clearGooch();
            closeShutter();

            record_data();

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
}
