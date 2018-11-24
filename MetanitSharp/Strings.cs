using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    case 'f':
                        format();
                        moneyFormat();
                        integerFormat();
                        floatFormat();
                        percentFormat();
                        customFormat();
                        toString();
                        interpolation();
                        break;
                    case 'b':
                        stringBuilder();
                        stringBuilderMethods();
                        break;
                    case 'r':
                        regex();
                        regex2();
                        regexPhone();
                        regexPhoneNum();
                        regexEmail();
                        regexReplace();
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
            Console.WriteLine("F - форматирование строк");
            Console.WriteLine("B - StringBuilder");
            Console.WriteLine("R - регулярные выражения");
            Console.WriteLine("X - выход из раздела");
        }

        #region Methods

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

        #endregion

        #region Format

        static void format()
        {
            Person person = new Person { Name = "Tom", Age = 23 };

            string output = String.Format("Имя: {0}  Возраст: {1}", person.Name, person.Age);
            Console.WriteLine(output);
        }

        static void moneyFormat()
        {
            double number = 23.7;
            string result = String.Format("{0:C}", number);
            Console.WriteLine(result); // $ 23.7
            string result2 = String.Format("{0:C2}", number);
            Console.WriteLine(result2); // $ 23.70
        }

        static void integerFormat()
        {
            int number = 23;
            string result = String.Format("{0:d}", number);
            Console.WriteLine(result); // 23
            string result2 = String.Format("{0:d4}", number);
            Console.WriteLine(result2); // 0023
        }

        static void floatFormat()
        {
            int number = 23;
            string result = String.Format("{0:f}", number);
            Console.WriteLine(result); // 23,00

            double number2 = 45.08;
            string result2 = String.Format("{0:f4}", number2);
            Console.WriteLine(result2); // 45,0800

            double number3 = 25.07;
            string result3 = String.Format("{0:f1}", number3);
            Console.WriteLine(result2); // 25,1
        }

        static void percentFormat()
        {
            decimal number = 0.15345m;
            Console.WriteLine("{0:P1}", number);// 15.3%
        }

        static void customFormat()
        {
            long number = 19876543210;
            string result = String.Format("{0:+# (###) ###-##-##}", number);
            Console.WriteLine(result); // +1 (987) 654-32-10
        }

        static void toString()
        {
            long number = 19876543210;
            Console.WriteLine(number.ToString("+# (###) ###-##-##"));// +1 (987) 654-32-10

            double money = 24.8;
            Console.WriteLine(money.ToString("C2")); // $ 24,80
        }

        static void interpolation()
        {
            int x = 8;
            int y = 7;
            string result = $"{x} + {y} = {x + y}";
            Console.WriteLine(result); // 8 + 7 = 15

            long number = 19876543210;
            Console.WriteLine($"{number:+# ### ### ## ##}"); // +1 987 654 32 10

            Person person = new Person { Name = "Tom", Age = 23 };
            Console.WriteLine($"Имя: {person.Name}  Возраст: {person.Age}");


            Console.WriteLine($"Имя: {person.Name,-5} Возраст: {person.Age}"); // пробелы после
            Console.WriteLine($"Имя: {person.Name,5} Возраст: {person.Age}"); // пробелы до

            person = null;
            string output = $"{person?.Name ?? "Имя по умолчанию"}";
            Console.WriteLine(output);
        }

        #endregion

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        static void stringBuilder()
        {
            StringBuilder sb = new StringBuilder("Название: ");
            Console.WriteLine("Длина строки: {0}", sb.Length); // 10
            Console.WriteLine("Емкость строки: {0}", sb.Capacity); // 16

            sb.Append(" Руководство");
            Console.WriteLine("Длина строки: {0}", sb.Length); // 22
            Console.WriteLine("Емкость строки: {0}", sb.Capacity); // 32

            sb.Append(" по C#");
            Console.WriteLine("Длина строки: {0}", sb.Length); // 28
            Console.WriteLine("Емкость строки: {0}", sb.Capacity); // 32
        }

        static void stringBuilderMethods()
        {
            StringBuilder sb = new StringBuilder("Привет мир");
            sb.Append("!");
            sb.Insert(7, "компьютерный ");
            Console.WriteLine(sb);

            // заменяем слово
            sb.Replace("мир", "world");
            Console.WriteLine(sb);

            // удаляем 13 символов, начиная с 7-го
            sb.Remove(7, 13);
            Console.WriteLine(sb);

            // получаем строку из объекта StringBuilder
            string s = sb.ToString();
            Console.WriteLine(s);
        }

        static void regex()
        {
            string s = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа";
            Regex regex = new Regex(@"туп(\w*)");
            MatchCollection matches = regex.Matches(s);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    Console.WriteLine(match.Value);
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }


        }

        static void regex2()
        {
            string s = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа";
            Regex regex = new Regex(@"\w*губ\w*");

            MatchCollection matches = regex.Matches(s);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    Console.WriteLine(match.Value);
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }
        }

        static void regexPhone()
        {
            string s = "456-435-2318";
            Regex regex = new Regex(@"\d{3}-\d{3}-\d{4}");

            Console.WriteLine(regex.Match(s).Value);
        }

        static void regexPhoneNum()
        {
            string s = "456-435-2318";
            Regex regex = new Regex("[0-9]{3}-[0-9]{3}-[0-9]{4}");

            Console.WriteLine(regex.Match(s).Value);
        }

        static void regexEmail()
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            Console.WriteLine("Введите адрес электронной почты");
            string email = Console.ReadLine();

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Email подтвержден");
            }
            else
            {
                Console.WriteLine("Некорректный email");
            }
        }

        static void regexReplace()
        {
            string s = "Мама  мыла  раму. ";
            string pattern = @"\s+";
            string target = " ";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(s, target);

            Console.WriteLine(result);
        }
    }
}
