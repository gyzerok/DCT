using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System;

namespace DCT
{
    public class Matrix
    {
        private int[,] vals;
        
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public int this[int row, int col]
        {
            get
            {
                return vals[row, col];
            }

            set
            {
                vals[row, col] = value;
            }
        }

        public Matrix Transpose
        {
            get
            {
                Matrix result = new Matrix(Cols, Rows);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        result[j, i] = vals[i, j];
                    }
                }

                return result;
            }
        }               

        public Matrix(int rows, int cols)
        {
            vals = new int[rows, cols];
            this.Rows = rows;
            this.Cols = cols;            
        }                

        public static Matrix operator *(Matrix A, Matrix B)  
        {            
            Matrix result = new Matrix(A.Rows, B.Cols);
            
            if (A.Cols != B.Rows)
                throw new System.Exception("UCHI MATAN, S00QA!");

            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < A.Cols; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public Matrix Submatrix(int row, int col, int sizeRows, int sizeCols)
        {
            Matrix result = new Matrix(sizeRows, sizeCols);
            for (int i = 0; i < sizeRows; i++)
            {
                for (int j = 0; j < sizeCols; j++)
                {
                    result[i, j] = vals[row + i, col + j];
                }
            }
            return result;
        }

        public static Matrix FromList(List<Matrix> submatrixList)
        {
            int size = (int) Math.Sqrt(submatrixList.Count) * 8;
            var ret = new Matrix(size, size);

            int k = 0;
            while (k % size != 0 && )
        }
    }
}
