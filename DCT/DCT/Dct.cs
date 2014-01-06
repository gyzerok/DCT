using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace DCT
{
    public class Dct
    {
        private RealMatrix dctMatrix;
        private Quantizer quantizer;

        public Dct(int quantity)
        {
            this.dctMatrix = this.Generate();
            this.quantizer = new Quantizer(quantity);
        }

        public List<List<List<int>>> Compress(BitmapYUV bmp)
        {
            var ret = new List<List<List<int>>>();

            var yMatrix = bmp.Component(Component.Y);
            var uMatrix = bmp.Component(Component.U);
            var vMatrix = bmp.Component(Component.V);

            ret.Add(this.Compress(yMatrix));
            ret.Add(this.Compress(uMatrix));
            ret.Add(this.Compress(vMatrix));

            return ret;
        }

        public BitmapYUV Decompress(List<List<List<int>>> compressedData)
        {
            var yMatrix = this.Decompress(compressedData[0]);
            var uMatrix = this.Decompress(compressedData[1]);
            var vMatrix = this.Decompress(compressedData[2]);

            return BitmapYUV.FromComponents(yMatrix, uMatrix, vMatrix);
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
                submatrixList[i] = (Matrix)(this.dctMatrix * (RealMatrix)submatrixList[i] * this.dctMatrix.Transpose);

            // Квантуем все подматрицы
            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.quantizer.Quantize(submatrixList[i]);

            // Проходим зигзаг-сканированием по матрицам
            var vectorsList = new List<List<int>>();
            for (int i = 0; i < submatrixList.Count; i++)
                vectorsList.Add(ZigzagScan.Scan(submatrixList[i]));

            return vectorsList;
        }

        public Matrix Decompress(List<List<int>> vectorsList)
        {
            var submatrixList = new List<Matrix>();

            // Производим обратное сканирование
            for (int i = 0; i < vectorsList.Count; i++)
                submatrixList.Add(ZigzagScan.ReverseScan(vectorsList[i]));

            // Деквантование
            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = this.quantizer.Dequantize(submatrixList[i]);

            // Обратное ДКП
            for (int i = 0; i < submatrixList.Count; i++)
                submatrixList[i] = (Matrix)(this.dctMatrix.Transpose * (RealMatrix)submatrixList[i] * this.dctMatrix);

            return Matrix.FromList(submatrixList);
        }

        public RealMatrix Generate()
        {
            var matrix = new RealMatrix(8, 8);

            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    if (i == 0)
                        matrix[i, j] = 1 / Math.Sqrt(8);
                    else
                        matrix[i, j] = 0.5 * Math.Cos((2 * j + 1) * i * 3.14 / 16);

            return matrix;
        }
    }
}
