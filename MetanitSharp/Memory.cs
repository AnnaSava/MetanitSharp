using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
   public static class Memory
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
                    case 'g':
                        usingGC();
                        break;
                    case 'f':
                        finalize();
                        break;
                    case 'c':
                        usingCombinedTemplate();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("G - сборщик мусора");
            Console.WriteLine("F - финализируемые объекты");
            Console.WriteLine("C - комбинирование Dispose и Finalize");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingGC()
        {
            long totalMemory = GC.GetTotalMemory(false);
            Console.WriteLine(totalMemory);

            for (int i = 1; i <= 1000; i++)
            {
                var user = new User { Name = $"User {i}", Age = i };

                if (i % 100 == 0)
                {
                    Console.WriteLine($"{i}\tMemory={GC.GetTotalMemory(false)}");
                }
            }

            var user1001 = new User { Name = "User 1001", Age = 1001 };
            var generation = GC.GetGeneration(user1001);
            Console.WriteLine($"Generation of {user1001.Name} = {generation}");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            totalMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"After Collect()\t{totalMemory}");

            generation = GC.GetGeneration(user1001);
            Console.WriteLine($"Generation of {user1001.Name} = {generation}");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            totalMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"After Second Collect()\t{totalMemory}");

            generation = GC.GetGeneration(user1001);
            Console.WriteLine($"Generation of {user1001.Name} = {generation}");
        }

        class User
        {
            public String Name { get; set; }
            public int Age { get; set; }
        }

        static void finalize()
        {
            Test();
            GC.Collect();

            PersonDisposable p2 = new PersonDisposable { Name = "Bob" };
            p2.Dispose();

            try
            {
                tryDispose();
            }
            catch { }

            using (PersonDisposable p = new PersonDisposable())
            {
                p.Name = "Jane";
            }
        }

        static void Test()
        {
            Person p = new Person { Name = "Tom" };            
        }

        static void tryDispose()
        {
            PersonDisposable p = null;
            var y = 0;
            try
            {
                p = new PersonDisposable { Name = "Kate" };
                var x = 10 / y;
            }
            finally
            {
                if (p != null)
                {
                    p.Dispose();
                }
            }
        }

        public class Person
        {
            public String Name { get; set; }

            ~Person()
            {
                Console.Beep();
                Console.WriteLine($"Person {Name} destroyed");
            }
        }

        public class PersonDisposable : IDisposable
        {
            public String Name { get; set; }

            public void Dispose()
            {
                Console.Beep();
                Console.WriteLine($"PersonDisposable {Name} destroyed");
            }
        }

        static void usingCombinedTemplate()
        {
            Method();
            GC.Collect();

            var obj = new SomeClass { Number = 2 };
            obj.Dispose();
        }

        static void Method()
        {
            var obj = new SomeClass { Number = 1 };
        }

        public class SomeClass : IDisposable
        {
            public int Number { get; set; }

            private bool disposed = false;

            // реализация интерфейса IDisposable.
            public void Dispose()
            {
                Console.WriteLine($"Объект {Number}: \tВызван метод Dispose");
                Dispose(true);
                // подавляем финализацию
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        // освобождаем управляемые ресурсы
                        Console.WriteLine($"Объект {Number}: \tOсвобождаем управляемые ресурсы");
                    }
                    // освобождаем неуправляемые объекты
                    Console.WriteLine($"Объект {Number}: \tOсвобождаем неуправляемые ресурсы");
                    disposed = true;
                }
            }

            // деструктор
            ~SomeClass()
            {
                Console.WriteLine($"Объект {Number}: \tВызван деструктор");
                Dispose(false);
            }
        }
    }
}
