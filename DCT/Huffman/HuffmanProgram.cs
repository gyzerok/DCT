using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hca
{
    public class HuffmanProgram
    {
        public static string Source { get; set; }

        public static void Execute(params string[] args)
        {            
            string key = args[0];
            string fromFile = args[1];
            string toFile = args[2];
            DateTime start = DateTime.Now;
            if (key == "-e")
            {
                HuffmanEncoding he = new HuffmanEncoding(fromFile, toFile);
                he.Encode();
            }
            else if (key == "-d")
            {
                HuffmanDecoding hd = new HuffmanDecoding(fromFile, toFile);
                hd.Decode();
            }
            DateTime finish = DateTime.Now;

            FileStream from = new FileStream(fromFile, FileMode.Open);
            long fromSize = from.Length;
            from.Close();
            FileStream to = new FileStream(toFile, FileMode.Open);
            long toSise = to.Length;            
            to.Close();

            Console.WriteLine();
            Console.WriteLine("Source size:\t" + FormatSize(fromSize));
            Console.WriteLine("New size:\t" + FormatSize(toSise));
            Console.WriteLine("Difference:\t" + FormatSize(toSise - fromSize));
            Console.WriteLine("Diff in %:\t" + (((double)toSise - fromSize) / fromSize).ToString("P"));            
            Console.WriteLine("Done in:\t" + (finish - start).ToString());

            File.Copy(Source, Source + '1');
            from = new FileStream(Source+'1', FileMode.Open);            
            fromSize = from.Length;
            from.Close();
            File.Delete(Source + '1');

            System.Windows.Forms.MessageBox.Show("Source size:\t" + FormatSize(fromSize) +
                                                 "\nNew size:\t\t" + FormatSize(toSise) + 
                                                 "\nDifference:\t" + FormatSize(toSise - fromSize) +
                                                 "\nDiff in %:\t\t" + (((double)toSise - fromSize) / fromSize).ToString("P") +
                                                 "\nDone in:\t\t" + (finish - start).ToString());

            Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }

        public static void PrintLn(params string[] strs)
        {
            /*
            string result = "";
            foreach (string str in strs)
            {
                result += str;
            }
            for (int i = result.Length; i < Console.WindowWidth - 1; i++)
            {
                result += " ";
            }
            Console.CursorLeft = 0;
            Console.Write(result);*/
        }

        public static string FormatSize(long size)
        {
            
            double dsize = size;
            string[] sizes = { "Bytes",
                               "KB",
                               "MB",
                               "GB"};
            int i = 0;
            while (i < sizes.Length & Math.Abs(dsize) > 512)
            {
                dsize /= 1024;
                i++;
            }
            string ssize = String.Format("{0:0.##}", dsize);
            return ssize + " " + sizes[i];
        }    
    }
}
