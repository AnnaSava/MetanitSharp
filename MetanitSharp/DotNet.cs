using System;
using System.Collections.Generic;
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
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - работа с датами");
            Console.WriteLine("X - выход из раздела");
        }

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
    }
}
