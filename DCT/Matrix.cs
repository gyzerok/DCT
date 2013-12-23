using System.Globalization;
using System.Runtime.CompilerServices;

namespace DCT
{
    public class Matrix
    {
        private int[,] vals;
        public int Cols { get { return vals.GetLength(1); } }
        public int Rows { get { return vals.GetLength(0); } }

        public Matrix(int cols, int rows)
        {
            this.vals = new int[rows, cols];
        }

        /// <summary>
        /// element
        /// </summary>
        /// <param name="col">Столбец</param>
        /// <param name="row">Строка</param>
        public int this[int row, int col]
        {
            get
            {
                return this.vals[row, col];   
            }
            set
            {
                this.vals[row, col] = value;
            }
        }

        /// <summary>
        /// Получаем транспонированную матрицу
        /// </summary>
        public Matrix Transpose
        {
            get
            {
                var matrix = new Matrix(this.Cols, this.Rows);

                for (int i = 0; i < this.Rows; i++)
                    for (int j = 0; j < this.Cols; j++)
                        matrix[j, i] = this[i, j];

                return matrix;
            }
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            var matrix = new Matrix(left.Cols, right.Rows);

            for (int i = 0; i < left.Rows; i++)
            {
                var sum = 0;
                for (int j = 0; j < right.Rows; j++)
                {
                    for (int k = 0; k < left.Cols; k++)
                        sum += left[i, k] * right[k, j];

                    matrix[i, j] = sum;
                }
            }

            return matrix;
        }

        public int[,] Sasai()
        {
            return this.vals;
        }
    }
}
