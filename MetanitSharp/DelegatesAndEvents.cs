using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class DelegatesAndEvents
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
                        DelegateDemo.Display();
                        DelegateWithParams.Display();
                        DelegateMultiple.Display();
                        DelegateInvoke.Display();
                        DelegateAsParam.Display();
                        GenericDelegates.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - использование делегатов");
            Console.WriteLine("X - выход из раздела");
        }
    }    
}
