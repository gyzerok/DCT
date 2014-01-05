using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCT
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

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
    }
}
