﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class AsyncFactorial
    {
        public static void Display()
        {
            FactorialAsync();   // вызов асинхронного метода

            Console.WriteLine("Введите число: ");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Квадрат числа равен {n * n}");
            Console.WriteLine("Конец метода Display");
        }

        static void Factorial()
        {
            int result = 1;
            for (int i = 1; i <= 6; i++)
            {
                result *= i;
            }
            Thread.Sleep(8000);
            Console.WriteLine($"Факториал равен {result}");
        }
        // определение асинхронного метода
        static async void FactorialAsync()
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно
            //await Task.Run(Factorial);                         // выполняется асинхронно
            await Task.Run(() => Factorial());                         // выполняется асинхронно
            Console.WriteLine("Конец метода FactorialAsync");  // выполняется синхронно
        }
    }

    class AsyncFile
    {
        public static void Display()
        {
            ReadWriteAsync();

            for (int i = 1; i <= 12; i++)
            {
                Console.WriteLine($"{i} Некоторая работа в методе Display");
            }
        }

        static async void ReadWriteAsync()
        {
            Console.WriteLine("Начало метода ReadWriteAsync");

            string s = "Hello world! One step at a time";

            // hello.txt - файл, который будет записываться и считываться
            using (StreamWriter writer = new StreamWriter(@"C:\Metanit\hello.txt", false))
            {
                await writer.WriteLineAsync(s);  // асинхронная запись в файл
                Console.WriteLine("Файл записан");
            }
            using (StreamReader reader = new StreamReader(@"C:\Metanit\hello.txt"))
            {
                string result = await reader.ReadToEndAsync();  // асинхронное чтение из файла
                Console.WriteLine("Считано из файла:");
                Console.WriteLine(result);
            }

            Console.WriteLine("Конец метода ReadWriteAsync");
        }
    }

    class AsyncParams
    {
        public static void Display()
        {
            FactorialAsync(5);
            FactorialAsync(6);
            Console.WriteLine("Некоторая работа в методе Display");
        }

        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Thread.Sleep(5000);
            Console.WriteLine($"Факториал {n} равен {result}");
        }
        // определение асинхронного метода
        static async void FactorialAsync(int n)
        {
            await Task.Run(() => Factorial(n));
        }
    }

    class AsyncReturn
    {
        public static void Display()
        {
            FactorialAsync(5);
            FactorialAsync(6);
        }

        static int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        // определение асинхронного метода
        static async void FactorialAsync(int n)
        {
            int x = await Task.Run(() => Factorial(n));
            Console.WriteLine($"Факториал {n} равен {x}");
        }
    }

    class AsyncFromComments
    {
        public static void Display()
        {
            RunAsyncOperations().GetAwaiter();
        }

        static async Task RunAsyncOperations()
        {
            var work = DoWork();
            DoIndependentWork();
            await work;
        }

        private static void DoIndependentWork()
        {
            Task.Delay(5000).Wait();
            Console.WriteLine("Independent work done");
        }

        static async Task DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000);
            }
        }
    }

    class AsyncReturnTask
    {
        public static void Display()
        {
            Task t1 = FactorialAsync(5);
            Task t2 = FactorialAsync(6);
            Console.WriteLine($"Task 1 completed: {t1.IsCompleted}\tTask 2 completed: {t2.IsCompleted}");
            Console.WriteLine("Некоторая работа в методе Display");

            Thread.Sleep(2000);
            Console.WriteLine($"Task 1 completed: {t1.IsCompleted}\tTask 2 completed: {t2.IsCompleted}");
        }

        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал {n} равен {result}");
        }

        // определение асинхронного метода
        static async Task FactorialAsync(int n)
        {
            await Task.Run(() => Factorial(n));
        }
    }

    class AsyncReturnTaskGeneric
    {
        public static async void Display()
        {
            int n1 = await FactorialAsync(5);
            int n2 = await FactorialAsync(6);
            Console.WriteLine($"n1={n1}  n2={n2}");
        }

        static int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        // определение асинхронного метода
        static async Task<int> FactorialAsync(int n)
        {
            return await Task.Run(() => Factorial(n));
        }
    }

    class AsyncValueTask
    {
        public static async void Display()
        {
            int n1 = await FactorialAsync(5);
            int n2 = await FactorialAsync(6);
            Console.WriteLine($"n1={n1}  n2={n2}");
        }

        static int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        // определение асинхронного метода
        static async ValueTask<int> FactorialAsync(int n)
        {
            return await Task.Run(() => Factorial(n));
        }
    }

    class AsyncParallel
    {
        public static void Display()
        {
            FactorialAsync();
            Thread.Sleep(2000);
            FactorialAsyncParallel();
        }

        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {n} равен {result}");
        }
        // определение асинхронного метода
        static async void FactorialAsync()
        {
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(3));
            await Task.Run(() => Factorial(5));
        }

        static async void FactorialAsyncParallel()
        {
            Task t1 = Task.Run(() => Factorial(4));
            Task t2 = Task.Run(() => Factorial(3));
            Task t3 = Task.Run(() => Factorial(5));
            await Task.WhenAll(new[] { t1, t2, t3 });
        }
    }

    class AsyncException
    {
        public static void Display()
        {
            FactorialAsync(-4);
            Thread.Sleep(5000);
            FactorialAsync(6);
        }

        static void Factorial(int n)
        {
            if (n < 1)
                throw new Exception($"{n} : число не должно быть меньше 1");
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {n} равен {result}");
        }

        static async void FactorialAsync(int n)
        {
            try
            {
                await Task.Run(() => Factorial(n));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
