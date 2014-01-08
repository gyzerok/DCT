using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Recognizer
{
    class RectangleRecognizer
    {
        public static bool Recognize(Figure fig, int accuracyRange)
        {            
            List<ColorPoint> Left = new List<ColorPoint>();
            List<ColorPoint> Right = new List<ColorPoint>();
            List<ColorPoint> Top = new List<ColorPoint>();
            List<ColorPoint> Bottom = new List<ColorPoint>();
            foreach (ColorPoint point in fig.Points)
            {
                if (point.X == fig.MinX)
                    Left.Add(point.Clone());
                if (point.X == fig.MaxX)
                    Right.Add(point.Clone());
                if (point.Y == fig.MinY)
                    Top.Add(point.Clone());
                if (point.Y == fig.MaxY)
                    Bottom.Add(point.Clone());
            }
            ColorPoint A = new ColorPoint(Left[0].X, Left.Min(point => point.Y), Color.Black);
            ColorPoint B = new ColorPoint(Top.Max(point => point.X), Top[0].Y, Color.Black);
            ColorPoint C = new ColorPoint(Right[0].X, Right.Max(point => point.Y), Color.Black);
            ColorPoint D = new ColorPoint(Bottom.Min(point => point.X), Bottom[0].Y, Color.Black);

            Line AB = new Line(A.X, A.Y, B.X, B.Y);
            Line BC = new Line(B.X, B.Y, C.X, C.Y);
            Line CD = new Line(C.X, C.Y, D.X, D.Y);
            Line DA = new Line(D.X, D.Y, A.X, A.Y);

            if (!(Line.IsOrthogonal(AB, BC) && 
                  Line.IsOrthogonal(BC, CD) && 
                  Line.IsOrthogonal(CD, DA)))
                return false;

            foreach (ColorPoint point in fig.Contour)
            {
                if (!Common.InRange((int)Math.Round(AB.Distance(point.X, point.Y)), 0, accuracyRange) &&
                    !Common.InRange((int)Math.Round(BC.Distance(point.X, point.Y)), 0, accuracyRange) &&
                    !Common.InRange((int)Math.Round(CD.Distance(point.X, point.Y)), 0, accuracyRange) &&
                    !Common.InRange((int)Math.Round(DA.Distance(point.X, point.Y)), 0, accuracyRange))
                    return false;
            }

            return true;
        }
    }
}
