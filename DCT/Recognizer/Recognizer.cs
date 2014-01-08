using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Recognizer
{
    class Recognizer
    {
        private Bitmap img;

        public int AccuracyRange { get; set; }
        public double OrthogonalAccuracy { get { return Line.OrthogonalAccuracy; } set { Line.OrthogonalAccuracy = value; } }

        public List<Figure> Unknown { get; private set; }
        public List<Figure> Circles { get; private set; }
        public List<Figure> Rectangles { get; private set; }

        public Recognizer(Bitmap img)
        {
            this.AccuracyRange = 3;
            this.OrthogonalAccuracy = 0.1;
            this.img = img;            
        }

        public void Recognize()
        {
            this.Unknown = GetAllFigures();
            this.Circles = GetCircles();
            this.Rectangles = GetRectangles();
        }

        private List<Figure> GetAllFigures()
        {
            List<Figure> result = new List<Figure>();
            for (int i = 0; i < img.Width; i++)
                for (int j = 0; j < img.Height; j++)
                {
                    if (img.GetPixel(i, j).R == 0)
                    {
                        Figure fig = new Figure(img, i, j);
                        result.Add(fig);
                    }
                }
            return result;
        }

        private List<Figure> GetCircles()
        {
            List<Figure> result = new List<Figure>();
            for (int i = 0; i < this.Unknown.Count; i++)
            {
                if (CircleRecognizer.Recognize(this.Unknown[i], this.AccuracyRange))
                {
                    result.Add(this.Unknown[i]);
                    this.Unknown.RemoveAt(i);
                }
            }
            return result;
        }

        private List<Figure> GetRectangles()
        {
            List<Figure> result = new List<Figure>();
            for (int i = 0; i < this.Unknown.Count; i++)
            {
                if (RectangleRecognizer.Recognize(this.Unknown[i], this.AccuracyRange))
                {
                    result.Add(this.Unknown[i]);
                    this.Unknown.RemoveAt(i);
                }
            }
            return result;
        }
    }
}
