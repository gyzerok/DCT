using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DCT;

namespace Labs
{
    public partial class MainForm : Form
    {
        private Bitmap bmp;

        public MainForm()
        {
            InitializeComponent();
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
            var dct = new Dct(6);

            var yuvBitmap = BitmapYUV.FromBitmap(this.bmp);
            var newBmp = dct.Decompress(dct.Compress(yuvBitmap));

            this.pictureBox2.Image = newBmp.ToBitmap();
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
    }
}
