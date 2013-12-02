using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniqueHues;

namespace UniqueHues
{
    public partial class MaculaForm : Form
    {
        private static UniqueHues.Macula thisTrial;
        private double INTENSITY = 85;
        private double STEP = 3;
        private double trial;
        private EccentricityForm ecc_form = new EccentricityForm();

        public MaculaForm(string name, double frequency)
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ForcedChoiceForm_FormClosing);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(kPress);

            // call the blue eccentricity form
            
            ecc_form.Location = new Point(-1450, 60);
            ecc_form.Show();

            thisTrial = new UniqueHues.Macula(name, flicker_speed: frequency, intensity: INTENSITY);
            trial = 0;
            change_trial_text();

        }
        public void run()
        {
            thisTrial.RunMacula();
        }
        void kPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                // low arrow key:
                low_arrow_select();
            }
            if (e.KeyCode == Keys.Right)
            {
                // high arrow key:
                high_arrow_select_();
            }
            if (e.KeyCode == Keys.Space)
            {
                record_data();
                change_trial_text();
            }
        }

        public void record_data()
        {
            trial++;
            if (trial < 5)
            {
                thisTrial.record_data();
                
                // randomly select new starting position for blue intensity
                Random rnd = new Random();
                INTENSITY = (double)Convert.ToDouble(rnd.Next(1, 100));
                thisTrial.Set_Short_Intensity(INTENSITY);
            }
            else
            {
                thisTrial.record_data();
                thisTrial.end_trial();
                this.Close();
            }
        }

        public void low_arrow_select()
        {
            if (INTENSITY - STEP > 0)
            {
                INTENSITY -= STEP;
            }
            else
            {
                Console.Beep();
            }
            thisTrial.Set_Short_Intensity(INTENSITY);
        }

        public void high_arrow_select_()
        {
            if (INTENSITY + STEP < 100)
            {
                INTENSITY += STEP;
            }
            else
            {
                Console.Beep();
            }
            thisTrial.Set_Short_Intensity(INTENSITY);
        }
        private void ForcedChoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close Shutter and Clear Gooch on close event
            thisTrial.end_flicker();
            thisTrial.clrGooch();
            thisTrial.closeShutter();

            // close eccentricity form
            ecc_form.Close();
        }

        private void change_trial_text()
        {
            this.trialLabel.Text = (string)Convert.ToString(trial + 1);
        }
    }
}
