using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab21
{
    class Program
    {
        /*ЗАДАНИЕ 21. МНОГОПОТОЧНОСТЬ. КЛАСС THREAD
            Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать.
            Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом.
            Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз.
            Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево.
            Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно.
            Создать многопоточное приложение, моделирующее работу садовников.
            */
        static int[,] field;
        static int width;
        static int height;
        static object locker = new object();

        static void Main()
        {
            Console.WriteLine("Введите размеры поля для сада:");
            Console.WriteLine("Введите размер поля по вертикали:");
            height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите размер поля по горизонтали:");
            width = Convert.ToInt32(Console.ReadLine());

            field = new int[height, width];

            ThreadStart threadStart = new ThreadStart(garden1);
            Thread thread = new Thread(threadStart);
            thread.Start();
            garden2();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        private static void garden1()
        {
            lock (locker)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (field[i, j] == 0)
                            field[i, j] = 1;
                        Thread.Sleep(1);
                    }
                }
            }
        }
        private static void garden2()
        {
            for (int i = width - 1; i >= 0; i--)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    if (field[j, i] == 0)
                        field[j, i] = 2;
                    Thread.Sleep(1);
                }
            }
        }
    }
}
