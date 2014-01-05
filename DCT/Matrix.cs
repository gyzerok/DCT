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

        // 1 + 2, матрицы одинакового размера
        private static Matrix CompileInRow(List<Matrix> submatrixList)
        {
            if (submatrixList.Count == 0)
                throw new System.Exception("нужно больше матриц!");
            int rows = submatrixList[0].Rows;
            int cols = submatrixList[0].Cols;
            for (int i = 0; i < submatrixList.Count; i++)
            {
                if (submatrixList[i].Cols != cols || submatrixList[i].Rows != rows)
                    throw new Exception("разные размеры");
            }

            Matrix result = new Matrix(rows, cols * submatrixList.Count);
            for (int i = 0; i < submatrixList.Count; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        result[j, i * cols + k] = submatrixList[i][j, k];
                    }
                }
            }
            return result;
        }

        // 1
        // +
        // 2, матрицы одинакового размера
        private static Matrix CompileInColumn(List<Matrix> submatrixList)
        {
            if (submatrixList.Count == 0)
                throw new System.Exception("нужно больше матриц!");
            int rows = submatrixList[0].Rows;
            int cols = submatrixList[0].Cols;
            for (int i = 0; i < submatrixList.Count; i++)
            {
                if (submatrixList[i].Cols != cols || submatrixList[i].Rows != rows)
                    throw new Exception("разные размеры");
            }

            Matrix result = new Matrix(rows * submatrixList.Count, cols);
            for (int i = 0; i < submatrixList.Count; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        result[i * rows + j, k] = submatrixList[i][j, k];
                    }
                }
            }
            return result;
        }

        public static Matrix FromList(List<Matrix> submatrixList)
        {
            int size = (int)Math.Sqrt(submatrixList.Count);
            if (size * size != submatrixList.Count)
                throw new System.Exception("Неправильное кол-во матриц в списке");

            List<Matrix> matrices = new List<Matrix>();
            List<Matrix> column = new List<Matrix>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrices.Add(submatrixList[i * size + j]);
                }
                column.Add(Matrix.CompileInRow(matrices));
                matrices.Clear();
            }
            return Matrix.CompileInColumn(column);
        }
    }
}
