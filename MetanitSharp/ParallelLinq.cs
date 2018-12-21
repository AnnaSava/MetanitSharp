using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ParallelLinq
    {
        public static void Run()
        {
            char key;

            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'p':
                        parallelArr();
                        break;
                    case 'f':
                        forAll();
                        break;
                    case 'o':
                        asOrdered();
                        break;
                    case 'u':
                        asUnordered();
                        break;
                    case 'e':
                        handleExceptions();
                        break;
                    case 'c':
                        cancellation();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("P - параллельная обработка массива");
            Console.WriteLine("F - метод ForAll");
            Console.WriteLine("O - упорядочивание AsOrdered");
            Console.WriteLine("U - отмена упорядочивания AsUnordered");
            Console.WriteLine("E - обработка исключений");
            Console.WriteLine("C - прерывание операции");
            Console.WriteLine("X - выход из раздела");
        }

        static void parallelArr()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, };
            var factorials = from n in numbers.AsParallel()
                             select Factorial(n);
            foreach (var n in factorials)
                Console.WriteLine(n);

            var factorialsExt = numbers.AsParallel().Select(x => Factorial(x));
        }

        static void forAll()
        {
            int[] numbers = new int[] { -2, -1, 0, 1, 2, 4, 3, 5, 6, 7, 8, };
            (from n in numbers.AsParallel()
             where n > 0
             select Factorial(n))
             .ForAll(n => Console.WriteLine(n));
        }

        static void asOrdered()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var factorials = from n in numbers.AsParallel().AsOrdered()
                             where n > 0
                             select Factorial(n);

            foreach (var n in factorials)
                Console.WriteLine(n);
        }

        static void asUnordered()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var factorials = from n in numbers.AsParallel().AsOrdered()
                             where n > 0
                             select Factorial(n);
            var query = from n in factorials.AsUnordered()
                        where n > 100
                        select n;
            foreach (var n in query)
                Console.WriteLine(n);
        }

        static void handleExceptions()
        {
            object[] numbers2 = new object[] { 1, 2, 3, 4, 5, "hello" };

            var factorials = from n in numbers2.AsParallel()
                             let x = (int)n
                             select Factorial(x);
            try
            {
                factorials.ForAll(n => Console.WriteLine(n));
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static int Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            return result;
        }

        static void cancellation()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            new Task(() =>
            {
                Thread.Sleep(400);
                cts.Cancel();
            }).Start();
            try
            {
                int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, };
                var factorials = from n in numbers.AsParallel().WithCancellation(cts.Token)
                                 select SlowFactorial(n);
                foreach (var n in factorials)
                    Console.WriteLine(n);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Операция была прервана");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions != null)
                {
                    foreach (Exception e in ex.InnerExceptions)
                        Console.WriteLine(e.Message);
                }
            }
            finally
            {
                cts.Dispose();
            }
        }

        static int SlowFactorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            Thread.Sleep(1000);
            return result;
        }

    }
}
