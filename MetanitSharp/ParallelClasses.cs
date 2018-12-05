using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class TaskFromMethod
    {
        public static void Display()
        {
            Task task = new Task(Show);
            task.Start();

            Console.WriteLine("Завершение метода Display");
        }

        static void Show()
        {
            Console.WriteLine("Начало работы метода Show");

            Console.WriteLine("Завершение работы метода Show");
        }
    }

    class TaskWait
    {
        public static void Display()
        {
            Task task = new Task(Show);
            task.Start();
            task.Wait();

            Console.WriteLine("Завершение метода Display");
        }

        static void Show()
        {
            Console.WriteLine("Начало работы метода Show");

            Console.WriteLine("Завершение работы метода Show");
        }
    }

    class TaskReturn
    {
        public static void Display()
        {
            Task<int> task1 = new Task<int>(() => Factorial(5));
            task1.Start();

            Console.WriteLine($"Факториал числа 5 равен {task1.Result}");

            Task<Book> task2 = new Task<Book>(() =>
            {
                return new Book { Title = "Война и мир", Author = "Л. Толстой" };
            });
            task2.Start();

            Book b = task2.Result;
            Console.WriteLine($"Название книги: {b.Title}, автор: {b.Author}");
        }

        static int Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
        }
    }

    class TaskResult
    {
        public static void Display()
        {
            Task<string> taskHello = new Task<string>(Hello);
            taskHello.Start();
            Console.WriteLine("Main thread before Result");
            Console.WriteLine(taskHello.Result);
            Console.WriteLine("Main thread after Result");
        }
        static string Hello()
        {
            Thread.Sleep(3000);
            return "Hello world!";
        }
    }

    class ContinuationTask
    {
        public static void Display()
        {
            Task task1 = new Task(() =>
            {
                Console.WriteLine("Id задачи: {0}", Task.CurrentId);
            });

            // задача продолжения
            Task task2 = task1.ContinueWith(Show);

            task1.Start();

            // ждем окончания второй задачи
            task2.Wait();
            Console.WriteLine("Выполняется работа метода Display");
        }

        static void Show(Task t)
        {
            Console.WriteLine("Id задачи: {0}", Task.CurrentId);
            Console.WriteLine("Id предыдущей задачи: {0}", t.Id);
            Thread.Sleep(3000);
        }
    }

    class ContinuationTaskMany
    {
        public static void Display()
        {
            Task task1 = new Task(() =>
            {
                Console.WriteLine("task 1");
                Console.WriteLine("Id задачи: {0}", Task.CurrentId);
            });

            // задача продолжения
            Task task2 = task1.ContinueWith(Show);

            Task task3 = task1.ContinueWith((Task t) =>
            {
                Console.WriteLine("task 3 after task 1");
                Console.WriteLine($"Id задачи: {Task.CurrentId}\tId предыдущей задачи: {t.Id}");
            });

            Task task4 = task2.ContinueWith((Task t) =>
            {
                Console.WriteLine("task 4 after task 2");
                Console.WriteLine($"Id задачи: {Task.CurrentId}\tId предыдущей задачи: {t.Id}");
            });

            task1.Start();
        }

        static void Show(Task t)
        {
            Console.WriteLine("Show");
            Console.WriteLine($"Id задачи: {Task.CurrentId}\tId предыдущей задачи: {t.Id}");
            Thread.Sleep(3000);
        }
    }
}
