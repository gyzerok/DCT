using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace hca
{
    public class HuffmanDecoding
    {
        private string fromFile;
        private string toFile;

        private Node root;
        private BitArray[] table;
        private Node[] nodes;

        public HuffmanDecoding(string fromFile, string toFile)
        {
            this.fromFile = fromFile;
            this.toFile = toFile;
            table = new BitArray[256];            
        }

        public void Decode()
        {
            GetNodes();
            CodingCommon.CreateTreeAndTable(ref nodes, ref table, out root);
            DoDecode();
        }

        private void GetNodes()
        {
            nodes = new Node[256];
            double[] probs = new double[256];
            FileStream fs = new FileStream(fromFile, FileMode.Open);
            byte[] buf = new byte[512];
            fs.Read(buf, 0, 512);
            ushort val;            
            for (int i = 0; i < 256; i++)
            {                
                val = buf[2 * i];
                val <<= 8;
                val |= buf[2 * i + 1];
                probs[i] = val / 1000000d;
                nodes[i] = new Node(probs[i], i);
            }
            fs.Close();
        }

        private void DoDecode()
        {
            FileStream sr = new FileStream(fromFile, FileMode.Open);
            FileStream sw = new FileStream(toFile, FileMode.Create);
            sr.Position = 512;
            byte[] byteBuffer = new byte[sr.Length - 512];
            sr.Read(byteBuffer, 0, (int)sr.Length - 512);
            BitArray bitBuf = new BitArray(byteBuffer);            
            BitArray insignificantBits = new BitArray(8);
            int i;
            for (i = 0; i < 8; i++)
            {
                insignificantBits[i] = bitBuf[bitBuf.Length - 8 + i];
            }
            byte[] insignificant = new byte[1];
            insignificantBits.CopyTo(insignificant, 0);
            bitBuf.Length -= 8;
            bitBuf.Length -= insignificant[0];
            Node node = root;            
            i = 0;
            while (i < bitBuf.Length)
            {
                if (bitBuf[i])
                {
                    node = node.Right;
                }
                else
                {
                    node = node.Left;
                }
                i++;
                // leaf
                if (node.Left == null && node.Right == null)
                {
                    sw.WriteByte((byte)node.Value);
                    // to root
                    node = root;
                }
            }
            sr.Close();
            sw.Close();
        }
    }
}
