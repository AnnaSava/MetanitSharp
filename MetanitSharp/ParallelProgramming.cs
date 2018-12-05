using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ParallelProgramming
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
                    case 't':
                        taskDemo();
                        break;
                    case 'm':
                        TaskFromMethod.Display();
                        break;
                    case 'w':
                        TaskWait.Display();
                        break;
                    case 'i':
                        innerTask();
                        break;
                    case 'h':
                        innerTaskAttached();
                        break;
                    case 'a':
                        tasksArray();
                        break;
                    case 'l':
                        taskWaitAll();
                        break;
                    case 'r':
                        TaskReturn.Display();
                        break;
                    case 's':
                        TaskResult.Display();
                        break;
                    case 'c':
                        ContinuationTask.Display();
                        break;
                    case 'y':
                        ContinuationTaskMany.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("T - задачи Task");
            Console.WriteLine("M - задача из метода");
            Console.WriteLine("W - ожидание завершения задачи");
            Console.WriteLine("I - вложенные задачи");
            Console.WriteLine("H - вложенные прикрепленные задачи");
            Console.WriteLine("A - массив задач");
            Console.WriteLine("L - ожидание выполнения всех задач");
            Console.WriteLine("R - возвращение результатов из задач");
            Console.WriteLine("S - ожидание получения результата");
            Console.WriteLine("C - задачи продолжения");
            Console.WriteLine("Y - цепочки задач продолжения");
            Console.WriteLine("X - выход из раздела");
        }

        static void taskDemo()
        {
            Task task1 = new Task(() => Console.WriteLine("Task1 is executed"));
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Task2 is executed"));

            Task task3 = Task.Run(() => Console.WriteLine("Task3 is executed"));
        }       

        static void innerTask()
        {
            var outer = Task.Factory.StartNew(() =>      // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                });
            });
            outer.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of innerTask");
        }

        static void innerTaskAttached()
        {
            var outer = Task.Factory.StartNew(() =>      // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                }, TaskCreationOptions.AttachedToParent);
            });
            outer.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of Main");
        }

        static void tasksArray()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (var t in tasks1)
                t.Start();

            Task[] tasks2 = new Task[3];
            int j = 1;
            for (int i = 0; i < tasks2.Length; i++)
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));

            Console.WriteLine("Завершение метода Main");
        }

        static void taskWaitAll()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (var t in tasks1)
                t.Start();

            var i = Task.WaitAny(tasks1);
            Console.WriteLine($"Одна из задач завершилась. Индекс = {i}");

            Task.WaitAll(tasks1); // ожидаем завершения задач 
            Console.WriteLine("Завершение метода Main");
        }
               
    }
}
