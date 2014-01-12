using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DCT
{
    public class Histogram
    {
        private Bitmap bitmap;
        private int[] red = new int[256];
        private int[] green = new int[256];
        private int[] blue = new int[256];

        public Histogram(Bitmap targetBMP)
        {
            this.bitmap = targetBMP;
            this.Calculate();
        }

        private void Calculate()
        {
            for (int i = 0; i < 256; i++)
            {
                this.red[i] = 0;
                this.green[i] = 0;
                this.blue[i] = 0;
            }

            for (int i = 0; i < this.bitmap.Height; i++)
            {
                for (int j = 0; j < this.bitmap.Width; j++)
                {
                    this.red[this.bitmap.GetPixel(j, i).R]++;
                    this.green[this.bitmap.GetPixel(j, i).G]++;
                    this.blue[this.bitmap.GetPixel(j, i).B]++;
                }
            }
        }

        public void Draw(PictureBox Red, PictureBox Green, PictureBox Blue)
        {
            int max = 0;
            for (var i = 0; i < 256; i++)
            {
                if (this.red[i] > max)
                    max = this.red[i];
                if (this.green[i] > max)
                    max = this.green[i];
                if (this.blue[i] > max)
                    max = this.blue[i];
            }

            this.DrawHistogram(Red, this.red, max, Color.Red);
            this.DrawHistogram(Green, this.green, max, Color.Green);
            this.DrawHistogram(Blue, this.blue, max, Color.Blue);
        }

        private void DrawHistogram(PictureBox pb, int[] colorArray, int max, Color color)
        {
            Graphics g = pb.CreateGraphics();

            List<Rectangle> rects = new List<Rectangle>();

            var x = 0;

            for (var i = 0; i < 256; i++)
            {
                int height = (int)Math.Round(colorArray[i] * 1.0 / max * pb.Height);
                rects.Add(new Rectangle(x, pb.Height - height, 2, height));
                x += 2;
            }

            g.FillRectangles(new SolidBrush(color), rects.ToArray());
        }

    }
}
