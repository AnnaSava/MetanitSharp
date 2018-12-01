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
                        GarbageCollectorDemo.Display();
                        break;
                    case 'f':
                        FinalizeAndDispose.Display();
                        break;
                    case 'c':
                        FinalizeAndDisposeTemplate.Display();
                        break;
                    case 'u':
                        usingUnsafe();
                        pointerOperations();
                        pointerOperations2();
                        pointerToPointer();
                        break;
                    case 'p':
                        ComplexPointers.Display();
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
            Console.WriteLine("U - небезопасный код");
            Console.WriteLine("Р - указатели на структуры, массивы, классы");
            Console.WriteLine("X - выход из раздела");
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
}
