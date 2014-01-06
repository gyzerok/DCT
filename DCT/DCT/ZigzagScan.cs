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

        public static List<int> Scan(Matrix quantizedMatrix)
        {
            List<int> result = new List<int>();
            
            int col, row;
            col = row = 0;
            Direction direction = Direction.Up;            
            while (col <= 7 && row <= 7)
            {
                result.Add(quantizedMatrix[row, col]);
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
                        throw new System.Exception("Шёл отсюда ущербный! Так не должно быть!");
                }                
            }
            return result;
        }

        public static Matrix ReverseScan(List<int> vector)
        {
            Matrix result = new Matrix(8, 8);

            if (vector.Count != 64)
                throw new System.Exception("Ужас! Ты че творишь?! Тут 64 элемента должно быть!!!!!1111");

            int col, row, index;
            col = row = index = 0;
            Direction direction = Direction.Up;
            while (col <= 7 && row <= 7)
            {
                result[row, col] = vector[index++];
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
                        throw new System.Exception("Шёл отсюда ущербный! Так не должно быть!");
                }
            }
            return result;
        }
    }
}
