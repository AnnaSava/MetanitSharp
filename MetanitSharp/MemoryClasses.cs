using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class GarbageCollectorDemo
    {
        public static void Display()
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
    }

    class FinalizeAndDispose
    {
        public static void Display()
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
    }

    class FinalizeAndDisposeTemplate
    {
        public static void Display()
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

    class ComplexPointers
    {
        public static void Display()
        {
            structDemo();
            array();
            fixedOperator();
        }

        static void structDemo()
        {
            unsafe
            {
                Person person;
                person.age = 29;
                person.height = 176;
                Person* p = &person;
                p->age = 30;
                Console.WriteLine(p->age);

                // разыменовывание указателя
                (*p).height = 180;
                Console.WriteLine((*p).height);

                p->weight = 76;
                Console.WriteLine((*p).weight);

                Console.WriteLine((ulong)p);
                p += 1;
                Console.WriteLine((ulong)p);
            }
        }

        static void array()
        {
            unsafe
            {
                const int size = 7;
                int* factorial = stackalloc int[size]; // выделяем память в стеке под семь объектов int
                int* p = factorial;

                *(p++) = 1; // присваиваем первой ячейке значение 1 и
                            // увеличиваем указатель на 1
                            // эквивалентно: *p = 1; p++;

                for (int i = 2; i <= size; i++, p++)
                {
                    // считаем факториал числа
                    *p = p[-1] * i;
                }
                for (int i = 1; i <= size; ++i)
                {
                    Console.WriteLine(factorial[i - 1]);
                }
            }
        }

        static void fixedOperator()
        {
            unsafe
            {
                User user = new User();
                user.age = 28;
                user.height = 178;
                // блок фиксации указателя
                fixed (int* p = &user.age)
                {
                    if (*p < 30)
                    {
                        *p = 30;
                    }
                }
                Console.WriteLine(user.age); // 30

                int[] nums = { 0, 1, 2, 3, 7, 88 };
                string str = "Привет мир";
                fixed (int* p = nums)
                {
                    *p = 100;
                    p[4] = 99;
                }

                foreach(var num in nums)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();

                fixed (char* p = str)
                {
                    p[2] = 'ю';
                }
                Console.WriteLine(str);
            }
        }

        public struct Person
        {
            public int age;
            public int height;
            public int weight;
        }

        public class User
        {
            public int age;
            public int height;
        }
    }
}
