using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Strings
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
                    case 's':
                        usingString();
                        break;
                    case 'm':
                        concat();
                        join();
                        compare();
                        search();
                        split();
                        trim();
                        substring();
                        insert();
                        remove();
                        replace();
                        upperLower();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("S - использование строк");
            Console.WriteLine("M - методы работы со строками");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingString()
        {
            string s1 = "hello";
            string s2 = null;

            string s3 = new String('a', 6); // результатом будет строка "aaaaaa"
            string s4 = new String(new char[] { 'w', 'o', 'r', 'l', 'd' });

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.WriteLine(s4);

            char ch1 = s1[1]; // символ 'e'
            Console.WriteLine(ch1);
            Console.WriteLine(s1.Length);
        }

        static void concat()
        {
            string s1 = "hello";
            string s2 = "world";
            string s3 = s1 + " " + s2; // результат: строка "hello world"
            string s4 = String.Concat(s3, "!!!"); // результат: строка "hello world!!!"

            Console.WriteLine(s4);
        }

        static void join()
        {
            string s5 = "apple";
            string s6 = "a day";
            string s7 = "keeps";
            string s8 = "a doctor";
            string s9 = "away";
            string[] values = new string[] { s5, s6, s7, s8, s9 };

            String s10 = String.Join(" ", values);
            // результат: строка "apple a day keeps a doctor away"
            Console.WriteLine(s10);
        }

        static void compare()
        {
            string s1 = "hello";
            string s2 = "world";

            int result = String.Compare(s1, s2);
            if (result < 0)
            {
                Console.WriteLine("Строка s1 перед строкой s2");
            }
            else if (result > 0)
            {
                Console.WriteLine("Строка s1 стоит после строки s2");
            }
            else
            {
                Console.WriteLine("Строки s1 и s2 идентичны");
            }
            // результатом будет "Строка s1 перед строкой s2"
        }

        static void search()
        {
            string s1 = "hello world";
            char ch = 'o';
            int indexOfChar = s1.IndexOf(ch); // равно 4
            Console.WriteLine(indexOfChar);

            string subString = "wor";
            int indexOfSubstring = s1.IndexOf(subString); // равно 6
            Console.WriteLine(indexOfSubstring);

            string ending = "rld";
            if (s1.EndsWith(ending))
            {
                Console.WriteLine($"Строка заканчивается на {ending}");
            }
            else
            {
                Console.WriteLine($"Строка не заканчивается на {ending}");
            }
        }

        static void split()
        {
            string text = "И поэтому все   так произошло ";

            string[] words = text.Split(new char[] { ' ' });

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine($"{i} [{words[i]}]");
            }

            string[] words2 = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words2.Length; i++)
            {
                Console.WriteLine($"{i} [{words2[i]}]");
            }
        }

        static void trim()
        {
            string text = " hello world ";

            text = text.Trim(); // результат "hello world"
            Console.WriteLine(text);
            text = text.Trim(new char[] { 'd', 'h' }); // результат "ello worl"
            Console.WriteLine(text);
        }

        static void substring()
        {
            string text = "Хороший день";
            // обрезаем начиная с третьего символа
            text = text.Substring(2);
            // результат "роший день"
            Console.WriteLine(text);
            // обрезаем сначала до последних двух символов
            text = text.Substring(0, text.Length - 2);
            // результат "роший де"
            Console.WriteLine(text);
        }

        static void insert()
        {
            string text = "Хороший день";
            string subString = "замечательный ";

            text = text.Insert(8, subString);
            Console.WriteLine(text);
        }

        static void remove()
        {
            string text = "Хороший день";
            // индекс последнего символа
            int ind = text.Length - 1;
            // вырезаем последний символ
            text = text.Remove(ind);
            Console.WriteLine(text);

            // вырезаем первые два символа
            text = text.Remove(0, 2);
            Console.WriteLine(text);
        }

        static void replace()
        {
            string text = "хороший день";

            text = text.Replace("хороший", "плохой");
            Console.WriteLine(text);

            text = text.Replace("о", "");
            Console.WriteLine(text);
        }

        static void upperLower()
        {
            string hello = "Hello world!";

            Console.WriteLine(hello.ToLower()); // hello world!
            Console.WriteLine(hello.ToUpper()); // HELLO WORLD!
        }
    }
}
