using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ParallelInvoke
    {
        public static void Display()
        {
            Parallel.Invoke(Show,
                () =>
                {
                    Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
                    Thread.Sleep(3000);
                },
                () => Factorial(5));
        }

        static void Show()
        {
            Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
            Thread.Sleep(3000);
            Console.WriteLine("Результат {0}", result);
        }
    }

    class ParallelFor
    {
        public static void Display()
        {
            Parallel.For(1, 10, Factorial);
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Задача {Task.CurrentId}\t Факториал {x} равен {result}");
            Thread.Sleep(3000);
        }
    }

    class ParallelForEach
    {
        public static void Display()
        {
            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 3, 5, 8 },
                Factorial);
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Задача {Task.CurrentId}\t Факториал {x} равен {result}");
            Thread.Sleep(3000);
        }
    }

    class ParallelForBreak
    {
        public static void Display()
        {
            ParallelLoopResult result = Parallel.For(1, 10, Factorial);

            if (!result.IsCompleted)
                Console.WriteLine("Выполнение цикла завершено на итерации {0}",
                                                result.LowestBreakIteration);
        }

        static void Factorial(int x, ParallelLoopState pls)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
                if (i == 5)
                    pls.Break();
            }
            Console.WriteLine($"Задача {Task.CurrentId}\t Факториал {x} равен {result}");
            Thread.Sleep(3000);
        }
    }

    class Cancellation
    {
        public static void Display()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int number = 6;

            Task task1 = new Task(() =>
            {
                int result = 1;
                for (int i = 1; i <= number; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана токеном");
                        return;
                    }

                    result *= i;
                    Console.WriteLine("Факториал числа {0} равен {1}", i, result);
                    Thread.Sleep(300);
                }
            });
            task1.Start();

            new Task(() =>
            {
                Thread.Sleep(1000);
                cancelTokenSource.Cancel();
            }).Start();

        }
    }

    class CancellationExternal
    {
        public static void Display()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task1 = new Task(() => Factorial(5, token));
            task1.Start();

            new Task(() =>
            {
                Thread.Sleep(1000);
                cancelTokenSource.Cancel();
            }).Start();
        }

        static void Factorial(int x, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                result *= i;
                Console.WriteLine("Факториал числа {0} равен {1}", i, result);
                Thread.Sleep(300);
            }
        }
    }

    class CancellationParallel
    {
        public static void Display()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancelTokenSource.Cancel();
            }).Start();

            try
            {
                Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 },
                                        new ParallelOptions { CancellationToken = token }, Factorial);
                // или так
                //Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Операция прервана токеном");
            }
            finally
            {
                cancelTokenSource.Dispose();
            }

            Console.ReadLine();
        }
        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            Thread.Sleep(3000);
        }
    }
}
