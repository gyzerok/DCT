using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace hca
{
    public class CodingCommon
    {
        public static void CreateTreeAndTable(ref Node[] nodes, ref BitArray[] table, out Node root)
        {
            List<Node> queue = new List<Node>();
            for (int i = 0; i < 256; i++)
            {
                queue.Add(nodes[i]);
            }
            SortQueue(queue);
            while (queue.Count > 1)
            {
                Node x = queue[0];
                Node y = queue[1];
                queue.RemoveRange(0, 2);
                queue.Add(new Node(x, y));
                SortQueue(queue);
            }
            root = queue[0];
            for (int i = 0; i < 256; i++)
            {
                table[i] = GetNodeByValue(i, nodes);
            }
        }

        public static BitArray GetNodeByValue(int value, Node[] nodes)
        {
            int i = 0;
            for (i = 0; i < 256 && nodes[i].Value != value; i++) ;
            Node node = nodes[i];
            BitArray bits = new BitArray(0);
            while (node.Parent != null)
            {
                bits.Length++;
                bits[bits.Length - 1] = node.BoolBit;
                node = node.Parent;
            }
            BitArray result = new BitArray(bits.Length);
            for (i = 0; i < result.Length; i++)
            {
                result[i] = bits[bits.Length - i - 1];
            }
            return result;
        }

        public static void SortQueue(List<Node> queue)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                for (int j = 1; j < queue.Count - i; j++)
                {
                    if (queue[j - 1].Probability > queue[j].Probability)
                    {
                        Node aux = queue[j - 1];
                        queue[j - 1] = queue[j];
                        queue[j] = aux;
                    }
                }
            }
        } 
    }
}
