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
    public partial class ForcedChoiceForm : Form
    {
        private static double trial_count;
        private static StaircaseProgram.Staircase thisTrial = null;

        public ForcedChoiceForm(string uniqueHue, string subject_name)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyPress);

            trial_count = 0;

            thisTrial = new StaircaseProgram.Staircase();
            thisTrial.RunStaircase(uniqueHue, subject_name);
        }

        public void changeLabel3text(string labelText, int key)
        {
            this.label3.Text = labelText;
        }

        public void changeLabel5text(string labelText, int key)
        {
            this.label5.Text = labelText;
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

        public void low_arrow_select()
        {
            if (thisTrial.end_staircase == true) { this.Close(); }
            else 
            { 
                thisTrial.continueStaircase(1); trial_count++;
                if (thisTrial.end_staircase == true) { this.Close(); }
            }
        }

        public void high_arrow_select_()
        {
            if (thisTrial.end_staircase == true) { this.Close(); }
            else
            {
                thisTrial.continueStaircase(2); trial_count++;
                if (thisTrial.end_staircase == true) { this.Close(); }
            }
        }

        private void ForcedChoiceForm_Load(object sender, EventArgs e)
        {
            //return 2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
