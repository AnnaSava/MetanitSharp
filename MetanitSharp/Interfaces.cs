using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    public static class Interfaces
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
                    case 'i':
                        usingInterface();
                        break;
                    case 'h':
                        InterfaceInheritance.Display();
                        OverrideInInterface.Display();
                        ExplicitImplementation.Display();
                        break;
                    case 'g':
                        GenericInterfaces.Display();
                        break;
                    case 'e':
                        Clonable.Display();
                        ClonableComplex.Display();
                        Comparable.Display();
                        break;
                    case 'c':
                        InterfaceCoContraVariance.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("I - использование интерфейсов");
            Console.WriteLine("H - наследование интерфейсов");
            Console.WriteLine("G - интерфейсы в обобщениях и обобщенные интерфейсы");
            Console.WriteLine("E - примеры встроенных интерфейсов");
            Console.WriteLine("C - ковариантность и контрвариантность обобщенных интерфейсов");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingInterface()
        {
            InterfaceDemo.Display();
            MultipleInterfaces.Display();
        }
    }
}
