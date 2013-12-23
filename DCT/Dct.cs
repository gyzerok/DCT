﻿using System;

namespace DCT
{
    public class Dct
    {
        public static Matrix<double> Generate()
        {
            var matrix = new Matrix<double>(8, 8);

            for (int i = 0; i < matrix.Height; i++)
                for (int j = 0; j < matrix.Width; j++)
                    if (i == 0)
                    {
                        matrix[i, j] = 1 / Math.Sqrt(8);
                    }
                    else
                    {
                        matrix[i, j] = 1/Math.Sqrt(8)*Math.Cos((2*j + 1)*i*3.14/2/8);
                    }

            return matrix;
        }
    }
}