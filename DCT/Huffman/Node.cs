using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hca
{
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public double Probability { get; set; }
        public int Bit { get; set; }
        public bool BoolBit { get { return (Bit == 0) ? false : true; } set { Bit = (value) ? 1 : 0; } }
        public int Value { get; set; }       

        public Node() { }

        public Node(double prob, int Value, int bit = 0)
        {
            Probability = prob;
            this.Value = Value;
            Bit = bit;
        }       

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
            Left.Parent = this;
            Right.Parent = this;
            Probability = left.Probability + right.Probability;
            Left.Bit = 0;
            Right.Bit = 1;
        }

        public Node Clone()
        {
            Node aux = new Node();
            aux.Left = Left;
            aux.Right = Right;
            aux.Parent = Parent;
            aux.Probability = Probability;
            aux.Bit = Bit;
            aux.Value = Value;
            return aux;
        }
    }
}
