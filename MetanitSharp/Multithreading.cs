﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Multithreading
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
                    case 'h':
                        usingThread();
                        break;
                    case 'c':
                        createThread();
                        break;
                    case 'y':
                        multiThreadFromComments();
                        break;
                    case 'p':
                        ParameterizedThread.Display();
                        break;
                    case 'a':
                        ParameterizedWithArray.Display();
                        break;
                    case 'o':
                        ParameterizedWithClass.Display();
                        break;
                    case 'q':
                        ParameterizedTypeSafe.Display();
                        break;
                    case 'r':
                        ThreadCommonResources.Display();
                        break;
                    case 'l':
                        ThreadLocker.Display();
                        break;
                    case 'm':
                        MonitorDemo.Display();
                        break;
                    case 'e':
                        AutoResetEventDemo.Display();
                        break;
                    case 'u':
                        MutexDemo.Display();
                        break;
                    case 'n':
                        launchSingleApp();
                        break;
                    case 's':
                        SemaphoreDemo.Display();
                        break;
                    case 'k':
                        SemaphoreColor.Display();
                        break;
                    case 't':
                        TimerDemo.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("H - класс Thread");
            Console.WriteLine("C - создание потоков");
            Console.WriteLine("Y - несколько потоков в цикле");
            Console.WriteLine("P - поток с параметром");
            Console.WriteLine("A - поток с параметром в виде массива");
            Console.WriteLine("O - поток с параметром в виде класса");
            Console.WriteLine("Q - поток с методом из отдельного класса");
            Console.WriteLine("R - потоки с общим ресурсом");
            Console.WriteLine("L - блокировка потока");
            Console.WriteLine("M - мониторы");
            Console.WriteLine("E - AutoResetEvent");
            Console.WriteLine("U - мьютексы");
            Console.WriteLine("N - запуск только одной копии приложения");
            Console.WriteLine("S - семафоры");
            Console.WriteLine("K - семафоры, пример с цветом");
            Console.WriteLine("T - таймер");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingThread()
        {
            // получаем текущий поток
            Thread t = Thread.CurrentThread;

            //получаем имя потока
            Console.WriteLine("Имя потока: {0}", t.Name);
            t.Name = "Метод Main";
            Console.WriteLine("Имя потока: {0}", t.Name);

            Console.WriteLine("Запущен ли поток: {0}", t.IsAlive);
            Console.WriteLine("Приоритет потока: {0}", t.Priority);
            Console.WriteLine("Статус потока: {0}", t.ThreadState);

            // получаем домен приложения
            Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);
        }

        static void createThread()
        {
            // создаем новый поток
            Thread myThread = new Thread(new ThreadStart(Count));
            myThread.Start(); // запускаем поток

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"Главный поток:\t{i * i}");
                Thread.Sleep(300);
            }
        }

        static void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"\tВторой поток:\t{i * i}");
                Thread.Sleep(400);
            }
        }

        static void multiThreadFromComments()
        {
            Console.WriteLine("Несколько потоков в цикле");

            for (int i = 0; i < 10; i++)
            {
                int j = i;
                Thread tr = new Thread(() =>
                {
                    Console.WriteLine($"i={i}\tj={j}");
                });

                tr.Start();
            }
        }

        static void launchSingleApp()
        {
            System.Diagnostics.Process.Start("MetanitSharpMutexSingle.exe");
            System.Diagnostics.Process.Start("MetanitSharpMutexSingle.exe");
        }
    }
}
