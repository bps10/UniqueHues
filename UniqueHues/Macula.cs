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
        private static double FLICKER_SPEED;
        private double TIME_STEP;
        private static System.Timers.Timer flickerTimer;

        public static double current_wavelength = 456;

        private static double short_light_intensity;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();

        private static string data_record = "light\tintensity\tlight\tintensity\tspeed(HZ)\n";

        public static string subject_name;


        public Macula(string name, float bandwidth=10, float intensity=100, 
            double flicker_speed = 15.0)
        {   
            // set parameters
            BANDWIDTH = bandwidth;
            INTENSITY = intensity / 3.0;
            short_light_intensity = intensity / 3.0;
            FLICKER_SPEED = flicker_speed;
            subject_name = name;

        }

        public void Set_Short_Intensity(double intensity)
        {
            short_light_intensity = intensity / 3.0;
            end_flicker();
            _updateMacula();
        }

        public void Set_Flicker_Speed(float speed)
        {
            // in hertz
            FLICKER_SPEED = speed;
        }

        public void RunMacula()
        {
            // log subject name
            //subject_name = sub_name;

            // initialize the gooch;
            initializeGooch();
            clearGooch();
            openShutter();

            _updateMacula();

        }

        public void _updateMacula()
        {

            TIME_STEP = 1000 / FLICKER_SPEED / 2; // 1 sec / Hz / cycle
            // continue running until user inputs new value

            flickerTimer = new System.Timers.Timer(TIME_STEP);

            // while (run) {
            // Create a timer with a specified interval.

            // Hook up the Elapsed event for the timer.
            flickerTimer.Elapsed += OnTimedEvent;

            // Set the Interval to 2 seconds (2000 milliseconds).
            //flickerTimer.Interval = TIME_STEP;
            flickerTimer.Enabled = true;


            // If the timer is declared in a long-running method, use 
            // KeepAlive to prevent garbage collection from occurring 
            // before the method ends. 
            //GC.KeepAlive(flickerTimer);

        }

        public void end_flicker()
        {
            flickerTimer.Enabled = false;
        }

        // Specify what you want to happen when the Elapsed event is raised. 
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            double intensity;
            if (current_wavelength == 456) {
                current_wavelength = 570;
                intensity = INTENSITY;
            }
            else {
                current_wavelength = 456;
                intensity = short_light_intensity;
            }

            updateGooch(current_wavelength, BANDWIDTH, intensity);
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
        }

        public void closeShutter()
        {
            s_OL490.CloseShutter();
        }

        private static void updateGooch(double wavelength, double bandwidth, double intensity)
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(wavelength, bandwidth, intensity);
            //print("Clearing Spectrum");
            processErrorCode("ClrSpectrum()", errCode);

        }
        public void record_data()
        {
            //string data = "light\tintensity\tlight\tintensity\tspeed(HZ)\n";
            string data = "460\t" + short_light_intensity.ToString() + "\t";
            data += "570\t" + INTENSITY + "\t";
            data += FLICKER_SPEED.ToString() + "\t\n";
            data_record += data;
        }

        public static void clearGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() > 0) { s_OL490.ResetLiveSpectrum(); }
        }

        public void clrGooch()
        {
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() > 0) { s_OL490.ResetLiveSpectrum(); }
        }


        public void end_trial()
        {
            end_flicker();
            clearGooch();
            closeShutter();

            // save results to file
            string name;
            string dir = "C:/Users/Jay/Desktop/macula/" + subject_name + "/";

            // create directory for subject if it doesn't already exist
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // get date
            DateTime date = DateTime.Today;

            string basename = (dir + subject_name +
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

