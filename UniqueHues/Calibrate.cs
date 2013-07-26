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

        private const double bandwidth = 10;
        private const double intensity = 100;

        private static OL490SdkLibrary s_OL490 = new OL490SdkLibrary();
        //private static bool s_ShutterOpen;

        public Calibrate()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            current_wavelength = 400;
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
            if (s_OL490.GetNumberOfLiveSpectrumPeaks() > 0) { s_OL490.ResetLiveSpectrum(); }

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

        private static void updateGooch()
        {
            clearGooch();
            eErrorCodes errCode = s_OL490.SendLivePeak(current_wavelength, bandwidth, intensity);
            //print("Clearing Spectrum");

        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'f')
            {
                if (current_wavelength < 710)
                {
                    
                    current_wavelength += 10;
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

        }

        public void changeLabel2text(string labelText)
        {
            this.label2.Text = labelText;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
