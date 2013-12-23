using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT
{
    class ZigzagScan<T>
    {
        /// <summary>
        /// Матрица зигзаг сканирования
        /// </summary>
        private T[,] elems;        

        /// <summary>
        /// Создаёт клон матрицы
        /// </summary>
        /// <param name="matrix">Матрица для клонирования</param>
        public ZigzagScan(T[,] matrix)
        {
            elems = (T[,])(matrix.Clone());
        }

        /// <summary>
        /// Создаёт квадратную матрицу
        /// </summary>
        /// <param name="size">Размер матрицы</param>
        public ZigzagScan(int size)
        {
            Initialize(size, size);
        }

        /// <summary>
        /// Создаёт прямоугольную матрицу
        /// </summary>
        /// <param name="x">Ширина матрицы</param>
        /// <param name="y">Высота матрицы</param>
        public ZigzagScan(int x, int y)
        {
            Initialize(x, y);            
        }

        /// <summary>
        /// Инициализирует матрицу
        /// </summary>
        /// <param name="x">Ширина</param>
        /// <param name="y">Высота</param>
        private void Initialize(int x, int y)
        {
            elems = new T[x, y];
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
        /// Ширина матрицы
        /// </summary>
        public int Width
        {
            get
            {
                return elems.GetLength(0);
            }
        }

        /// <summary>
        /// Высота матрицы
        /// </summary>
        public int Height
        {
            get
            {
                return elems.GetLength(1);
            }
        }
    }
}
