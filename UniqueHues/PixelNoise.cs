using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace UniqueHues
{
    public partial class PixelNoise : Form
    {
        private int FRAMES = 25;
        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        Dictionary<int, Bitmap> buffer_dict = new Dictionary<int, Bitmap>();

        public PixelNoise()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PixelNoiseForm_FormClosing);
            this.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            create_buffer_dict();

            
            MyTimer.Interval = (50);
            MyTimer.Tick += new EventHandler(timer1_Tick);
            MyTimer.Start();

        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            paint(CreateGraphics());
        }

        private void Form1_paint(object sender, PaintEventArgs e)
        {
            paint(e.Graphics);
        }

        private void paint(Graphics graphicsObj)
        {
            //Graphics graphicsObj = e.Graphics;
            Rectangle rect = new Rectangle(275, 175, 200, 200);

            Random rnd = new Random();
            int i = rnd.Next(1, FRAMES);

            // get graphics context and create buffer
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics b = context.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            // draw to buffer
            b.Graphics.FillRectangle(Brushes.Black, this.DisplayRectangle);

            b.Graphics.DrawImage(buffer_dict[i], 150, 50, buffer_dict[i].Width * 2, buffer_dict[i].Height * 2);
            b.Graphics.FillEllipse(Brushes.Black, rect);

            // renders content of buffer to drawing surface
            b.Render();
        }

        private void create_buffer_dict()
        {
            for (int i = 0; i < FRAMES; i++)
            {
                buffer_dict.Add(i, CreateBitmap());
            }
        }

        private Bitmap CreateBitmap()
        {
            const int pix_size = 5;
            const int pix = 45;
            System.Drawing.Bitmap checks = new System.Drawing.Bitmap(
                pix_size * pix, pix_size * pix);
            Random rnd = new Random();
            // The checkerboard consists of 15 rows and 15 columns.
            // Each square in the checkerboard is 15 x 15 pixels.
            // The nested for loops are used to calculate the position
            // of each square on the bitmap surface, and to set the
            // pixels to black or white.

            // The two outer loops iterate through 
            //  each square in the bitmap surface.

            for (int columns = 0; columns < pix; columns++)
            {
                for (int rows = 0; rows < pix; rows++)
                {
                    // Determine whether the current sqaure
                    // should be red or green.

                    Color color;
                    int choice = rnd.Next(2);
                    int val = rnd.Next(120, 240);
                    color = choice == 0 ? Color.FromArgb(255, val, 0, 0) : Color.FromArgb(255, 0, val, 0);

                    // Set the pixel to the correct color
                    // The two inner loops iterate through
                    // each pixel in an individual square.
                    for (int j = columns * pix_size; j < (columns * pix_size) +
                        pix_size; j++)
                    {
                        for (int k = rows * pix_size; k < (rows * pix_size) +
                            pix_size; k++)
                        {


                            checks.SetPixel(j, k, color);
                        }
                    }
                }
            }
            return checks;
        }

        private void PixelNoiseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyTimer.Stop();
        }

    }
}
