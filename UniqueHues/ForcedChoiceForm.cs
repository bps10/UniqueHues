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
        private static char low_key;
        private static char high_key;
        private static StaircaseProgram.Staircase thisTrial = null;

        public ForcedChoiceForm(string uniqueHue, string subject_name)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            trial_count = 0;
            low_key = 'o';
            high_key = 'o';

            thisTrial = new StaircaseProgram.Staircase();
            thisTrial.RunStaircase(uniqueHue, subject_name);
        }

        public void changeLabel3text(string labelText, int key)
        {
            this.label3.Text = labelText;
            if (key == 0) { low_key = 'b'; }
            if (key == 1) { low_key = 'g'; }
            if (key == 2) { low_key = 'r'; }
            if (key == 3) { low_key = 'y'; }
            if (key == 4) { low_key = 'p'; }
        }

        public void changeLabel5text(string labelText, int key)
        {
            this.label5.Text = labelText;
            if (key == 0) { high_key = 'b'; }
            if (key == 1) { high_key = 'g'; }
            if (key == 2) { high_key = 'r'; }
            if (key == 3) { high_key = 'y'; }
            if (key == 4) { high_key = 'p'; }
        }

        // Detect all numeric characters at the form level and consume 1,  
        // 4, and 7. Note that Form.KeyPreview must be set to true for this 
        // event handler to be called. 
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if (e.KeyChar == low_key)
            {
                // low arrow key:
                low_arrow_select();
            }
            if (e.KeyChar == high_key)
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
