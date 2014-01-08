using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace hca
{
    public class HuffmanEncoding
    {
        string fromFile;
        string toFile;
        const int ekb = 8192;

        double[] probs;
        Node root;

        BitArray[] table;
        Node[] nodes;

        public HuffmanEncoding(string fromFile, string toFile)
        {
            this.fromFile = fromFile;
            this.toFile = toFile;

            probs = new double[256];
            table = new BitArray[256];
            nodes = new Node[256];
        }

        public void Encode()
        {
            CalcProbs();
            CodingCommon.CreateTreeAndTable(ref nodes, ref table, out root);
            DoEncode();
        }

        private void CalcProbs()
        {
            Console.WriteLine("Calculating probabilites...");
            FileStream fs = new FileStream(fromFile, FileMode.Open);
            long fsLength = fs.Length;
            int step = (int)(fsLength / 1000);
            for (int i = 0; i < fsLength; i++)
            {
                if (step > 0 && i % step == 0)
                    HuffmanProgram.PrintLn(((double)i / fsLength).ToString("P"));
                probs[fs.ReadByte()]++;
            }
            HuffmanProgram.PrintLn("100%");
            Console.WriteLine();
            for (int i = 0; i < 256; i++)
            {
                probs[i] /= fsLength;
                probs[i] = ((double)((ushort)(probs[i] * 1000000))) / 1000000;
                nodes[i] = new Node(probs[i], i);
            }
            fs.Close();
        }        

        private void DoEncode()
        {
            Console.WriteLine("Encoding...");
            // table
            FileStream sr = new FileStream(fromFile, FileMode.Open);
            long srLength = sr.Length;
            FileStream ws = new FileStream(toFile, FileMode.Create);
            ushort[] shortProbs = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                shortProbs[i] = (ushort)(probs[i] * 1000000);
            }
            byte[] buf = new byte[512];
            for (int i = 0; i < 256; i++)
            {
                int index = i*2;
                buf[index] = (byte)(shortProbs[i] >> 8);
                buf[index + 1] = (byte)(shortProbs[i] & 0x00FF);
            }
            ws.Write(buf, 0, 512);
            // body
            int step = (int)(srLength / 1000);

            BitArray bits, buffer;                  
            int pos = 0;
            //buffer = new BitArray(0);
            buffer = new BitArray((int)srLength * 8); // new 
            byte[] file = new byte[srLength];
            sr.Read(file, 0, (int)srLength);
            for (long i = 0; i < srLength; i++)
            {
                if (step > 0 && i % step == 0)
                    HuffmanProgram.PrintLn(((double)i / srLength).ToString("P"));
                bits = table[file[i]];
                //pos = buffer.Length;                
                //buffer.Length += bits.Length;                
                for (int j = 0; j < bits.Length; j++)
                {
                    buffer[pos + j] = bits[j];                    
                }
                pos += bits.Length; // new 
            }
            buffer.Length = pos; // new 
            HuffmanProgram.PrintLn("100%");
            Console.WriteLine();
            byte insignificant = (byte)(8 - (buffer.Length % 8));
            if (0 < insignificant && insignificant < 8)
            {
                buffer.Length += insignificant;                
            }
            buf = new byte[buffer.Length / 8];
            buffer.CopyTo(buf, 0);
            ws.Write(buf, 0, buf.Length);
            ws.WriteByte(insignificant);
            ws.Close();
            sr.Close();
        }     
    }
}
