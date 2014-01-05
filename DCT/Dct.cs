using System;

namespace DCT
{
    public class Dct
    {
        public static void Compress()
        {
            
        }

        public static void Decompress()
        {
            
        }

        public static Matrix Generate()
        {
            var matrix = new Matrix(8, 8);

            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    if (i == 0)
                        matrix[i, j] = (int)(1 / Math.Sqrt(8));
                    else
                        matrix[i, j] = (int)(1 / Math.Sqrt(8) * Math.Cos((2 * j + 1) * i * 3.14 / 2 / 8));

            return matrix;
        }
    }
}
