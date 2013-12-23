using System;

namespace DCT
{
    class Quantizer
    {
        private int[,] qualityMatrix = new int[8,8];
        private int quality;

        public Quantizer(int quality)
        {
            this.quality = quality;
            this.MakeQualityMatrix();
        }

        private void MakeQualityMatrix()
        {
            for (var i = 0; i < qualityMatrix.GetLength(1); i++)
            {
                for (var j = 0; j < qualityMatrix.GetLength(0); j++)
                {
                    qualityMatrix[i, j] = 1 + ((1 + i + j)*this.quality);
                }
            }
        }

        public int[,] MakeQuantization(int[,] matrix)
        {
            var resultMatrix = new int[8,8];

            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                for (var j = 0; j < matrix.GetLength(0); j++)
                {
                    var temp = (int)Math.Round((double)matrix[i, j]/(double)qualityMatrix[i, j]);
                    resultMatrix[i,j] = temp;
                }
            }
            return this.qualityMatrix;
        }
    }
}
