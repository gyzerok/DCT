using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer
{
    class CircleRecognizer
    {
        public static bool Recognize(Figure fig, int accuracyRange)
        {
            if (!Common.InRange(fig.Height, fig.Width, accuracyRange))
                return false;

            int radius = fig.Width / 2;

            foreach (ColorPoint point in fig.Contour)
            {
                double distance = Math.Sqrt(Math.Pow(point.X - fig.CenterX, 2) + Math.Pow(point.Y - fig.CenterY, 2));
                if (!Common.InRange((int)Math.Round(distance), radius, accuracyRange))
                    return false;
            }

            return true;
        }        
    }
}
