using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Linq
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
                        linqDemo();
                        linqExtentions();
                        linqBoth();
                        break;
                    case 'f':
                        LinqFilter.Display();
                        break;
                    case 'p':
                        LinqProjection.Display();
                        break;
                    case 's':
                        LinqSort.Display();
                        break;
                    case 'a':
                        arrays();
                        break;
                    case 'g':
                        LinqAggr.Display();
                        break;
                    case 't':
                        skipTake();
                        skipTakeWhile();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - пример с выбором и сортировкой");
            Console.WriteLine("F - фильтрация выборки");
            Console.WriteLine("P - проекция");
            Console.WriteLine("S - сортировка");
            Console.WriteLine("A - работа с множествами");
            Console.WriteLine("G - агрегатные операции");
            Console.WriteLine("T - методы Skip и Take");
            Console.WriteLine("X - выход из раздела");
        }

        static void linqDemo()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            var selectedTeams = from t in teams // определяем каждый объект из teams как t
                                where t.ToUpper().StartsWith("Б") //фильтрация по критерию
                                orderby t  // упорядочиваем по возрастанию
                                select t; // выбираем объект

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }

        static void linqExtentions()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            var selectedTeams = teams.Where(t => t.ToUpper().StartsWith("Б")).OrderBy(t => t);

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }

        static void linqBoth()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            int number = (from t in teams where t.ToUpper().StartsWith("Б") select t).Count();
            Console.WriteLine(number);
        }

        static void arrays()
        {
            string[] soft = { "Microsoft", "Google", "Apple" };
            string[] hard = { "Apple", "IBM", "Samsung" };

            var except = soft.Except(hard);

            Console.WriteLine("разность множеств");
            foreach (string s in except)
                Console.Write(s + " ");
            Console.WriteLine();

            var intersect = soft.Intersect(hard);

            Console.WriteLine("\nпересечение множеств");
            foreach (string s in intersect)
                Console.Write(s + " ");
            Console.WriteLine();

            var union = soft.Union(hard);

            Console.WriteLine("\nобъединение множеств");
            foreach (string s in union)
                Console.Write(s + " ");
            Console.WriteLine();

            var concat = soft.Concat(hard);

            Console.WriteLine("\nобъединение двух наборов");
            foreach (string s in concat)
                Console.Write(s + " ");
            Console.WriteLine();

            var concatDistinct = soft.Concat(hard).Distinct();

            Console.WriteLine("\nудаление дубликатов");
            foreach (string s in concatDistinct)
                Console.Write(s + " ");
            Console.WriteLine();
        }

        static void skipTake()
        {
            int[] numbers = { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5 };
            var result = numbers.Take(3);

            foreach (int i in result)
                Console.Write(i + " ");
            Console.WriteLine();

            var result2 = numbers.Skip(3);
            foreach (int i in result2)
                Console.Write(i + " ");
            Console.WriteLine();

            var result3 = numbers.Skip(4).Take(3);

            foreach (int i in result3)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        static void skipTakeWhile()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            foreach (var t in teams.TakeWhile(x => x.StartsWith("Б")))
                Console.WriteLine(t);
            foreach (var t in teams.SkipWhile(x => x.StartsWith("Б")))
                Console.WriteLine(t);
        }
    }
}
