using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            char key;
            
            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'l':
                        printLiterals();
                        break;
                    case 'v':
                        printVars();
                        break;
                    case 't':
                        printTypes();
                        break;
                    case 'i':
                        printInterpolation();
                        break;
                    case 'e':
                        enterInfo();
                        break;
                    case 'b':
                        printBits();
                        break;
                    case 'c':
                        usingChecked();
                        break;
                    case 'g':
                        usingGotoCase();
                        break;
                    case 'a':
                        printArray();
                        break;
                    case 's':
                        sortArray();
                        break;
                    case 'h':
                        printShortMethods();
                        break;
                    case 'p':
                        usingParams();
                        break;
                    case 'r':
                        printRecursion();
                        break;
                    case 'u':
                        printTuple();
                        break;
                }

                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("L - вывод литералов");
            Console.WriteLine("V - вывод переменных");
            Console.WriteLine("T - вывод типов");
            Console.WriteLine("I - вывод строки с интерполяцией");
            Console.WriteLine("E - ввод данных и их вывод");
            Console.WriteLine("B - вывод результатов побитовых операций");
            Console.WriteLine("C - преобразование типов и checked");
            Console.WriteLine("G - использование goto case");
            Console.WriteLine("A - вывод массивов");
            Console.WriteLine("S - сортировка массива");
            Console.WriteLine("H - вывод из сокращенных методов");
            Console.WriteLine("P - работа с параметрами");
            Console.WriteLine("R - рекурсивное вычисление факториала и числа Фибоначчи");
            Console.WriteLine("U - вывод кортежей");
        }

        static void printLiterals()
        {
            Console.WriteLine("\t Логические литералы");
            Console.WriteLine(true);
            Console.WriteLine(false);

            Console.WriteLine("\t Целочисленные литералы");
            Console.WriteLine(-11);
            Console.WriteLine(5);

            Console.WriteLine("\t Целочисленные в двоичной системе");
            Console.WriteLine(0b11);        // 3
            Console.WriteLine(0b1011);      // 11

            Console.WriteLine("\t Целочисленные в шестнадцатеричной системе");
            Console.WriteLine(0x0A);    // 10
            Console.WriteLine(0xFF);    // 255

            Console.WriteLine("\t Вещественные литералы");
            Console.WriteLine(1.009);
            Console.WriteLine(-0.5);

            Console.WriteLine("\t Вещественные в экспоненциальной форме");
            Console.WriteLine(3.2e3);   // по сути равно 3.2 * 10<sup>3</sup> = 3200
            Console.WriteLine(1.2E-1);  // равно 1.2 * 10<sup>-1</sup> = 0.12

            Console.WriteLine("\t Символьные литералы");
            Console.WriteLine('A');    // x
            Console.WriteLine('N');    // Z

            Console.WriteLine("\t Символьные в ASCII кодах");
            Console.WriteLine('\x78');    // x
            Console.WriteLine('\x5A');    // Z

            Console.WriteLine("\t Символьные в кодах Unicode");
            Console.WriteLine('\u0420');    // Р
            Console.WriteLine('\u0421');    // С

            Console.WriteLine("\t Строковые литералы");
            Console.WriteLine("hello");
            Console.WriteLine("world");
        }

        static void printVars()
        {
            string name = "Tom";
            int age = 33;
            bool isEmployed = false;
            double weight = 78.65;

            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Возраст: {age}");
            Console.WriteLine($"Вес: {weight}");
            Console.WriteLine($"Работает: {isEmployed}");
        }

        static void printTypes()
        {
            var num1 = 5;
            var num2 = 5.1;

            Console.WriteLine(num1.GetType().ToString());
            Console.WriteLine(num2.GetType().ToString());

            float fl1 = 3.14F;
            var fl2 = 9.11F;
            float fl3 = 30.6f;
            var fl4 = 31.7f;

            Console.WriteLine(fl1.GetType().ToString());
            Console.WriteLine(fl2.GetType().ToString());
            Console.WriteLine(fl3.GetType().ToString());
            Console.WriteLine(fl4.GetType().ToString());

            decimal dc1 = 1005.8M;
            var dc2 = 1002.8M;

            Console.WriteLine(dc1.GetType().ToString());
            Console.WriteLine(dc2.GetType().ToString());

            uint ui1 = 10U;
            var ui2 = 11U;

            Console.WriteLine(ui1.GetType().ToString());
            Console.WriteLine(ui2.GetType().ToString());

            long lg1 = 20L;
            var lg2 = 30L;

            Console.WriteLine(lg1.GetType().ToString());
            Console.WriteLine(lg2.GetType().ToString());

            ulong ul1 = 30UL;
            var ul2 = 40UL;

            Console.WriteLine(ul1.GetType().ToString());
            Console.WriteLine(ul2.GetType().ToString());
        }

        static void printInterpolation()
        {
            Console.WriteLine($"Вывод суммированного числа {1 + 2} c помощью интерполяции.");
            var n = 10;
            Console.WriteLine($"Когда к числу {n} прибавили 5, получилось {n + 5}.");
        }

        static void enterInfo()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите рост: ");
            double height = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите размер зарплаты: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine($"Имя: {name}  Возраст: {age}  Рост: {height}м  Зарплата: {salary}$");
        }

        static void printBits()
        {
            int x1 = 2; //010
            int y1 = 5; //101
            Console.WriteLine(x1 & y1); // выведет 0

            int x2 = 4; //100
            int y2 = 5; //101
            Console.WriteLine(x2 | y2); // выведет 5 - 101

            int x = 45; // Значение, которое надо зашифровать - в двоичной форме 101101
            int key = 102; //Пусть это будет ключ - в двоичной форме 1100110
            int encrypt = x ^ key; //Результатом будет число 1001011 или 75
            Console.WriteLine("Зашифрованное число: " + encrypt);

            int decrypt = encrypt ^ key; // Результатом будет исходное число 45
            Console.WriteLine("Расшифрованное число: " + decrypt);

            int x3 = 12;                 // 00001100
            Console.WriteLine(~x3);      // 11110011   или -13

            int x4 = 55;
            Console.WriteLine(x4 << 1);

            int x5 = 55;
            Console.WriteLine(x5 >> 1);
        }

        static void usingChecked()
        {
            try
            {
                int a = 33;
                int b = 600;
                byte c = checked((byte)(a + b));
                Console.WriteLine(c);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void usingGotoCase()
        {
            int number = 1;
            switch (number)
            {
                case 1:
                    Console.WriteLine("case 1");
                    goto case 5; // переход к case 5
                case 3:
                    Console.WriteLine("case 3");
                    break;
                case 5:
                    Console.WriteLine("case 5");
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }

        static void printArray()
        {
            // Двумерный массив
            int[,] arr1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } };

            // Выводит все элементы вне зависимости от размерности
            foreach (int elem in arr1)
                Console.Write($"{elem} ");
            Console.WriteLine();

            //GetUpperBound(dimension) возвращает индекс последнего элемента в определенной размерности
            int rows = arr1.GetUpperBound(0) + 1;
            int columns = arr1.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{arr1[i, j]} \t");
                }
                Console.WriteLine();
            }

            // Массив массивов
            int[][] arr2 = new int[3][];
            arr2[0] = new int[] { 1, 2 };
            arr2[1] = new int[] { 1, 2, 3 };
            arr2[2] = new int[] { 1, 2, 3, 4, 5 };

            foreach (int[] row in arr2)
            {
                foreach (int number in row)
                {
                    Console.Write($"{number} \t");
                }
                Console.WriteLine();
            }

            // перебор с помощью цикла for
            for (int i = 0; i < arr2.Length; i++)
            {
                for (int j = 0; j < arr2[i].Length; j++)
                {
                    Console.Write($"{arr2[i][j]} \t");
                }
                Console.WriteLine();
            }
        }

        static void sortArray()
        {
            // ввод чисел
            int[] nums = new int[7];
            Console.WriteLine("Введите семь чисел");
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write("{0}-е число: ", i + 1);
                nums[i] = Int32.Parse(Console.ReadLine());
            }

            // сортировка
            int temp;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }

            // вывод
            Console.WriteLine("Вывод отсортированного массива");
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
            }
        }

        static void printShortMethods()
        {
            sayHello();

            Console.WriteLine($"Сумма {sum(2, 3)}");
        }

        static void sayHello() => Console.WriteLine("Hello");
        static int sum(int a, int b) => a + b;

        static void usingParams()
        {
            int a = 5;
            Console.WriteLine($"Начальное значение переменной a = {a}");

            //Передача переменных по значению
            //После выполнения этого кода по-прежнему a = 5, так как мы передали лишь ее копию
            incrementVal(a);
            Console.WriteLine($"Переменная a после передачи по значению равна = {a}");

            int b = 5;
            Console.WriteLine($"Начальное значение переменной a  = {b}");
            //Передача переменных по ссылке
            //После выполнения этого кода a = 6, так как мы передали саму переменную
            incrementRef(ref b);
            Console.WriteLine($"Переменная a после передачи ссылке равна = {b}");

            int x = 10;
            // Начиная с версии C# 7.0 можно определять переменные в непосредственно при вызове метода
            getRectData(x, 15, out int area, out int perimeter); 
            Console.WriteLine($"Площадь : {area}");
            Console.WriteLine($"Периметр : {perimeter}");

            addition(1, 2, 3, 4, 5);

            int[] array = new int[] { 1, 2, 3, 4 };
            addition(array);

            addition();
        }

        static void incrementVal(int x)
        {
            x++;
            Console.WriteLine($"IncrementVal: {x}");
        }
        
        static void incrementRef(ref int x)
        {
            x++;
            Console.WriteLine($"IncrementRef: {x}");
        }

        static void getRectData(int x, int y, out int area, out int perim)
        {
            area = x * y;
            perim = (x + y) * 2;
        }

        static void addition(params int[] integers)
        {
            int result = 0;
            for (int i = 0; i < integers.Length; i++)
            {
                result += integers[i];
            }
            Console.WriteLine($"Результат суммирования: {result}");
        }

        static void printRecursion()
        {
            Console.WriteLine($"Факториал числа 6: {factorial(6)}");
            Console.WriteLine($"Десятое число Фибоначчи: {fibonacci(10)}");
        }

        static int factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * factorial(x - 1);
            }
        }

        static int fibonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }
            else
            {
                return fibonacci(n - 1) + fibonacci(n - 2);
            }
        }

        static void printTuple()
        {
            var tuple = (5, 10);
            Console.WriteLine(tuple.Item1); // 5
            Console.WriteLine(tuple.Item2); // 10
            tuple.Item1 += 26;
            Console.WriteLine(tuple.Item1); // 31

            (string, int, double) person = ("Tom", 25, 81.23);
            Console.WriteLine($"Person {person.Item1}, age {person.Item2}, weight {person.Item3}");

            var tuple2 = (count: 5, sum: 10);
            Console.WriteLine(tuple2.count); // 5
            Console.WriteLine(tuple2.sum); // 10

            var tuple3 = getTupleValues();
            Console.WriteLine(tuple3.Item1); // 1
            Console.WriteLine(tuple3.Item2); // 3

            var tuple4 = getTupleNamedValues(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            Console.WriteLine(tuple4.count);
            Console.WriteLine(tuple4.sum);

            var (name, age) = getTuplePerson(("Tom", 23), 12);
            Console.WriteLine(name);    // Tom
            Console.WriteLine(age);     // 35
        }

        private static (int, int) getTupleValues()
        {
            var result = (1, 3);
            return result;
        }

        private static (int sum, int count) getTupleNamedValues(int[] numbers)
        {
            var result = (sum: 0, count: 0);
            for (int i = 0; i < numbers.Length; i++)
            {
                result.sum += numbers[i];
                result.count++;
            }
            return result;
        }

        private static (string name, int age) getTuplePerson((string _name, int _age) tuple, int x)
        {
            var result = (name: tuple._name, age: tuple._age + x);
            return result;
        }
    }
}
