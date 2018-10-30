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
                        break;
                }
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для выбора раздела");
            Console.WriteLine("B - основы программирования");
            Console.WriteLine("O - объектно-ориентированное программирование");
        }
    }
}
