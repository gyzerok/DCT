using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer
{
    class Line
    {
        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }
        private double A, B, C;
        public static double OrthogonalAccuracy { get; set; }

        public Line(int x1, int y1, int x2, int y2)        
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;

            if (y1 == y2 && x1 == x2)
                throw new Exception("Оба коэффициента равны нулю!");
            A = y1 - y2;
            B = x2 - x1;
            C = x1 * y2 - x2 * y1;
        }

        public double Distance(int x, int y)
        {
            return (A * x + B * y + C) / (Math.Sign(C) * Math.Sqrt(A * A + B * B));
        }

        public static bool IsOrthogonal(Line line1, Line line2)
        {
            double corner = Math.Abs(Math.Atan((line1.A * line2.B - line2.A * line1.B) / (line1.A * line2.A + line1.B * line2.B)) * 180 / Math.PI);            
            //if (Math.Abs(line1.A * line2.A + line1.B * line2.B) <= OrthogonalAccuracy)
            if (Math.Abs(corner - 90) <= OrthogonalAccuracy) 
                return true;            
            return false;
        }
    }
}
