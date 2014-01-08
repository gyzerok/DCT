using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using hca;

namespace DCT
{
    public class ImgIO
    {
        public static void Write(string fileName, List<List<List<int>>> data)
        {
            StreamWriter sw = new StreamWriter("auxfilename", false);
            Serialize(data, sw);
            sw.Close();
            HuffmanProgram.Execute("-e", "auxfilename", fileName);
            File.Delete("auxfilename");
        }

        public static List<List<List<int>>> Read(string fileName)
        {
            HuffmanProgram.Execute("-d", fileName, "auxfilename");
            fileName = "auxfilename";
            StreamReader sr = new StreamReader(fileName);
            List<List<List<int>>> result = Deserialize(sr);
            sr.Close();
            File.Delete(fileName);
            return result;
        }

        private static void Serialize(List<List<List<int>>> data, StreamWriter sw)
        {
            sw.WriteLine(data.Count);
            for (int i = 0; i < data.Count; i++)
                Serialize(data[i], sw);
        }

        private static List<List<List<int>>> Deserialize(StreamReader sr)
        {
            List<List<List<int>>> result = new List<List<List<int>>>();
            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
                result.Add(Deserialize2(sr));
            return result;
        }

        private static void Serialize(List<List<int>> data, StreamWriter sw)
        {
            sw.WriteLine(data.Count);
            for (int i = 0; i < data.Count; i++)
                Serialize(data[i], sw);
        }

        private static List<List<int>> Deserialize2(StreamReader sr)
        {
            List<List<int>> result = new List<List<int>>();
            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
                result.Add(Deserialize3(sr));
            return result;
        }

        private static void Serialize(List<int> data, StreamWriter sw)
        {
            List<int> compressed = new List<int>();
            compressed.Add(data[0]);
            compressed.Add(1);
            data.RemoveAt(0);
            while (data.Count > 0)
            {
                if (data[0] == compressed[compressed.Count - 2])
                {
                    compressed[compressed.Count - 1]++;
                    data.RemoveAt(0);
                }
                else
                {
                    compressed.Add(data[0]);
                    compressed.Add(1);
                    data.RemoveAt(0);
                }
            }
            sw.WriteLine(compressed.Count);
            for (int i = 0; i < compressed.Count; i++)
            {
                sw.WriteLine(compressed[i]);
            }
        }

        private static List<int> Deserialize3(StreamReader sr)
        {
            List<int> result = new List<int>();
            List<int> compressed = new List<int>();
            int count = int.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
            {
                compressed.Add(int.Parse(sr.ReadLine()));
            }
            for (int i = 0; i < compressed.Count; i++)
            {
                for (int j = 0; j < compressed[i+1]; j++)
                    result.Add(compressed[i]);
                i++;
            }
            return result;
        }
    }
}
