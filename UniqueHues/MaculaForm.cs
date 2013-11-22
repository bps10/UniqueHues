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
        private double INTENSITY = 100;
        private double trial;

        public MaculaForm(string name)
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ForcedChoiceForm_FormClosing);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(KeyPress);
            thisTrial = new UniqueHues.Macula(name);
            trial = 0;
            //thisTrial.RunMacula("name");

        }
        public void run()
        {
            thisTrial.RunMacula();
        }
        void KeyPress(object sender, KeyEventArgs e)
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
            INTENSITY -= 1;
            thisTrial.Set_Short_Intensity(INTENSITY);
        }

        public void high_arrow_select_()
        {
            INTENSITY += 1;
            thisTrial.Set_Short_Intensity(INTENSITY);
        }
        private void ForcedChoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close Shutter and Clear Gooch on close event
            thisTrial.end_flicker();
            thisTrial.clrGooch();
            thisTrial.closeShutter();
        }
    }
}
