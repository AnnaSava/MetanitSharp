using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetanitSharpMemory;

namespace MetanitSharp
{
    public static class Memory
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
                    case 'g':
                        GarbageCollectorDemo.Display();
                        break;
                    case 'f':
                        FinalizeAndDispose.Display();
                        break;
                    case 'c':
                        FinalizeAndDisposeTemplate.Display();
                        break;
                    case 'u':
                        Pointers.Display();
                        break;
                    case 'p':
                        ComplexPointers.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("G - сборщик мусора");
            Console.WriteLine("F - финализируемые объекты");
            Console.WriteLine("C - комбинирование Dispose и Finalize");
            Console.WriteLine("U - небезопасный код");
            Console.WriteLine("Р - указатели на структуры, массивы, классы");
            Console.WriteLine("X - выход из раздела");
        }      
    }
}
