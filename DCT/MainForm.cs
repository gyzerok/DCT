using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DCT;
using Recognizer;

namespace Labs
{
    public partial class MainForm : Form
    {
        private Bitmap bmp;

        public MainForm()
        {
            InitializeComponent();
            label5.Text = trackBar1.Value.ToString();
        }

        #region DCT

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.bmp = (Bitmap)Bitmap.FromFile(dlg.FileName);

                this.pictureBox1.Image = this.bmp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dct = new Dct(trackBar1.Value);

            var yuvBitmap = BitmapYUV.FromBitmap(this.bmp);
            var newBmp = dct.Decompress(dct.Compress(yuvBitmap));

            this.pictureBox2.Image = newBmp.ToBitmap();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar1.Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // сохранение

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // загрузить

        }

        #endregion

        #region YUV

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(od.FileName);
                pictureBox3.Image = bitmap;
                BitmapYUV YUV = BitmapYUV.FromBitmap(bitmap);
                Matrix Y = YUV.YUVComponent(Component.Y);
                Matrix U = YUV.YUVComponent(Component.U);
                Matrix V = YUV.YUVComponent(Component.V);
                Bitmap bmpY = new Bitmap(bitmap.Width, bitmap.Height);
                Bitmap bmpU = new Bitmap(bitmap.Width, bitmap.Height);
                Bitmap bmpV = new Bitmap(bitmap.Width, bitmap.Height);
                for (int i = 0; i < bmpY.Width; i++)
                    for (int j = 0; j < bmpY.Height; j++)
                    {
                        ColorYUV yuv = new ColorYUV();
                        yuv.Y = Y[j, i];
                        yuv.U = 128;
                        yuv.V = 128;                        
                        bmpY.SetPixel(i, j, yuv.ToColor());
                        yuv = new ColorYUV();
                        yuv.Y = 0; 
                        yuv.U = U[j, i];
                        yuv.V = 128; 
                        bmpU.SetPixel(i, j, yuv.ToColor());
                        yuv = new ColorYUV();
                        yuv.Y = 0;
                        yuv.U = 128; 
                        yuv.V = V[j, i];
                        bmpV.SetPixel(i, j, yuv.ToColor());
                    }
                pictureBox4.Image = bmpY;
                pictureBox5.Image = bmpU;
                pictureBox6.Image = bmpV;
            }
        }

        #endregion                
                
        #region Recognizer

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp = (Bitmap)Bitmap.FromFile(od.FileName);
                pictureBox7.Image = bmp;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                Bitmap result = new Bitmap(bmp.Width, bmp.Height);
                Recognizer.Recognizer rec = new Recognizer.Recognizer(bmp);
                rec.AccuracyRange = 4;
                rec.OrthogonalAccuracy = 4;
                rec.Recognize();
                label8.Text = rec.Unknown.Count.ToString();
                label11.Text = rec.Circles.Count.ToString();
                label14.Text = rec.Rectangles.Count.ToString();
                foreach (Figure fig in rec.Circles)
                {
                    foreach (ColorPoint point in fig.Points)
                    {
                        result.SetPixel(point.X, point.Y, Color.Red);
                    }
                }
                foreach (Figure fig in rec.Rectangles)
                {
                    foreach (ColorPoint point in fig.Points)
                    {
                        result.SetPixel(point.X, point.Y, Color.Green);
                    }
                }
                foreach (Figure fig in rec.Unknown)
                {
                    foreach (ColorPoint point in fig.Points)
                    {
                        result.SetPixel(point.X, point.Y, Color.Blue);
                    }
                }
                pictureBox8.Image = result;
            }
        }  

        #endregion        
    }
}
