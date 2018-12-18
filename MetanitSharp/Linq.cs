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
                    case 'q':
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
                    case 'r':
                        LinqAggr.Display();
                        break;
                    case 't':
                        skipTake();
                        skipTakeWhile();
                        break;
                    case 'g':
                        LinqGroup.Display();
                        break;
                    case 'j':
                        LinqJoin.Display();
                        break;
                    case 'n':
                        LinqAllAny.Display();
                        break;
                    case 'i':
                        deffered();
                        immediateCount();
                        defferedCount();
                        immediateList();
                        break;
                    case 'd':
                        delegateParam();
                        methodParam();
                        selectMethodParam();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("Q - пример с выбором и сортировкой");
            Console.WriteLine("F - фильтрация выборки");
            Console.WriteLine("P - проекция");
            Console.WriteLine("S - сортировка");
            Console.WriteLine("A - работа с множествами");
            Console.WriteLine("R - агрегатные операции");
            Console.WriteLine("T - методы Skip и Take");
            Console.WriteLine("G - группировка");
            Console.WriteLine("J - соединение коллекций");
            Console.WriteLine("N - методы All и Any");
            Console.WriteLine("I - отложенное и немедленное выполнение запроса");
            Console.WriteLine("D - делегаты и анонимные методы в запросах");
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

        static void deffered()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            var selectedTeams = from t in teams where t.ToUpper().StartsWith("Б") orderby t select t;

            // изменение массива после определения LINQ-запроса
            teams[1] = "Ювентус";
            // выполнение LINQ-запроса
            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }

        static void immediateCount()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            // определение и выполнение LINQ-запроса
            int i = (from t in teams
                     where t.ToUpper().StartsWith("Б")
                     orderby t
                     select t).Count();
            Console.WriteLine(i); //3
            teams[1] = "Ювентус";
            Console.WriteLine(i); //3
        }

        static void defferedCount()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            // определение LINQ-запроса
            var selectedTeams = from t in teams
                                where t.ToUpper().StartsWith("Б")
                                orderby t
                                select t;
            // выполнение запроса
            Console.WriteLine(selectedTeams.Count()); //3
            teams[1] = "Ювентус";
            // выполнение запроса
            Console.WriteLine(selectedTeams.Count()); //2
        }

        static void immediateList()
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            // выполнение LINQ-запроса
            var selectedTeams = (from t in teams
                                 where t.ToUpper().StartsWith("Б")
                                 orderby t
                                 select t).ToList<string>();
            // изменение массива никак не затронет список selectedTeams
            teams[1] = "Ювентус";

            foreach (string s in selectedTeams)
                Console.WriteLine(s);
        }

        static void delegateParam()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            Func<int, bool> MoreThanTen = delegate (int i) { return i > 10; };

            var result = numbers.Where(MoreThanTen);

            foreach (int i in result)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        static void methodParam()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            var result = numbers.Where(MoreThanTen);

            foreach (int i in result)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        private static bool MoreThanTen(int i)
        {
            return i > 10;
        }

        static void selectMethodParam()
        {
            int[] numbers = { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 };

            var result = numbers.Where(i => i > 0).Select(Factorial);

            foreach (int i in result)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        static int Factorial(int x)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
                result *= i;
            return result;
        }
    }
}
