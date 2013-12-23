using System;
using System.Collections.Generic;

namespace DCT
{
    public class Compressor
    {
        /// <summary>
        /// Static method that compresses data using zero coding
        /// </summary>
        /// <param name="toCompressList"></param>
        /// <returns></returns>
        public static List<Tuple<int, int>> MakeCompression(List<int> toCompressList)
        {
            var result = new List<Tuple<int, int>>();
            var zeroCounter = 0;

            foreach (var value in toCompressList)
            {
                if (value == 0)
                {
                    zeroCounter++;
                }
                else
                {
                    result.Add(new Tuple<int, int>(zeroCounter, value));
                    zeroCounter = 0;
                }
            }

            return result;
        }


        /// <summary>
        /// Static method that decompresses data using zero coding
        /// </summary>
        /// <param name="toDecompressList"></param>
        /// <returns></returns>
        public static List<int> MakeDecompression(List<Tuple<int, int>> toDecompressList)
        {
            var result = new List<int>();

            foreach (var value in toDecompressList)
            {
                for (var i = 0; i<value.Item1; i++)
                {
                    result.Add(0);
                }
                
                result.Add(value.Item2);
            }

            return result;
        }
    }
}
