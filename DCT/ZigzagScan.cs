using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT
{
    class ZigzagScan
    {
        private enum Direction { Up, Down }        

        public static List<int> Scan(Matrix QuantizedMatrix)
        {
            List<int> result = new List<int>();
            
            int col, row;
            col = row = 0;
            Direction direction = Direction.Up;

            switch (direction)
            {
                case Direction.Up:
                    if (row == 0)
                    {
                        col++;
                        direction = Direction.Down;                        
                    }
                    else if (col == 7)
                    {
                        row++;
                        direction = Direction.Down;

                    }
                    else
                    {
                        col++;
                        row--;
                    }
                    break;

                case Direction.Down:
                    if (row == 7)
                    {
                        col++;
                        direction = Direction.Up;
                    }
                    else if (col == 0)
                    {
                        row++;
                        direction = Direction.Up;
                    }
                    else
                    {
                        col--;
                        row++;
                    }
                    break;
                default:
                    throw new System.Exception("Шёл нах уёба! Так не должно быть!");
            }

            return result;
        }

        public static Matrix ReverseScan()
        {
            Matrix result = new Matrix(8, 8);

            return result;
        }
    }
}
