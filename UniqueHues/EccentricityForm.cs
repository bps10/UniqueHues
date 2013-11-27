using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniqueHues
{
    public partial class EccentricityForm : Form
    {
        public EccentricityForm()
        {
            InitializeComponent();
            //this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            paint(CreateGraphics());

        }

        private void Form1_paint(object sender, PaintEventArgs e)
        {
            paint(e.Graphics);
        }


        private void paint(Graphics graphicsObj)
        {
            Pen BlackPen = new Pen(Color.FromArgb(255, 255, 0, 0), 50);
            Point ptFovea = new Point(10, 10);
            Point ptFovea1 = new Point(10, 12);
            Point pt6deg= new Point(30, 30);
            Point pt6deg1 = new Point(30, 32);

            // get graphics context and create buffer
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics b = context.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            // draw to buffer
            b.Graphics.FillRectangle(Brushes.Black, this.DisplayRectangle);
            b.Graphics.DrawLine(BlackPen, ptFovea, ptFovea);
            b.Graphics.DrawLine(BlackPen, pt6deg, pt6deg);
            b.Render();
        }
    }
}
