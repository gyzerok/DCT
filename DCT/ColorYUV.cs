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
            yuvColor.U = color.B - yuvColor.Y;
            yuvColor.V = color.R - yuvColor.Y;

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
    }
}
