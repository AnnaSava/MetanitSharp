using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            char key;

            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'b':
                        BasicConcepts.Run();
                        break;
                    case 'o':
                        OopConcepts.Run();
                        break;
                    case 'e':
                        ExceptionsHandling.Run();
                        break;
                    case 'i':
                        Interfaces.Run();
                        break;
                    case 'd':
                        DelegatesAndEvents.Run();
                        break;
                    case 'n':
                        OopExtention.Run();
                        break;
                    case 'c':
                        Collections.Run();
                        break;
                    case 'f':
                        FileAndStream.Run();
                        break;
                    case 'z':
                        Serialization.Run();
                        break;
                    case 's':
                        Strings.Run();
                        break;
                    case 'm':
                        Memory.Run();
                        break;
                    case 'y':
                        DynamicObjects.Run();
                        break;
                    case 't':
                        Multithreading.Run();
                        break;
                    case 'k':
                        Tasks.Run();
                        break;
                    case 'p':
                        ParallelProgramming.Run();
                        break;
                    case 'a':
                        AsyncProgramming.Run();
                        break;
                    case 'r':
                        ReflectionWork.Run();
                        break;
                    case 'q':
                        Linq.Run();
                        break;
                    case 'l':
                        XmlWork.Run();
                        break;
                }
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для выбора раздела");
            Console.WriteLine("B - основы программирования");
            Console.WriteLine("O - объектно-ориентированное программирование");
            Console.WriteLine("E - обработка исключений");            
            Console.WriteLine("I - интерфейсы");
            Console.WriteLine("D - делегаты, события и лямбды");
            Console.WriteLine("N - дополнительные возможности ООП");            
            Console.WriteLine("C - коллекции");
            Console.WriteLine("F - работа с потоками и файловой системой");
            Console.WriteLine("Z - сериализация");
            Console.WriteLine("S - строки");
            Console.WriteLine("M - сборка мусора, управление памятью, указатели");
            Console.WriteLine("Y - динамические объекты");
            Console.WriteLine("T - многопоточность");
            Console.WriteLine("K - задачи");
            Console.WriteLine("P - параллельное программирование");
            Console.WriteLine("A - асинхронное программирование");
            Console.WriteLine("R - рефлексия");
            Console.WriteLine("Q - LINQ (Language-Integrated Query)");
            Console.WriteLine("L - работа с XML");
        }
    }
}
