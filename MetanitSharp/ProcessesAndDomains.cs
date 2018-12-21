using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ProcessesAndDomains
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
                    case 'p':
                        showProcesses();
                        break;
                    case 't':
                        showTreads();
                        break;
                    case 'm':
                        showModules();
                        break;
                    case 's':
                        startProcess();
                        break;
                    case 'r':
                        startProcessWithParams();
                        break;
                    case 'd':
                        showDomainAndAssemblies();
                        break;
                    case 'c':
                        createDomain();
                        break;
                    case 'a':
                        createDomainFromApp();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("P - вывод процессов");
            Console.WriteLine("T - вывод потоков процесса");
            Console.WriteLine("M - вывод модулей процесса");
            Console.WriteLine("S - запуск процесса");
            Console.WriteLine("R - запуск процесса с параметрами");
            Console.WriteLine("D - домен приложения и сборки");
            Console.WriteLine("C - создание вторичного домена");
            Console.WriteLine("A - создание вторичного домена для сборки");
            Console.WriteLine("X - выход из раздела");
        }

        static void showProcesses()
        {
            foreach (Process process in Process.GetProcesses())
            {
                // выводим id и имя процесса
                Console.WriteLine("ID: {0}  Name: {1}", process.Id, process.ProcessName);
            }

            Process proc = Process.GetProcessesByName("devenv")[0]; //процесс, представляющий Visual Studio
            Console.WriteLine("Visual Studio process ID: {0}", proc.Id);
        }

        static void showTreads()
        {
            Process proc = Process.GetProcessesByName("devenv")[0];
            ProcessThreadCollection processThreads = proc.Threads;

            foreach (ProcessThread thread in processThreads)
            {
                Console.WriteLine("ThreadId: {0}  StartTime: {1}",
                    thread.Id, thread.StartTime);
            }
        }

        static void showModules()
        {
            Process proc = Process.GetProcessesByName("devenv")[0];
            ProcessModuleCollection modules = proc.Modules;

            foreach (ProcessModule module in modules)
            {
                Console.WriteLine("Name: {0}  MemorySize: {1}",
                            module.ModuleName, module.ModuleMemorySize);
            }
        }

        static void startProcess()
        {
            // обращение к интернет-ресурсу
            Process.Start("http://google.com");
            // обращение к локальному файлу
            Process.Start("D://news.txt");
            // обращение к исполняемой программе
            Process.Start("C://Program Files (x86)//Notepad++//notepad++.exe");
        }

        static void startProcessWithParams()
        {
            Process.Start("C://Program Files (x86)//Notepad++//notepad++.exe", "D://news.txt");

            ProcessStartInfo procInfo = new ProcessStartInfo();
            // исполняемый файл программы - браузер хром
            procInfo.FileName = "C://Program Files (x86)//Google//Chrome//Application//chrome.exe";
            // аргументы запуска - адрес интернет-ресурса
            procInfo.Arguments = "http://google.com";
            Process.Start(procInfo);
        }

        static void showDomainAndAssemblies()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine("Name: {0}", domain.FriendlyName);
            Console.WriteLine("Base Directory: {0}", domain.BaseDirectory);
            Console.WriteLine();

            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);
        }

        static void createDomain()
        {
            AppDomain secondaryDomain = AppDomain.CreateDomain("Secondary domain");
            // событие загрузки сборки
            secondaryDomain.AssemblyLoad += Domain_AssemblyLoad;
            // событие выгрузки домена
            secondaryDomain.DomainUnload += SecondaryDomain_DomainUnload;


            Console.WriteLine("Домен: {0}", secondaryDomain.FriendlyName);
            secondaryDomain.Load(new AssemblyName("System.Data.Common")); // предполагается, что в папке Base Directory 
                                                                          //(по умолчанию папка проекта \bin\Debug\) находится файл System.Data.Common.dll
            Assembly[] assemblies = secondaryDomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine(asm.GetName().Name);
            // выгрузка домена
            AppDomain.Unload(secondaryDomain);
        }

        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен выгружен из процесса");
        }

        private static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Сборка загружена");
        }

        static void createDomainFromApp()
        {
            // создаем вторичный домен
            AppDomain factorialDomain = AppDomain.CreateDomain("Factorial Domain");
            factorialDomain.DomainUnload += FactorialDomain_DomainUnload;

            int number = 6;
            // определяем аргументы для программы
            string[] arguments = new string[] { number.ToString() };
            // полный путь к файлу программы - bin/Debug/MetanitSharpFactorial.exe
            string assemblyPath = factorialDomain.BaseDirectory + "MetanitSharpFactorial.exe";
            // загрузка и выполнение программы
            factorialDomain.ExecuteAssembly(assemblyPath, arguments);
            // выгрузка домена
            AppDomain.Unload(factorialDomain);
        }

        private static void FactorialDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен Factorial Domain выгружен из процесса");
        }
    }
}
