using System.Runtime.CompilerServices;

namespace DCT
{
    public class Matrix
    {
        private int[,] vals;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Matrix(int width, int height)
        {
            this.vals = new int[width, height];
            this.Width = width;
            this.Height = height;
        }

        public int this[int x, int y]
        {
            get
            {
                return this.vals[x, y];   
            }
            set
            {
                this[x, y] = value;
            }
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            Matrix matrix = new Matrix(left.Width, right.Height);

            for (int i = 0; i < left.Height; i++)
                for (int j = 0; j < right.Width; j++)
                    matrix[i, j] = 0;

            return matrix;
        }

        public Matrix Transpose
        {
            get
            {
                Matrix matrix = new Matrix(this.Width, this.Height);

                for (int i = 0; i < this.Height; i++)
                    for (int j = 0; j < this.Width; j++)
                        matrix[j, i] = this[i, j];

                return matrix;
            }
        }
    }
}
