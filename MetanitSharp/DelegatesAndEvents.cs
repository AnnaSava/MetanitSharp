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
                    case 'h':
                        DelegateAccountExample.Display();
                        break;
                    case 'e':
                        EventAccountExample.Display();
                        EventAccountArgsExample.Display();
                        break;
                    case 'a':
                        AnonymousMethodsDemo.Display();
                        AnonymousMethodsHandler.Display();
                        break;
                    case 'l':
                        LambdaDemo.Display();
                        LambdaHandler.Display();
                        LambdaAsArgument.Display();
                        break;
                    case 'c':
                        DelegateCoContraVariance.Display();
                        DelegateCoContraVarianceGeneric.Display();
                        break;
                    case 'f':
                        DonNetDelegates.Display();
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
            Console.WriteLine("H - пример с делегатами и списанием средств со счета");
            Console.WriteLine("E - пример с событиями");
            Console.WriteLine("A - анонимные методы");
            Console.WriteLine("L - лямбды");
            Console.WriteLine("C - ковариантность и контрвариантность");
            Console.WriteLine("F - делегаты Action, Predicate и Func");
            Console.WriteLine("X - выход из раздела");
        }
    }    
}
