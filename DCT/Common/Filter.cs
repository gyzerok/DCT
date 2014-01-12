using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Common
{
    public class Filter
    {
        private Bitmap bmp;

        public Filter(Bitmap bmp)
        {
            this.bmp = bmp;
        }

        public Bitmap Grayscale
        {
            get
            {
                var ret = (Bitmap)this.bmp.Clone();

                for (int i = 0; i < ret.Width; i++)
                    for (int j = 0; j < ret.Height; j++)
                    {
                        var color = ret.GetPixel(i, j);
                        var newColor = (color.R + color.G + color.B) / 3;

                        ret.SetPixel(i, j, Color.FromArgb(255, newColor, newColor, newColor));
                    }

                return ret;
            }
        }

        public Bitmap Mirror
        {
            get
            {
                var ret = (Bitmap)this.bmp.Clone();

                int i1 = 0, i2 = ret.Width - 1;

                while (i1 < i2)
                {
                    for (int j = 0; j < ret.Height; j++)
                    {
                        var tmp = ret.GetPixel(i1, j);
                        ret.SetPixel(i1, j, ret.GetPixel(i2, j));
                        ret.SetPixel(i2, j, tmp);
                    }

                    i1++; i2--;
                }

                return ret;
            }
        }

        public Bitmap Negative
        {
            get
            {
                var ret = (Bitmap)this.bmp.Clone();

                for (int i = 0; i < ret.Width; i++)
                    for (int j = 0; j < ret.Height; j++)
                    {
                        var color = ret.GetPixel(i, j);

                        ret.SetPixel(i, j, Color.FromArgb(255, 255 - color.R, 255 - color.G, 255 - color.B));
                    }

                return ret;
            }
        }

        public Bitmap Contoured
        {
            get
            {
                var ret = (Bitmap)this.bmp.Clone();

                for (int i = 1; i < ret.Width; i++)
                    for (int j = 1; j < ret.Height; j++)
                    {
                        var color0 = this.bmp.GetPixel(i - 1, j - 1);
                        var color1 = this.bmp.GetPixel(i, j);

                        ret.SetPixel(i, j, this.Minus(color0, color1));
                    }

                return ret;
            }
        }

        private Color Minus(Color color0, Color color1)
        {
            int R = Math.Abs(color1.R - color0.R);
            int G = Math.Abs(color1.G - color0.G);
            int B = Math.Abs(color1.B - color0.B);

            R = (R > 255) ? 255 : (R < 0) ? 0 : R;
            G = (G > 255) ? 255 : (G < 0) ? 0 : G;
            B = (B > 255) ? 255 : (B < 0) ? 0 : B;

            return Color.FromArgb(255, R, G, B);
        }
    }
}
