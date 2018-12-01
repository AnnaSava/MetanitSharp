using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharpMemory
{
    public class GarbageCollectorDemo
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

    public class FinalizeAndDispose
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

    public class FinalizeAndDisposeTemplate
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

    public class Pointers
    {
        public static void Display()
        {
            usingUnsafe();
            pointerOperations();
            pointerOperations2();
            pointerToPointer();
        }

        static void usingUnsafe()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y
                Console.WriteLine(*x); // 10

                y = y + 20;
                Console.WriteLine(*x);// 30

                *x = 50;
                Console.WriteLine(y); // переменная y=50                
            }
        }

        static void pointerOperations()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную
                x = &y; // указатель x теперь указывает на адрес переменной y

                // получим адрес переменной y
                uint addr = (uint)x;
                Console.WriteLine("Адрес переменной y: {0}", addr);

                byte* bytePointer = (byte*)(addr + 4); // получить указатель на следующий байт после addr
                Console.WriteLine("Значение byte по адресу {0}: {1}", addr + 4, *bytePointer);

                // обратная операция
                uint oldAddr = (uint)bytePointer - 4; // вычитаем четыре байта, так как bytePointer - указатель на байт
                int* intPointer = (int*)oldAddr;
                Console.WriteLine("Значение int по адресу {0}: {1}", oldAddr, *intPointer);

                // преобразование в тип double
                double* doublePointer = (double*)(addr + 4);
                Console.WriteLine("Значение double по адресу {0}: {1}", addr + 4, *doublePointer);

                float* floatPointer = (float*)addr;
                Console.WriteLine("Значение float по адресу {0}: {1}", addr, *floatPointer);
            }
        }

        static void pointerOperations2()
        {
            unsafe
            {
                char* charPointer = (char*)123000;
                *charPointer = 'A';
                charPointer += 4; // 123008
                Console.WriteLine("Адрес {0}", (uint)charPointer);
                *charPointer = 'B';

                double* doublePointer = (double*)123000;
                doublePointer = doublePointer + 3; // 123024
                Console.WriteLine("Адрес {0}", (uint)doublePointer);

                *doublePointer = 20;
                Console.WriteLine("Значение double по адресу {0}: {1}", (uint)doublePointer, *doublePointer);

                uint addr = (uint)charPointer - 8;
                int* intPointer = (int*)addr;

                Console.WriteLine("Адрес\tint");

                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("{0}\t{1}",
                        (uint)intPointer, *intPointer);
                    intPointer += 1;
                }
            }
        }

        static void pointerToPointer()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y
                int** z = &x; // указатель z теперь указывает на адрес, который указывает на указатель x
                **z = **z + 40; // изменение указателя z повлечет изменение переменной y
                Console.WriteLine($"y={y}"); // переменная y=50
                Console.WriteLine($"**z={**z}"); // переменная **z=50
                Console.WriteLine($"*x={*x}"); // переменная *x=50

                Console.WriteLine("Адрес переменной y={0}", (uint)x);
                Console.WriteLine("Адрес указателя x={0}", (uint)z);
            }
        }
    }

    public class ComplexPointers
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

                foreach (var num in nums)
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
