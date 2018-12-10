using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class AsyncProgramming
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
                    case 'd':
                        AsyncFactorial.Display();
                        break;
                    case 'f':
                        AsyncFile.Display();
                        break;
                    case 'p':
                        AsyncParams.Display();
                        break;
                    case 'n':
                        AsyncReturn.Display();
                        break;
                    case 'c':
                        AsyncFromComments.Display();
                        break;
                    case 't':
                        AsyncReturnTask.Display();
                        break;
                    case 'g':
                        AsyncReturnTaskGeneric.Display();
                        break;
                    case 'v':
                        AsyncValueTask.Display();
                        break;
                    case 'l':
                        AsyncParallel.Display();
                        break;
                    case 'e':
                        AsyncException.Display();
                        break;
                    case 'u':
                        AsyncExceptionIsFaulted.Display();
                        break;
                    case 'm':
                        AsyncMultipleExceptions.Display();
                        break;
                    case 'y':
                        AsyncCatchFinally.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - пример с факториалом");
            Console.WriteLine("F - пример с файлом");
            Console.WriteLine("P - передача параметров в асинхронный метод");
            Console.WriteLine("N - получение результата из асинхронной операции");
            Console.WriteLine("C - пример из комментариев");
            Console.WriteLine("T - возвращение задачи из асинхронного метода");
            Console.WriteLine("G - возвращение значения внутри задачи");
            Console.WriteLine("V - возвращение ValueTask");
            Console.WriteLine("L - параллельный запуск задач");
            Console.WriteLine("E - обработка исключений");            
            Console.WriteLine("U - свойство IsFaulted");
            Console.WriteLine("M - обработка нескольких исключений");
            Console.WriteLine("Y - await в блоках catch и finally");
            Console.WriteLine("X - выход из раздела");
        }
    }
}
