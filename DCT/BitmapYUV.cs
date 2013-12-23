using System.Drawing;

namespace DCT
{
    public enum Component
    {
        Y,
        U,
        V
    }

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

        public Matrix Component(Component cmp)
        {
            var matrix = new Matrix(this.Width, this.Height);

            for (int i = 0; i < this.Height; i++)
                for (int j = 0; j < this.Width; j++)
                {
                    var pixel = this.GetPixel(i, j);

                    switch (cmp)
                    {
                            case DCT.Component.Y:
                                matrix[i, j] = pixel.Y - 128;
                                break;
                            case DCT.Component.U:
                                matrix[i, j] = pixel.U - 128;
                                break;
                            case DCT.Component.V:
                                matrix[i, j] = pixel.V - 128;
                                break;
                    }
                }

            return matrix;
        }
    }
}
