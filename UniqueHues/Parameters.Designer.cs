﻿namespace UniqueHues
{
    partial class Parameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.intensityBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stepSizeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bandwidthBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.repeats = new System.Windows.Forms.Label();
            this.repeatsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "intensity (%)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 284);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // intensityBox
            // 
            this.intensityBox.Location = new System.Drawing.Point(175, 24);
            this.intensityBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.intensityBox.MaxLength = 3;
            this.intensityBox.Name = "intensityBox";
            this.intensityBox.Size = new System.Drawing.Size(81, 29);
            this.intensityBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(20, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "step size (nm)";
            // 
            // stepSizeBox
            // 
            this.stepSizeBox.Location = new System.Drawing.Point(175, 180);
            this.stepSizeBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.stepSizeBox.Name = "stepSizeBox";
            this.stepSizeBox.Size = new System.Drawing.Size(81, 29);
            this.stepSizeBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(20, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "bandwidth (nm)";
            // 
            // bandwidthBox
            // 
            this.bandwidthBox.Location = new System.Drawing.Point(175, 74);
            this.bandwidthBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bandwidthBox.Name = "bandwidthBox";
            this.bandwidthBox.Size = new System.Drawing.Size(81, 29);
            this.bandwidthBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(20, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Randomized only:";
            // 
            // repeats
            // 
            this.repeats.AutoSize = true;
            this.repeats.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.repeats.Location = new System.Drawing.Point(20, 229);
            this.repeats.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.repeats.Name = "repeats";
            this.repeats.Size = new System.Drawing.Size(72, 24);
            this.repeats.TabIndex = 8;
            this.repeats.Text = "repeats";
            // 
            // repeatsBox
            // 
            this.repeatsBox.Location = new System.Drawing.Point(175, 229);
            this.repeatsBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.repeatsBox.Name = "repeatsBox";
            this.repeatsBox.Size = new System.Drawing.Size(81, 29);
            this.repeatsBox.TabIndex = 9;
            // 
            // Parameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(284, 338);
            this.Controls.Add(this.repeatsBox);
            this.Controls.Add(this.repeats);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bandwidthBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stepSizeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.intensityBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Parameters";
            this.Text = "Parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox intensityBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stepSizeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bandwidthBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label repeats;
        private System.Windows.Forms.TextBox repeatsBox;
    }
}