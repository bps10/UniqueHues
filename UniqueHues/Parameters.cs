using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniqueHues
{
    public partial class Parameters : Form
    {
        public Parameters()
        {
            InitializeComponent();
            this.intensityBox.Text = "100";
            this.bandwidthBox.Text = "10";
            this.stepSizeBox.Text = "5";
            this.repeatsBox.Text = "10";
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Dictionary<string, float> EnteredValue
        {
            get
            {
                Dictionary<string, float> parameters = new Dictionary<string, float>();
                parameters.Add("intensity", (float)Convert.ToDouble(intensityBox.Text));
                parameters.Add("bandwidth", (float)Convert.ToDouble(bandwidthBox.Text));
                parameters.Add("step", (float)Convert.ToDouble(stepSizeBox.Text));
                parameters.Add("repeat", (float)Convert.ToDouble(repeatsBox.Text));

                return parameters;
            }
        }
    }
}
