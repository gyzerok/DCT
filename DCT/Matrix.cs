using System.Globalization;
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
            this.vals = new int[height, width];
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
                this.vals[x, y] = value;
            }
        }

        /// <summary>
        /// Получаем транспонированную матрицу
        /// </summary>
        public Matrix Transpose
        {
            get
            {
                var matrix = new Matrix(this.Width, this.Height);

                for (int i = 0; i < this.Height; i++)
                    for (int j = 0; j < this.Width; j++)
                        matrix[j, i] = this[i, j];

                return matrix;
            }
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            var matrix = new Matrix(left.Width, right.Height);

            for (int i = 0; i < left.Height; i++)
            {
                var sum = 0;
                for (int j = 0; j < right.Width; j++)
                {
                    for (int k = 0; k < left.Width; k++)
                        sum += left[i, k] * right[k, j];

                    matrix[i, j] = sum;
                }
            }

            return matrix;
        }
    }
}
