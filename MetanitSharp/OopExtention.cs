using MetanitSharp.AccountSpace.ClientSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vip = MetanitSharp.AccountSpace.ClientSpace.VipSpace;
using Person = MetanitSharp.OopExtention.User;
using static MetanitSharp.StaticSpace.Message;
using static MetanitSharp.Geometry;
using MetanitSharp.ExtentionMethodDemo;

namespace MetanitSharp
{
    class OopExtention
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
                    case 'n':
                        namespaceDemo();
                        break;
                    case 'e':
                        extentionMethodDemo();
                        break;
                    case 'p':
                        partialClass();
                        break;
                    case 'a':
                        AnonymousTypes.Display();
                        break;
                    case 'l':
                        LocalFunction.Display();
                        break;
                    case 'm':
                        PatternMatching.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("N - пространства имен");
            Console.WriteLine("E - методы расширения");
            Console.WriteLine("P - частичные классы и методы");
            Console.WriteLine("A - анонимные типы");
            Console.WriteLine("L - локальные функции");
            Console.WriteLine("M - pattern matching");
            Console.WriteLine("X - выход из раздела");
        }

        static void namespaceDemo()
        {
            MetanitSharp.AccountSpace.Account account = new MetanitSharp.AccountSpace.Account(4);
            account.Display();

            Client client = new Client(10);
            client.Display();

            vip.VipClient vipClient = new vip.VipClient(8);
            vip.VipClient.DisplayVip(vipClient);

            Person person = new Person();
            person.name = "Tom";
            Console.WriteLine(person.name);

            ShowMyMessage("using static");

            double radius = 50;
            double result = GetArea(radius); // MetanitSharp.Geometry.GetArea
            Console.WriteLine(result);
        }

        public class User
        {
            public string name;
        }

        static void extentionMethodDemo()
        {
            string s = "Привет мир";
            char ch = 'и';
            int i = s.CharCount(ch);
            Console.WriteLine(i);

            int a = "38".To<int>();
            double b = "45,35".To<double>();
            int c = "3we".To<int>();
            Console.WriteLine($"a={a} b={b} c={c}");
        }

        static void partialClass()
        {
            PartialPerson tom = new PartialPerson();
            tom.Move();
            tom.Eat();

            tom.DoSomething();
        }
    }
}
