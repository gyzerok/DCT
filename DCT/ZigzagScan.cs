using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT
{
    class ZigzagScan<T>
    {
        private enum Direction { Right, LeftDown, Down, RightUp }
        private enum InverseDirection { Left, RightUp, Up, LeftDown }

        /// <summary>
        /// Матрица зигзаг сканирования
        /// </summary>
        private T[,] elems;        

        public ZigzagScan()
        {
            elems = new T[8, 8];
        }

        /// <summary>
        /// Создаёт клон матрицы
        /// </summary>
        /// <param name="matrix">Матрица для клонирования</param>
        public ZigzagScan(T[,] matrix)
        {
            elems = (T[,])(matrix.Clone());
        }

        public ZigzagScan(List<T> list)
        {
            GetFromList(list);
        }

        /// <summary>
        /// Получает или задаёт элемент матрицы
        /// </summary>
        /// <returns>Элемент матрицы</returns>
        public T this[int x, int y]
        {
            get
            {
                return elems[x, y];
            }

            set
            {
                elems[x, y] = value;
            }
        }        

        /// <summary>
        /// Проводит зигзаг сканирование
        /// </summary>
        /// <returns>Список квантованных элементов</returns>
        public List<T> Scan()
        {
            List<T> result = new List<T>();            

            int x, y;
            Direction direction;
            x = y = 0;
            direction = 0;
            result.Add(elems[x, y]);
            while ( ! (x == 7 && y == 7))
            {
                switch (direction)
                {
                    case Direction.Right:
                        if (x < 7)
                        {
                            x++;
                        }
                        else
                        {
                            y++;
                        }                        
                        direction = Direction.LeftDown;
                        result.Add(elems[x, y]);
                        break;
                    case Direction.Down:
                        if (y < 7)
                        {
                            y++;
                        }
                        else
                        {
                            x++;
                        }
                        direction = Direction.RightUp;
                        result.Add(elems[x, y]);
                        break;
                    case Direction.LeftDown:
                        while (x > 0 && y < 7)
                        {
                            x--;
                            y++;
                            result.Add(elems[x, y]);
                        }
                        direction = Direction.Down;
                        break;                                        
                    case Direction.RightUp:
                        while (x < 7 && y > 0)
                        {
                            x++;
                            y--;
                            result.Add(elems[x, y]);
                        }
                        direction = Direction.Right;
                        break;
                }
            }
            return result;
        }

        private void GetFromList(List<T> list)
        {
            elems = new T[8, 8];
            int x, y;
            InverseDirection direction;
            x = y = 7;
            direction = InverseDirection.Left;

            elems[x, y] = list[list.Count-1];
            list.RemoveAt(list.Count - 1);
            while (!(x == 0 && y == 0))
            {
                switch (direction)
                {
                    case InverseDirection.Left:
                        if (x > 0)
                        {
                            x--;
                        }
                        else
                        {
                            y--;
                        }
                        direction = InverseDirection.RightUp;
                        elems[x, y] = list[list.Count-1];
                        list.RemoveAt(list.Count - 1);
                        break;
                    case InverseDirection.Up:
                        if (y > 0)
                        {
                            y--;
                        }
                        else
                        {
                            x--;
                        }
                        direction = InverseDirection.LeftDown;
                        elems[x, y] = list[list.Count-1];
                        list.RemoveAt(list.Count - 1);
                        break;
                    case InverseDirection.RightUp:
                        while (x < 7 && y > 0)
                        {
                            x++;
                            y--;
                            elems[x, y] = list[list.Count - 1];
                            list.RemoveAt(list.Count - 1);
                        }
                        direction = InverseDirection.Up;
                        break;
                    case InverseDirection.LeftDown:
                        while (x > 0 && y < 7)
                        {
                            x--;
                            y++;
                            elems[x, y] = list[list.Count - 1];
                            list.RemoveAt(list.Count - 1);
                        }
                        direction = InverseDirection.Left;
                        break;
                }
            }
        }
    }
}
