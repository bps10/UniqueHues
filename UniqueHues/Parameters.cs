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
        public Parameters(Dictionary<string, float> param)
        {
            InitializeComponent();
            this.intensityBox.Text = (string)Convert.ToString(param["intensity"]);
            this.bandwidthBox.Text = (string)Convert.ToString(param["bandwidth"]);
            this.stepSizeBox.Text = (string)Convert.ToString(param["step"]);
            this.repeatsBox.Text = (string)Convert.ToString(param["repeats"]);
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
                parameters.Add("repeats", (float)Convert.ToDouble(repeatsBox.Text));

                return parameters;
            }
        }
    }
}
