using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using OL490_SDK_Dll;

namespace UniqueHues
{
    public partial class Calibrate : Form
    {
        public static double current_wavelength;

        public double BANDWIDTH;
        public double INTENSITY;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        //private static bool s_ShutterOpen;

        public Calibrate(double bandwidth = 10, double intensity = 100)
        {
            InitializeComponent();

            // set parameters
            BANDWIDTH = bandwidth;
            INTENSITY = intensity;
            
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            current_wavelength = 420;
            changeLabel2text(current_wavelength.ToString() + " nm");
            initializeGooch();
            if (s_OL490.GetOL490ShutterState() == 0)
            {
                openShutter();
            }

            //closeShutter();
            updateGooch();
            openShutter();
        }

        private static void clearGooch()
        {
            s_OL490.ResetLiveSpectrum();

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

        private static void closeShutter()
        {
            s_OL490.CloseShutter();
        }

        private void updateGooch()
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(current_wavelength, BANDWIDTH, INTENSITY);
            //print("Clearing Spectrum");

        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'f')
            {
                if (current_wavelength < 710)
                {
                    
                    current_wavelength += 5;
                    updateGooch();
                    //double wvlen = s_OL490.GetLiveSpectrumPeakWavelength(-1);
                    string wvlen = current_wavelength.ToString();
                    changeLabel2text(wvlen.ToString() + " nm");
                    
                }
                else
                {
                    clearGooch();
                    closeShutter();
                    changeLabel2text("end of spectrum");
                }
            }
            if (e.KeyChar == 'j')
            {
                if (current_wavelength > 380)
                {

                    current_wavelength -= 5;
                    updateGooch();
                    //double wvlen = s_OL490.GetLiveSpectrumPeakWavelength(-1);
                    string wvlen = current_wavelength.ToString();
                    changeLabel2text(wvlen.ToString() + " nm");

                }
                else
                {
                    clearGooch();
                    closeShutter();
                    changeLabel2text("end of spectrum");
                }
            if (e.KeyChar == 'q')
            {
                clearGooch();
                closeShutter();
                changeLabel2text("end of program");
            }
            }

        }

        public void changeLabel2text(string labelText)
        {
            this.label2.Text = labelText;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            // Close Shutter and Clear Gooch on close event
            clearGooch();
            closeShutter();
        }

    }

}
