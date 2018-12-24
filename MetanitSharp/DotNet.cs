using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class DotNet
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
                        dateTime();
                        formatDate();
                        break;
                    case 'l':
                        lazy();
                        break;
                    case 'm':
                        math();
                        break;
                    case 'r':
                        registry();
                        break;
                    case 'c':
                        convert();
                        break;
                    case 'a':
                        array();
                        arrayCopy();
                        arraySort();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - работа с датами");
            Console.WriteLine("L - отложенная инициализация и тип Lazy");
            Console.WriteLine("M - математические вычисления");
            Console.WriteLine("R - работа с реестром");
            Console.WriteLine("C - преобразование типов");
            Console.WriteLine("A - класс Array и массивы");
            Console.WriteLine("X - выход из раздела");
        }

        #region Dates

        static void dateTime()
        {
            DateTime date1 = new DateTime();
            Console.WriteLine(date1); // 01.01.0001 0:00:00

            Console.WriteLine(DateTime.MinValue);

            DateTime date2 = new DateTime(2015, 7, 20); // год - месяц - день
            Console.WriteLine(date2); // 20.07.2015 0:00:00

            DateTime date3 = new DateTime(2015, 7, 20, 18, 30, 25); // год - месяц - день - час - минута - секунда
            Console.WriteLine(date3); // 20.07.2015 18:30:25

            Console.WriteLine(DateTime.Now);
            Console.WriteLine(DateTime.UtcNow);
            Console.WriteLine(DateTime.Today);

            DateTime date4 = new DateTime(2015, 7, 20, 18, 30, 25); // 20.07.2015 18:30:25
            Console.WriteLine(date4.AddHours(3)); // 20.07.2015 21:30:25

            DateTime date5 = new DateTime(2015, 7, 20, 18, 30, 25); // 20.07.2015 18:30:25
            DateTime date6 = new DateTime(2015, 7, 20, 15, 30, 25); // 20.07.2015 15:30:25
            Console.WriteLine(date5.Subtract(date6)); // 03:00:00

            // вычтем три часа
            DateTime date7 = new DateTime(2015, 7, 20, 18, 30, 25);  // 20.07.2015 18:30:25
            Console.WriteLine(date7.AddHours(-3)); // 20.07.2015 15:30:25

            DateTime date8 = new DateTime(2015, 7, 20, 18, 30, 25);
            Console.WriteLine(date8.ToLocalTime()); // 20.07.2015 21:30:25
            Console.WriteLine(date8.ToUniversalTime()); // 20.07.2015 15:30:25
            Console.WriteLine(date8.ToLongDateString()); // 20 июля 2015 г.
            Console.WriteLine(date8.ToShortDateString()); // 20.07.2015
            Console.WriteLine(date8.ToLongTimeString()); // 18:30:25
            Console.WriteLine(date8.ToShortTimeString()); // 18:30
        }

        static void formatDate()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("D: " + now.ToString("D"));
            Console.WriteLine("d: " + now.ToString("d"));
            Console.WriteLine("F: " + now.ToString("F"));
            Console.WriteLine("f: {0:f}", now);
            Console.WriteLine("G: {0:G}", now);
            Console.WriteLine("g: {0:g}", now);
            Console.WriteLine("M: {0:M}", now);
            Console.WriteLine("O: {0:O}", now);
            Console.WriteLine("o: {0:o}", now);
            Console.WriteLine("R: {0:R}", now);
            Console.WriteLine("s: {0:s}", now);
            Console.WriteLine("T: {0:T}", now);
            Console.WriteLine("t: {0:t}", now);
            Console.WriteLine("U: {0:U}", now);
            Console.WriteLine("u: {0:u}", now);
            Console.WriteLine("Y: {0:Y}", now);

            Console.WriteLine(now.ToString("dd.MM.yy"));
            Console.WriteLine(now.ToString("ddd dddd MMM MMMM yyyy g"));
            Console.WriteLine(now.ToString("HH:mm:ss K zz"));
            Console.WriteLine(now.ToString("hh:mm"));
            Console.WriteLine(now.ToString("f/ffffff"));
        }

        #endregion

        #region Lazy

        static void lazy()
        {
            Reader reader = new Reader();
            reader.ReadEbook();
            reader.ReadBook();
        }

        class Reader
        {
            Lazy<Library> library = new Lazy<Library>();
            public void ReadBook()
            {
                library.Value.GetBook();
                Console.WriteLine("Читаем бумажную книгу");
            }

            public void ReadEbook()
            {
                Console.WriteLine("Читаем книгу на компьютере");
            }
        }

        class Library
        {
            private string[] books = new string[99];

            public void GetBook()
            {
                Console.WriteLine("Выдаем книгу читателю");
            }
        }

        #endregion

        static void math()
        {
            double arg = -12.4;
            Console.WriteLine($"Math.Abs({arg})\t\t{Math.Abs(arg)}"); // 12,4

            arg = 16;
            Console.WriteLine($"Math.Sqrt({arg})\t\t{Math.Sqrt(arg)}"); // 4

            arg = 1;
            Console.WriteLine($"Math.Asin({arg})\t\t{Math.Asin(arg)}");
            Console.WriteLine($"Math.Acos({arg})\t\t{Math.Acos(arg)}");
            Console.WriteLine($"Math.Atan({arg})\t\t{Math.Atan(arg)}");

            arg = 2.34;
            Console.WriteLine($"Math.Ceiling({arg})\t{Math.Ceiling(arg)}"); // 3
            arg = 2.56;
            Console.WriteLine($"Math.Floor({arg})\t{Math.Floor(arg)}"); // 2
            arg = 16.89;
            Console.WriteLine($"Math.Truncate({arg})\t{Math.Truncate(arg)}"); // 16

            arg = 20.56;
            Console.WriteLine($"Math.Round({arg})\t{Math.Round(arg)}"); // 21
            arg = 20.46;
            Console.WriteLine($"Math.Round({arg})\t{Math.Round(arg)}"); // 20

            arg = 20.567;
            Console.WriteLine($"Math.Round({arg}, 2)\t{Math.Round(arg, 2)}"); // 20,57
            arg = 20.463;
            Console.WriteLine($"Math.Round({arg}, 1)\t{Math.Round(arg, 1)}"); // 20,5

            arg = 15;
            Console.WriteLine($"Math.Sign({arg})\t\t{Math.Sign(arg)}"); // 1
            arg = -5;
            Console.WriteLine($"Math.Sign({arg})\t\t{Math.Sign(arg)}"); // -1

            int arg1 = 100, arg2 = 9340;
            Console.WriteLine($"Math.BigMul({arg1}, {arg2})\t{Math.BigMul(arg1, arg2)}"); // 934000 

            //возвращает остаток от деления a на b
            arg1 = 26; arg2 = 4;
            Console.WriteLine($"Math.IEEERemainder({arg1}, {arg2})\t{Math.IEEERemainder(arg1, arg2)}"); // 2 = 26-24

            //возвращает результат от деления a/b, а остаток помещается в параметр result
            arg1 = 14; arg2 = 5;
            Console.WriteLine($"Math.DivRem({arg1}, {arg2})\t{Math.DivRem(arg1, arg2, out int result)}\t{result}"); 

            Console.WriteLine("\nРадиус круга\tПлощадь круга");
            for (int i = 1; i <= 5; i++)
            {
                var radius = i;
                double area = Math.PI * Math.Pow(radius, 2);
                Console.WriteLine("{0}\t\t{1}", radius, area);
            }
        }

        #region Registry

        static void registry()
        {
            registryCreateKey();
            
            registryRead();
            Console.WriteLine("Нажмите клавишу для создания вложенного ключа");
            Console.ReadLine();
            registryCreateSubKey();
            Console.WriteLine("Нажмите клавишу для удаления вложенного ключа");
            Console.ReadLine();
            registryRemove();
        }

        static void registryCreateKey()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("HelloKey");
            helloKey.SetValue("login", "admin");
            helloKey.SetValue("password", "12345");
            helloKey.Close();
            Console.WriteLine("Ключ HelloKey создан");
        }

        static void registryCreateSubKey()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey", true);
            RegistryKey subHelloKey = helloKey.CreateSubKey("SubHelloKey");
            subHelloKey.SetValue("val", "23");
            subHelloKey.Close();
            helloKey.Close();
            Console.WriteLine("Ключ SubHelloKey создан");            
        }

        static void registryRead()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey");

            string login = helloKey.GetValue("login").ToString();
            string password = helloKey.GetValue("password").ToString();
            helloKey.Close();

            Console.WriteLine(login);
            Console.WriteLine(password);
        }

        static void registryRemove()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey", true);
            // удаляем вложенный ключ
            helloKey.DeleteSubKey("SubHelloKey");
            Console.WriteLine("Вложенный ключ удален");
            // удаляем значение из ключа
            helloKey.DeleteValue("login");
            Console.WriteLine("Значение login удалено");
            helloKey.Close();            

            Console.WriteLine("Нажмите клавишу для удаления ключа");
            Console.ReadLine();
            // удаляем сам ключ
            currentUserKey.DeleteSubKey("HelloKey");
            Console.WriteLine("Ключ удален");
        }

        #endregion

        static void convert()
        {
            int a = int.Parse("10");
            double b = double.Parse("23,56");
            decimal c = decimal.Parse("12,45");
            byte d = byte.Parse("4");
            Console.WriteLine($"a={a}  b={b}  c={c}  d={d}");

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            b = double.Parse("23.56", formatter);
            Console.WriteLine($"b={b}");

            int number;
            string input = "67t";

            bool result = int.TryParse(input, out number);
            if (result == true)
                Console.WriteLine("Преобразование прошло успешно");
            else
                Console.WriteLine("Преобразование завершилось неудачно");

            int n = Convert.ToInt32("23");
            bool x = true;
            double y = Convert.ToDouble(x);
            Console.WriteLine($"n={n}  y={y}");
        }

        static void array()
        {
            int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };

            // расположим в обратном порядке
            Array.Reverse(numbers);

            // уменьшим массив до 4 элементов
            Array.Resize(ref numbers, 4);

            foreach (int number in numbers)
            {
                Console.Write($"{number} \t");
            }
            Console.WriteLine();
        }

        static void arrayCopy()
        {
            int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
            int[] numbers2 = new int[5];

            // копируем из numbers с 2-го индекса 5 элементов 
            // и поместим их в массив numbers2, начиная с 0-го индекса
            Array.Copy(numbers, 2, numbers2, 0, 5);

            foreach (int number in numbers2)
            {
                Console.Write($"{number} \t");
            }
            Console.WriteLine();
        }

        static void arraySort()
        {
            int[] numbers = { -3, 10, 0, -5, 12, 1, 22, 3 };

            Array.Sort(numbers);

            foreach (int number in numbers)
            {
                Console.Write($"{number} \t");
            }
            Console.WriteLine();
        }
    }
}
