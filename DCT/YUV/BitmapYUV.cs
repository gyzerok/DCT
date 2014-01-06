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
        public int Width { get; private set; }
        public int Height { get; private set; }

        public BitmapYUV(int width, int height)
        {
            this.pixels = new ColorYUV[width, height];
            this.Width = width;
            this.Height = height;
        }        

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

        public Matrix YUVComponent(Component cmp)
        {
            var matrix = new Matrix(this.Height, this.Width);

            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                {
                    ColorYUV pixel = this.GetPixel(i, j);                   

                    switch (cmp)
                    {
                            case DCT.Component.Y:
                                matrix[j, i] = pixel.Y;
                                break;
                            case DCT.Component.U:
                                matrix[j, i] = pixel.U;
                                break;
                            case DCT.Component.V:
                                matrix[j, i] = pixel.V;
                                break;
                    }
                }

            return matrix;
        }

        public Matrix Component(Component cmp)
        {
            var matrix = new Matrix(this.Height, this.Width);

            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                {
                    ColorYUV pixel = this.GetPixel(i, j);

                    switch (cmp)
                    {
                        case DCT.Component.Y:
                            matrix[j, i] = pixel.Y - 128;
                            break;
                        case DCT.Component.U:
                            matrix[j, i] = pixel.U - 128;
                            break;
                        case DCT.Component.V:
                            matrix[j, i] = pixel.V - 128;
                            break;
                    }
                }

            return matrix;
        }

        public static BitmapYUV FromComponents(Matrix yMatrix, Matrix uMatrix, Matrix vMatrix)
        {
            var ret = new BitmapYUV(yMatrix.Cols, yMatrix.Rows);

            for (int i = 0; i < yMatrix.Rows; i++)
                for (int j = 0; j < yMatrix.Cols; j++)
                {
                    int y = yMatrix[i, j];
                    int u = uMatrix[i, j];
                    int v = vMatrix[i, j];

                    ret.SetPixel(j, i, ColorYUV.FromComponents(y + 128, u + 128, v + 128));   
                }

            return ret;
        }
    }
}
