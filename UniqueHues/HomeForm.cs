using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniqueHues
{
    public partial class HomeForm : Form
    {
        private string uniqueHue = "green";
        private bool randomized_opt = false;

        Dictionary<string, float> PARAMETERS = null;

        public HomeForm()
        {
            InitializeComponent();
            randomizeToolStripMenuItem.Checked = false;
            calibrationToolStripMenuItem.Checked = false;
            maculaToolStripMenuItem.Checked = false;

            PARAMETERS = new Dictionary<string,float>();
            PARAMETERS.Add("intensity", 100);
            PARAMETERS.Add("bandwidth", 10);
            PARAMETERS.Add("step", 5);
            PARAMETERS.Add("repeats", 10);
            PARAMETERS.Add("frequency", 13);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            uniqueHue = "yellow";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            uniqueHue = "green";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            uniqueHue = "blue";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subject_name = this.textBox1.Text;
            if (calibrationToolStripMenuItem.Checked)
            {
                Calibrate form1 = new Calibrate(PARAMETERS["bandwidth"], PARAMETERS["intensity"]);
                form1.Show();
            }
            else if (maculaToolStripMenuItem.Checked)
            {
                MaculaForm Macula_form = new MaculaForm(subject_name, PARAMETERS["frequency"]);
                //Rand_form.changeLabel3text(label1_text, key1);
                //Rand_form.changeLabel5text(label2_text, key2);
                Macula_form.Show();
                Macula_form.run();
            }
            else
            {
                
                // make sure form set to null to clear previous instance
                ForcedChoiceForm form = null;
                // rand form as well
                RandForcedChoiceForm Rand_form = null;
                

                string label1_text = "";
                string label2_text = "";
                int key1 = 0;
                int key2 = 0;

                if (uniqueHue == "yellow")
                {
                    label1_text = "left = too green"; key1 = 1;
                    label2_text = "right = too red"; key2 = 2;
                }
                if (uniqueHue == "blue")
                {
                    label1_text = "left = too purple"; key1 = 4;
                    label2_text = "right = too green"; key2 = 1;
                }
                if (uniqueHue == "green")
                {
                    label1_text = "left = too blue"; key1 = 0;
                    label2_text = "right = too yellow"; key2 = 3;
                }


                if (randomized_opt)
                {
                    Rand_form = new RandForcedChoiceForm(uniqueHue, subject_name, PARAMETERS);
                    Rand_form.changeLabel3text(label1_text, key1);
                    Rand_form.changeLabel5text(label2_text, key2);
                    Rand_form.Show();
                }

                else
                {
                    form = new ForcedChoiceForm(uniqueHue, subject_name, PARAMETERS);
                    form.changeLabel3text(label1_text, key1);
                    form.changeLabel5text(label2_text, key2);

                    form.Show();
                }
            }
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (randomizeToolStripMenuItem.Checked)
            {
                randomized_opt = false;
                randomizeToolStripMenuItem.Checked = false;
            }

            else
            {
                randomized_opt = true;
                randomizeToolStripMenuItem.Checked = true;
                maculaToolStripMenuItem.Checked = false;
                calibrationToolStripMenuItem.Checked = false;
            }
           
        }

        private void calibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (calibrationToolStripMenuItem.Checked == true)
            {
                calibrationToolStripMenuItem.Checked = false;
            }
            else
            {
                calibrationToolStripMenuItem.Checked = true;
                randomizeToolStripMenuItem.Checked = false;
                randomized_opt = false;
                maculaToolStripMenuItem.Checked = false;
            }
        }
        private void maculaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (maculaToolStripMenuItem.Checked)
            {
                maculaToolStripMenuItem.Checked = false;
            }

            else
            {
                randomized_opt = false;
                randomizeToolStripMenuItem.Checked = false;
                maculaToolStripMenuItem.Checked = true;
                calibrationToolStripMenuItem.Checked = false;
            }
        }

        private void setParamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameters param_form = new Parameters(PARAMETERS);
            param_form.ShowDialog();
            PARAMETERS = param_form.EnteredValue;
        }


    }
}
