using System;
using System.Collections.Generic;
using System.IO;

namespace DCT
{
    public class Writer
    {
        private static string filePath = "recorded.jf";

        public static void Write(int quality, List<List<List<Tuple<int, int>>>> matrices)
        {
            var binWriter = new BinaryWriter(File.Open(filePath, FileMode.Create));
            binWriter.Write("q" + quality + "/q");

            foreach (var component in matrices)
            {
                foreach (var matrix in component)
                {
                    foreach (var tuple in matrix)
                    {
                        binWriter.Write(tuple.Item1);
                        binWriter.Write(tuple.Item2);
                    }
                }
            }
        }
    }
}