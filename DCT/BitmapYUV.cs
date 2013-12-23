using System.Drawing;

namespace DCT
{
    public class BitmapYUV
    {
        private ColorYUV[,] pixels;

        public BitmapYUV(int width, int height)
        {
            this.pixels = new ColorYUV[width, height];
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ColorYUV GetPixel(int x, int y)
        {
            return this.pixels[x, y];
        }

        public void SetPixel(int x, int y, ColorYUV pixel)
        {
            this.pixels[x, y] = pixel;
        }

        public static BitmapYUV FromBitmap(Bitmap bitmap)
        {
            BitmapYUV yuvImage = new BitmapYUV(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    yuvImage.SetPixel(i, j, ColorYUV.FromColor(bitmap.GetPixel(i, j)));

            return yuvImage;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    bitmap.SetPixel(i, j, this.GetPixel(i, j).ToColor());

            return bitmap;
        }
    }
}
