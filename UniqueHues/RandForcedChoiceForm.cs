﻿using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using StaircaseProgram;


namespace UniqueHues
{

    public partial class RandForcedChoiceForm : Form
    {
        private static StaircaseProgram.Randomized thisTrial = null;

        public RandForcedChoiceForm(string uniqueHue, string subject_name, Dictionary<string, float> parameters)
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyPress);

            //trial_count = 0;
            float intensity = parameters["intensity"];
            float bandwidth = parameters["bandwidth"];
            float repeats = parameters["repeats"];
            int step = (int)Convert.ToDouble(parameters["step"]);

            thisTrial = new StaircaseProgram.Randomized(bandwidth, intensity, repeats, step);
            thisTrial.RunForcedChoice(uniqueHue, subject_name);
        }

        // Note that Form.KeyPreview must be set to true for this 
        // event handler to be called. 
        void Form1_KeyPress(object sender, KeyEventArgs e)
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

        }
        
        void low_arrow_select()
        {
            thisTrial.continueRandomized(1);
            if (thisTrial.end_wavelength == thisTrial.trial) { this.Close(); }
        }

        public void high_arrow_select_()
        {
            thisTrial.continueRandomized(2);
            if (thisTrial.end_wavelength == thisTrial.trial) { this.Close(); }
        }

        public void changeLabel3text(string labelText, int key)
        {
            this.label3.Text = labelText;
        }

        public void changeLabel5text(string labelText, int key)
        {
            this.label5.Text = labelText;
        }

        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close Shutter and Clear Gooch on close event
            thisTrial.clearGooch();
            thisTrial.closeShutter();
        }
    }
}
