using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace DCT
{
    public class Dct
    {
        private Matrix dctMatrix;
        private Quantizer quantizer;

        public Dct(int quantity)
        {
            this.dctMatrix = this.Generate();
            this.quantizer = new Quantizer(quantity);
        }

        public List<List<int>> Compress(Matrix matrix)
        {
            // Формируем подматрицы 8х8
            var submatrixList = new List<Matrix>();
            for (int i = 0; i < matrix.Rows; i = i + 8)
                for (int j = 0; j < matrix.Cols; j = j + 8)
                    submatrixList.Add(matrix.Submatrix(i, j, 8, 8));

            // Применяем ДКТ
            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.dctMatrix * submatrixList[i] * this.dctMatrix.Transpose;

            // Квантуем все подматрицы
            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.quantizer.Quantize(submatrixList[i]);

            // Проходим зигзаг-сканированием по матрицам
            var vectorsList = new List<List<int>>();
            for (int i = 0; i < submatrixList.Count; i++)
                vectorsList.Add(ZigzagScan.Scan(submatrixList[i]));

            return vectorsList;
        }

        public void Decompress(List<List<int>> vectorsList)
        {
            var submatrixList = new List<Matrix>();
            for (int i = 0; i < vectorsList.Count; i++)
                submatrixList.Add(ZigzagScan.ReverseScan(vectorsList[i]));

            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.quantizer.Dequantize(submatrixList[i]);

            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.dctMatrix * submatrixList[i] * this.dctMatrix.Transpose;
        }

        public Matrix Generate()
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
