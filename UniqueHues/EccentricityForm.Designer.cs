namespace UniqueHues
{
    partial class EccentricityForm
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape5,
            this.lineShape4,
            this.lineShape3,
            this.lineShape2});
            this.shapeContainer1.Size = new System.Drawing.Size(1382, 955);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape5
            // 
            this.lineShape5.BorderWidth = 2;
            this.lineShape5.Name = "lineShape5";
            this.lineShape5.X1 = 516;
            this.lineShape5.X2 = 516;
            this.lineShape5.Y1 = 248;
            this.lineShape5.Y2 = 271;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderWidth = 2;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 526;
            this.lineShape4.X2 = 506;
            this.lineShape4.Y1 = 260;
            this.lineShape4.Y2 = 260;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderWidth = 2;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 946;
            this.lineShape3.X2 = 926;
            this.lineShape3.Y1 = 262;
            this.lineShape3.Y2 = 262;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderWidth = 2;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 936;
            this.lineShape2.X2 = 936;
            this.lineShape2.Y1 = 250;
            this.lineShape2.Y2 = 273;
            // 
            // EccentricityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1382, 955);
            this.Controls.Add(this.shapeContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EccentricityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EccentricityForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;

    }
}