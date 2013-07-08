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

        public HomeForm()
        {
            InitializeComponent();
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
            // make sure form set to null to clear previous instance
            ForcedChoiceForm form = new ForcedChoiceForm(uniqueHue, subject_name);

            string label1_text = "";
            string label2_text = "";
            int key1 = 0;
            int key2 = 0;

            if (uniqueHue == "yellow")
            {
                label1_text = "'g' = too green"; key1 = 1;
                label2_text = "'r' = too red"; key2 = 2;
            }
            if (uniqueHue == "blue")
            {
                label1_text = "'r' = too red"; key1 = 2;
                label2_text = "'g' = too green"; key2 = 1;
            }
            if (uniqueHue == "green")
            {
                label1_text = "'b' = too blue"; key1 = 0;
                label2_text = "'y' = too yellow"; key2 = 3;
            }
            form.changeLabel3text(label1_text, key1);
            form.changeLabel5text(label2_text, key2);

            form.Show();

        }
    }
}
