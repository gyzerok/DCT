using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Recognizer
{
    class Figure
    {
        private ColorPoint[,] partOfImage;
        private List<ColorPoint> contour; 
        private int width, height;
        public int Width { get { return width - 2; } }
        public int Height { get { return height - 2; } }
        public int Top { get; private set; }
        public int Left { get; private set; }
        public int CenterX { get; private set; }
        public int CenterY { get; private set; }
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public List<ColorPoint> Points { get; private set; }
        public List<ColorPoint> Contour
        {
            get
            {
                List<ColorPoint> result = new List<ColorPoint>();
                foreach (ColorPoint point in contour)
                    result.Add(point.Clone());
                return result;
            }
        }

        public Figure(Bitmap img, int x, int y)
        {
            this.partOfImage = GetPartOfImage(img, x, y);            
            this.contour = GetCountour();
            this.Points = GetPoints(this.partOfImage);
        }

        private List<ColorPoint> GetPoints(ColorPoint[,] partOfImage)
        {
            List<ColorPoint> result = new List<ColorPoint>();

            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (partOfImage[i, j].Color.R == 0)
                    {
                        result.Add(partOfImage[i, j].Clone());
                    }
                }
            }

            return result;
        }

        private ColorPoint[,] GetPartOfImage(Bitmap img, int x, int y)
        {
            List<ColorPoint> points = GetPoints(ref img, x, y);
            List<ColorPoint> figure = new List<ColorPoint>();
            figure = points;
            //foreach (ColorPoint point in points)
            //{
            //    bool exist = false;
            //    for (int i = 0; i < figure.Count; i++)
            //    {
            //        if (figure[i].X == point.X && figure[i].Y == point.Y)
            //        {
            //            exist = true;
            //            break;
            //        }
            //    }
            //    if (!exist)
            //        figure.Add(point);
            //}

            int MinX, MinY, MaxX, MaxY;
            MinY = MinX = int.MaxValue;
            MaxY = MaxX = int.MinValue;
            for (int i = 0; i < figure.Count; i++)
            {
                MinX = Math.Min(figure[i].X, MinX);
                MaxX = Math.Max(figure[i].X, MaxX);
                MinY = Math.Min(figure[i].Y, MinY);
                MaxY = Math.Max(figure[i].Y, MaxY);
            }
            this.MinX = MinX;
            this.MinY = MinY;
            this.MaxX = MaxX;
            this.MaxY = MaxY;
            MinX--;
            MinY--;
            MaxX++;
            MaxY++; 
            this.width = MaxX - MinX;
            this.height = MaxY - MinY;
            this.CenterX = (MaxX - MinX) / 2 + MinX;
            this.CenterY = (MaxY - MinY) / 2 + MinY;
            ColorPoint[,] imgPart = new ColorPoint[this.width, this.height];
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    imgPart[i, j] = new ColorPoint(i, j, Color.White);
                }
            }

            foreach (ColorPoint point in figure)
            {
                imgPart[point.X - MinX, point.Y - MinY] = point.Clone();
            }

            return imgPart;
        }

        private List<ColorPoint> GetPoints(ref Bitmap img, int x, int y)
        {
            List<ColorPoint> result = new List<ColorPoint>();
            List<ColorPoint> unwatched = new List<ColorPoint>();

            unwatched.Add(new ColorPoint(x, y, Color.Black));
            img.SetPixel(x, y, Color.White);

            while (unwatched.Count > 0)
            {
                ColorPoint cur = unwatched[0];
                result.Add(cur.Clone());
                unwatched.RemoveAt(0);                     
                // Left
                if (img.GetPixel(cur.X - 1, cur.Y).R < 128)
                {
                    unwatched.Add(new ColorPoint(cur.X - 1, cur.Y, Color.Black));
                    img.SetPixel(cur.X - 1, cur.Y, Color.White);
                }
                // right
                if (img.GetPixel(cur.X + 1, cur.Y).R < 128)
                {
                    unwatched.Add(new ColorPoint(cur.X + 1, cur.Y, Color.Black));
                    img.SetPixel(cur.X + 1, cur.Y, Color.White);
                }
                // top
                if (img.GetPixel(cur.X, cur.Y - 1).R < 128)
                {
                    unwatched.Add(new ColorPoint(cur.X, cur.Y - 1, Color.Black));
                    img.SetPixel(cur.X, cur.Y - 1, Color.White);
                }
                // bottom
                if (img.GetPixel(cur.X, cur.Y + 1).R < 128)
                {
                    unwatched.Add(new ColorPoint(cur.X, cur.Y + 1, Color.Black));
                    img.SetPixel(cur.X, cur.Y + 1, Color.White);
                }
            }
            return result;
        }

        private List<ColorPoint> GetCountour()
        {
            List<ColorPoint> result = new List<ColorPoint>();

            for (int i = 1; i < this.width-1; i++)
            {
                for (int j = 1; j < this.height-1; j++)
                {
                    if (partOfImage[i, j].Color.R == 0)
                    {
                        if (partOfImage[i - 1, j].Color == Color.White ||
                            partOfImage[i + 1, j].Color == Color.White ||
                            partOfImage[i, j - 1].Color == Color.White ||
                            partOfImage[i, j + 1].Color == Color.White)
                        {
                            result.Add(partOfImage[i, j].Clone());
                        }
                    }
                }
            }

            return result;
        }
    }
}
