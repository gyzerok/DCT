using System;

namespace DCT
{
    public class Quantizer
    {
        private Matrix qualityMatrix = new Matrix(8,8);
        private int quality;

        public Quantizer(int quality)
        {
            this.quality = quality;
            this.MakeQualityMatrix();
        }

        /// <summary>
        /// Quantity matrix forming. Using this.quality as a parameter
        /// </summary>
        private void MakeQualityMatrix()
        {
            for (var i = 0; i < qualityMatrix.Rows; i++)
                for (var j = 0; j < qualityMatrix.Cols; j++)
                    qualityMatrix[i, j] = 1 + ((1 + i + j) * this.quality);
        }

        /// <summary>
        /// Make matrix quantization. Matrix parameter refers to Pdct matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public Matrix Quantize(Matrix matrix)
        {
            var ret = new Matrix(8, 8);

            for (var i = 0; i < matrix.Rows; i++)
                for (var j = 0; j < matrix.Cols; j++)
                    ret[i, j] = (int)Math.Round(matrix[i, j] / this.qualityMatrix[i, j] * 1.0);
                
            return ret;
        }

        public Matrix Dequantize(Matrix matrix)
        {
            var ret = new Matrix(8, 8);

            for (var i = 0; i < matrix.Rows; i++)
                for (var j = 0; j < matrix.Cols; j++)
                    ret[i, j] = matrix[i, j] * this.qualityMatrix[i, j];
            
            return ret;
        }
    }
}
