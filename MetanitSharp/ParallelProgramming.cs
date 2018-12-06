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
                    case 'p':
                        processorCount();
                        break;
                    case 'i':
                        ParallelInvoke.Display();
                        break;
                    case 'f':
                        ParallelFor.Display();
                        break;
                    case 'e':
                        ParallelForEach.Display();
                        break;
                    case 'b':
                        ParallelForBreak.Display();
                        break;
                    case 'c':
                        Cancellation.Display();
                        break;
                    case 'n':
                        CancellationExternal.Display();
                        break;
                    case 'l':
                        CancellationParallel.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("Р - количество ядер");
            Console.WriteLine("I - вызов задач Parallel.Invoke");
            Console.WriteLine("F - цикл Parallel.For");
            Console.WriteLine("E - цикл Parallel.ForEach");
            Console.WriteLine("B - выход из цикла");
            Console.WriteLine("C - отмена задачи");
            Console.WriteLine("N - отмена задачи в отдельной функции");
            Console.WriteLine("L - отмена параллельных операций");
            Console.WriteLine("X - выход из раздела");
        }

        static void processorCount()
        {
            Console.WriteLine($"Количество ядер {Environment.ProcessorCount}");
        }

    }
}
