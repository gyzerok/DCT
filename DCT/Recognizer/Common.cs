using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer
{
    class Common
    {
        public static bool InRange(int x, int y, int range)
        {
            if (Math.Abs(x - y) <= range)
                return true;
            return false;
        }

        public static bool InRange(ColorPoint pointA, ColorPoint pointB, int range)
        {
            double distance = Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2));
            if (distance <= range)
                return true;
            return false;
        }
    }
}
