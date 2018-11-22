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
                    case 't':
                        OopExtention.Run();
                        break;
                    case 'c':
                        Collections.Run();
                        break;
                    case 'f':
                        FileAndStream.Run();
                        break;
                    case 's':
                        Serialization.Run();
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
            Console.WriteLine("T - дополнительные возможности ООП");            
            Console.WriteLine("C - коллекции");
            Console.WriteLine("F - работа с потоками и файловой системой");
            Console.WriteLine("S - сериализация");
        }
    }
}
