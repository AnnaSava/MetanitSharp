using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharpUnsafe
{
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
