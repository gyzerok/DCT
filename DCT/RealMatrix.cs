using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT
{
    public class RealMatrix
    {
        private double[,] vals;
        
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public double this[int row, int col]
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

        public RealMatrix Transpose
        {
            get
            {
                RealMatrix result = new RealMatrix(Cols, Rows);

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

        public RealMatrix(int rows, int cols)
        {
            vals = new double[rows, cols];
            this.Rows = rows;
            this.Cols = cols;            
        }

        public static RealMatrix operator *(RealMatrix A, RealMatrix B)  
        {
            RealMatrix result = new RealMatrix(A.Rows, B.Cols);
            
            if (A.Cols != B.Rows)
                throw new System.Exception("UCHI MATAN, S00QA!");

            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < A.Cols; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public RealMatrix Submatrix(int row, int col, int sizeRows, int sizeCols)
        {
            RealMatrix result = new RealMatrix(sizeRows, sizeCols);
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
        private static RealMatrix CompileInRow(List<RealMatrix> submatrixList)
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

            RealMatrix result = new RealMatrix(rows, cols * submatrixList.Count);
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
        private static RealMatrix CompileInColumn(List<RealMatrix> submatrixList)
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

            RealMatrix result = new RealMatrix(rows * submatrixList.Count, cols);
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

        public static RealMatrix FromList(List<RealMatrix> submatrixList)
        {
            int size = (int)Math.Sqrt(submatrixList.Count);
            if (size * size != submatrixList.Count)
                throw new System.Exception("Неправильное кол-во матриц в списке");

            List<RealMatrix> matrices = new List<RealMatrix>();
            List<RealMatrix> column = new List<RealMatrix>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrices.Add(submatrixList[i * size + j]);
                }
                column.Add(RealMatrix.CompileInRow(matrices));
                matrices.Clear();
            }
            return RealMatrix.CompileInColumn(column);
        }

        public static explicit operator RealMatrix(Matrix matrix)
        {
            RealMatrix result = new RealMatrix(matrix.Rows, matrix.Cols);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    result[i, j] = Convert.ToDouble(matrix[i, j]);
            return result;
        }

        public static explicit operator Matrix(RealMatrix matrix)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Cols);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    result[i, j] = Convert.ToInt32(matrix[i, j]);
            return result;
        }
    }
}
