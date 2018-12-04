using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ParameterizedThread
    {
        public static void Display()
        {
            int number = 4;
            // создаем новый поток
            Thread myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(number);

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"Главный поток:\t{i * i}");
                Thread.Sleep(300);
            }
        }

        static void Count(object x)
        {
            for (int i = 1; i < 9; i++)
            {
                int n = (int)x;

                Console.WriteLine($"\tВторой поток:\t{i * n}");
                Thread.Sleep(400);
            }
        }
    }

    class ParameterizedWithArray
    {
        public static void Display()
        {
            var arr = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900 };

            Thread myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"Главный поток:\t{arr[i] - i * i}");
                Thread.Sleep(300);
            }
        }

        static void Count(object arrObj)
        {
            var arr = (int[])arrObj;

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"\tВторой поток:\t{i * i + arr[i]}");
                Thread.Sleep(400);
            }
        }
    }

    class ParameterizedWithClass
    {
        public static void Display()
        {
            Counter counter = new Counter();
            counter.x = 4;
            counter.y = 5;

            Thread myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(counter);

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"Главный поток:\t{i * i}");
                Thread.Sleep(300);
            }
        }

        static void Count(object obj)
        {
            for (int i = 1; i < 9; i++)
            {
                Counter c = (Counter)obj;

                Console.WriteLine($"\tВторой поток:\t{i * c.x * c.y}");
                Thread.Sleep(400);
            }
        }

        public class Counter
        {
            public int x;
            public int y;
        }
    }

    class ParameterizedTypeSafe
    {
        public static void Display()
        {
            Counter counter = new Counter(5, 4);

            Thread myThread = new Thread(new ThreadStart(counter.Count));
            myThread.Start();

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"Главный поток:\t{i * i}");
                Thread.Sleep(300);
            }
        }

        class Counter
        {
            private int x;
            private int y;

            public Counter(int _x, int _y)
            {
                this.x = _x;
                this.y = _y;
            }

            public void Count()
            {
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine($"\tВторой поток:\t{i * x * y}");
                    Thread.Sleep(400);
                }
            }
        }
    }

    class ThreadCommonResources
    {
        static int x = 0;
        public static void Display()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        static void Count()
        {
            x = 1;
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("{0}: x={1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
        }
    }

    class ThreadLocker
    {
        static int x = 0;
        static object locker = new object();

        public static void Display()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        public static void Count()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine("{0}: x={1}", Thread.CurrentThread.Name, x);
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
    }

    class MonitorDemo
    {
        static int x = 0;
        static object locker = new object();

        public static void Display()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        public static void Count()
        {
            try
            {
                Monitor.Enter(locker);
                x = 1;
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }

    class AutoResetEventDemo
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        static int x = 0;

        public static void Display()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        public static void Count()
        {
            waitHandler.WaitOne();
            x = 1;
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
            waitHandler.Set();
        }
    }

    class MutexDemo
    {
        static Mutex mutexObj = new Mutex();
        static int x = 0;

        public static void Display()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        public static void Count()
        {
            mutexObj.WaitOne();
            x = 1;
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
            mutexObj.ReleaseMutex();
        }
    }

    class SemaphoreDemo
    {
        public static void Display()
        {
            for (int i = 1; i < 6; i++)
            {
                Reader reader = new Reader(i);
            }
        }

        class Reader
        {
            // создаем семафор
            static Semaphore sem = new Semaphore(3, 3);
            Thread myThread;
            int count = 3;// счетчик чтения

            public Reader(int i)
            {
                myThread = new Thread(Read);
                myThread.Name = "Читатель " + i.ToString();
                myThread.Start();
            }

            public void Read()
            {
                while (count > 0)
                {
                    sem.WaitOne();

                    Console.WriteLine("{0} входит в библиотеку", Thread.CurrentThread.Name);

                    Console.WriteLine("{0} читает", Thread.CurrentThread.Name);
                    Thread.Sleep(1000);

                    Console.WriteLine("{0} покидает библиотеку", Thread.CurrentThread.Name);
                    sem.Release();

                    count--;
                    Thread.Sleep(1000);
                }
            }
        }
    }

    class SemaphoreColor
    {
        public static void Display()
        {
            var colors = new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue,
                ConsoleColor.Yellow, ConsoleColor.Magenta};

            for (int i = 0; i < colors.Length; i++)
            {
                Reader reader = new Reader(i + 1, colors[i]);
            }
        }

        class Reader
        {
            // создаем семафор
            static Semaphore sem = new Semaphore(3, 3);
            Thread myThread;
            int count = 3;// счетчик чтения
            ConsoleColor color;
            static object locker = new object();

            public Reader(int i, ConsoleColor color)
            {
                this.color = color;
                myThread = new Thread(Read);
                myThread.Name = "Читатель " + i.ToString();
                myThread.Start();
            }

            public void Read()
            {
                var c = color;
                while (count > 0)
                {
                    sem.WaitOne();

                    printAction($"{Thread.CurrentThread.Name} входит в библиотеку", c, count);

                    printAction($"{Thread.CurrentThread.Name} читает", c);
                    Thread.Sleep(1000);

                    printAction($"{Thread.CurrentThread.Name} покидает библиотеку", c);
                    sem.Release();

                    count--;
                    Thread.Sleep(1000);
                }
            }

            void printAction(String text,  ConsoleColor _color, int _count = 0)
            {
                try
                {
                    Monitor.Enter(locker);
                    Console.ForegroundColor = _color;
                    if (_count > 0) text += $" {4 - _count}-й раз";
                    Console.WriteLine(text);
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Monitor.Exit(locker);
                }
            }
        }
    }

    class TimerDemo
    {
        public static void Display()
        {
            int num = 0;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 2000);
        }

        public static void Count(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{i}. {x}*{i}={x * i}");
            }
        }
    }
}