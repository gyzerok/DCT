using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Recognizer
{
    class ColorPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public ColorPoint(int X, int Y, Color Color)
        {
            this.X = X;
            this.Y = Y;
            this.Color = Color;
        }

        public ColorPoint Clone()
        {
            return new ColorPoint(this.X, this.Y, this.Color);
        }
    }
}
