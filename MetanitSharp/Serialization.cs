using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Serialization
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
                    case 'b':
                        SerializationBinary.Display();
                        break;
                    case 's':
                        SerializationSoap.Display();
                        break;
                    case 'l':
                        SerializationXml.Display();
                        break;
                    case 'j':
                        SerializationJson.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("B - бинарная сериализация");
            Console.WriteLine("S - SOAP сериализация");
            Console.WriteLine("L - XML сериализация");
            Console.WriteLine("L - JSON сериализация");
            Console.WriteLine("X - выход из раздела");
        }
    }
}
