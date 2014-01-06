using System.Drawing;

namespace DCT
{
    public class ColorYUV
    {
        public int Y { get; set; }
        public int U { get; set; }
        public int V { get; set; }

        public static ColorYUV FromColor(Color color)
        {
            ColorYUV yuvColor = new ColorYUV();

            yuvColor.Y = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
            yuvColor.U = (int)(-0.14713 * color.R - 0.28886 * color.G + 0.436 * color.B + 128);
            yuvColor.V = (int)(0.615 * color.R - 0.51499 * color.G - 0.10001 * color.B + 128);

            return yuvColor;
        }

        public Color ToColor()
        {
            int R, G, B;

            R = (int)(this.Y + 1.13983 * (this.V - 128));
            G = (int)(this.Y - 0.39465 * (this.U - 128) - 0.58060 * (this.V - 128));
            B = (int)(this.Y + 2.03211 * (this.U - 128));

            R = R > 255 ? 255 : R < 0 ? 0 : R;
            G = G > 255 ? 255 : G < 0 ? 0 : G;
            B = B > 255 ? 255 : B < 0 ? 0 : B;

            return Color.FromArgb(R, G, B);
        }

        public static ColorYUV FromComponents(int y, int u, int v)
        {
            var ret = new ColorYUV();

            ret.Y = y;
            ret.U = u;
            ret.V = v;

            return ret;
        }
    }
}
