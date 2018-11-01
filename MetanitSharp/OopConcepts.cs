using MetanitSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    public static class OopConcepts
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
                    case 's':
                        printStructAndClass();
                        break;
                    case 'c':
                        printCopiedStructAndClass();
                        break;
                    case 'r':
                        printMixedValRef();
                        break;
                    case 'a':
                        printArrays();
                        break;
                    case 'm':
                        printModifiers();
                        break;
                    case 'p':
                        usingProperties();
                        break;
                    case 't':
                        printStatic();
                        break;
                    case 'n':
                        printConst();
                        break;
                    case 'o':
                        usingOperatorsOverloading();
                        break;
                    case 'u':
                        checkingNulls();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("S - вывод данных структуры и класса");
            Console.WriteLine("С - копирование структур и классов");
            Console.WriteLine("R - структуры внутри классов и классы внутри структур");
            Console.WriteLine("A - массивы внутри классов и структур");
            Console.WriteLine("M - вывод полей с разными модификаторами доступа");
            Console.WriteLine("P - вывод значений свойств объекта");
            Console.WriteLine("T - работа со статическими методами");
            Console.WriteLine("N - вывод констант");
            Console.WriteLine("O - перегрузка операторов");
            Console.WriteLine("U - проверка на null");
            Console.WriteLine("X - выход из раздела");
        }

        #region Struct and Class

        static void printStructAndClass()
        {
            var userStruct = new UserStruct("Ann", 18);
            Console.WriteLine("Структура");
            userStruct.DisplayInfo(); // Name: Ann  Age: 18
            changeStruct(userStruct);
            userStruct.DisplayInfo(); // Name: Ann  Age: 18
            changeStruct(ref userStruct);
            userStruct.DisplayInfo(); //Name: Jane  Age: 34

            var userStruct2 = new UserStruct { name = "Sam", age = 31 };
            userStruct2.DisplayInfo();
            var userStruct3 = new UserStruct();
            userStruct3.DisplayInfo(); //Name:   Age: 0
            userStruct3.name = "Tom";
            userStruct3.age = 68;
            userStruct3.DisplayInfo(); //Name: Tom  Age: 68

            var userClass = new UserClass("Ann", 18);
            Console.WriteLine("Класс");
            userClass.DisplayInfo(); // Name: Ann  Age: 18
            changeClass(userClass);
            userClass.DisplayInfo(); // Name: Tarja  Age: 41
            changeClassRef(ref userClass);
            userClass.DisplayInfo(); // Name: Andrew  Age: 42
        }

        static void printCopiedStructAndClass()
        {
            Console.WriteLine("Структура");
            var userStruct1 = new UserStruct();
            var userStruct2 = new UserStruct("Kate", 14);
            userStruct1 = userStruct2;
            userStruct2.age = 16;
            userStruct1.DisplayInfo();
            userStruct2.DisplayInfo();

            Console.WriteLine("Класс");
            var userClass1 = new UserClass();
            var userClass2 = new UserClass("Kate", 14);
            userClass1 = userClass2;
            userClass2.age = 16;
            userClass1.DisplayInfo();
            userClass2.DisplayInfo();
        }

        class UserClass
        {
            public string name;
            public int age;

            public UserClass() { }
            public UserClass(string name, int age)
            {
                this.name = name;
                this.age = age;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"Name: {name}  Age: {age}");
            }
        }

        struct UserStruct
        {
            public string name;
            public int age;
            public UserStruct(string name, int age)
            {
                this.name = name;
                this.age = age;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"Name: {name}  Age: {age}");
            }
        }

        static void changeStruct(UserStruct user)
        {
            user.name = "Alice";
            user.age = 25;

            Console.WriteLine("\tPrint from method");
            Console.Write("\t");
            user.DisplayInfo();
            Console.WriteLine("\tEnd print from method");
        }

        static void changeStruct(ref UserStruct user)
        {
            user.name = "Jane";
            user.age = 34;

            Console.WriteLine("\tPrint from method");
            Console.Write("\t");
            user.DisplayInfo();
            Console.WriteLine("\tEnd print from method");
        }

        static void changeClass(UserClass user)
        {
            user.name = "Tarja";
            user.age = 41;

            Console.WriteLine("\tPrint from method");
            Console.Write("\t");
            user.DisplayInfo();

            user = new UserClass("Jerry", 22);
            Console.Write("\t");
            user.DisplayInfo();

            Console.WriteLine("\tEnd print from method");
        }

        static void changeClassRef(ref UserClass user)
        {
            user.name = "Mary";
            user.age = 27;

            Console.WriteLine("\tPrint from method");
            Console.Write("\t");
            user.DisplayInfo();

            user = new UserClass("Andrew", 42);
            Console.Write("\t");
            user.DisplayInfo();

            Console.WriteLine("\tEnd print from method");
        }

        #endregion

        #region Struct and Class Part 2

        static void printMixedValRef()
        {
            printBags();
            Console.WriteLine();
            printBoxes();
        }

        static void printBags()
        {
            var bag1 = new BagStruct();

            var bag2 = new BagStruct();
            bag2.weight = 3;
            bag2.color = "Red";
            bag2.apple = new AppleClass() { size = 2 };

            bag1 = bag2;

            Console.WriteLine("Создали Bag2, присвоили его Bag1");
            bag1.DisplayInfo("Bag1");
            bag2.DisplayInfo("Bag2");

            bag2.weight = 4;
            bag2.color = "Blue";
            bag2.apple.size = 3;

            Console.WriteLine("\nИзменили у Bag2 вес, цвет и размер яблока");
            bag1.DisplayInfo("Bag1");
            bag2.DisplayInfo("Bag2");

            bag1.apple.size = 6;
            bag1.color = "Yellow";
            bag1.weight = 8;

            Console.WriteLine("\nИзменили у Bag1 вес, цвет и размер яблока");
            bag1.DisplayInfo("Bag1");
            bag2.DisplayInfo("Bag2");

            bag2.apple = new AppleClass() { size = 5 };
            bag2.color = "Green";
            bag2.weight = 6;

            Console.WriteLine("\nИзменили у Bag2 вес, цвет и яблоко целиком");
            bag1.DisplayInfo("Bag1");
            bag2.DisplayInfo("Bag2");
        }

        static void printBoxes()
        {
            var box1 = new BoxClass();

            var box2 = new BoxClass();
            box2.volume = 5;
            box2.shape = "Cube";
            box2.banana = new BananaStruct { length = 13 };

            box1 = box2;

            Console.WriteLine("Создали Box2, присвоили его Box1");
            box1.DisplayInfo("Box1");
            box2.DisplayInfo("Box2");

            box2.volume = 6;
            box2.shape = "Cylinder";
            box2.banana.length = 14;

            Console.WriteLine("\nИзменили у Box2 объем, форму и длину банана");
            box1.DisplayInfo("Box1");
            box2.DisplayInfo("Box2");

            box2.volume = 7;
            box2.shape = "Pyramid";
            box2.banana = new BananaStruct { length = 16 };

            Console.WriteLine("\nИзменили у Box2 объем, форму и банан целиком");
            box1.DisplayInfo("Box1");
            box2.DisplayInfo("Box2");

            box1.volume = 8;
            box1.shape = "Parallelepiped";
            box1.banana = new BananaStruct { length = 15 };

            Console.WriteLine("\nИзменили у Box1 объем, форму и банан целиком");
            box1.DisplayInfo("Box1");
            box2.DisplayInfo("Box2");
        }

        struct BagStruct
        {
            public int weight;
            public string color;
            public AppleClass apple;

            public void DisplayInfo(String instanceName)
            {
                Console.WriteLine($"{instanceName}: weight={weight} color={color} apple.size={apple.size}");
            }
        }

        class AppleClass
        {
            public int size;
        }

        class BoxClass
        {
            public int volume;
            public string shape;
            public BananaStruct banana;

            public void DisplayInfo(String instanceName)
            {
                Console.WriteLine($"{instanceName}: volume={volume} shape={shape} banana.length={banana.length}");
            }
        }

        struct BananaStruct
        {
            public int length;
        }

        #endregion

        #region Arrays in Class and Struct

        static void printArrays()
        {
            Console.WriteLine("Структура");
            var numStruct1 = new NumStruct();
            var numStruct2 = new NumStruct();
            numStruct2.integers = new int[] { 1, 2, 3, 4, 5 };
            numStruct1 = numStruct2;

            printIntegers(numStruct1.integers);
            printIntegers(numStruct2.integers);

            changeIntegers(numStruct2.integers);
            printIntegers(numStruct1.integers);
            printIntegers(numStruct2.integers);

            numStruct2.integers = new int[] { 3, 5, 7, 9 };
            printIntegers(numStruct1.integers);
            printIntegers(numStruct2.integers);

            Console.WriteLine("Класс");
            var numClass1 = new NumClass();
            var numClass2 = new NumClass();
            numClass2.integers = new int[] { 1, 2, 3, 4, 5 };
            numClass1 = numClass2;

            printIntegers(numClass1.integers);
            printIntegers(numClass2.integers);

            changeIntegers(numClass2.integers);
            printIntegers(numClass1.integers);
            printIntegers(numClass2.integers);

            numClass2.integers = new int[] { 1, 3, 5, 7 };
            printIntegers(numClass1.integers);
            printIntegers(numClass2.integers);
        }

        struct NumStruct
        {
            public int[] integers;
        }

        class NumClass
        {
            public int[] integers;
        }

        static void printIntegers(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write($"{nums[i]} ");
            }
            Console.WriteLine();
        }

        static void changeIntegers(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] *= 2;
            }
        }

        #endregion

        #region Modifiers

        static void printModifiers()
        {
            //Вывод внутри класса
            new Modifiers().PrintModifiersFields();

            //Вывод внутри класса-наследника в той же сборке
            new ModifiersInternalInheritor().PrintInternalInheritorModifiers();

            //Вывод в другом классе той же сборки
            new ModifiersInternalExample().PrintModifiers();

            //Вывод внутри класса-наследника в другой сборке
            new ModifiersExternalInheritor().PrintExternalInheritorModifiers();

            //Вывод в другом классе другой сборки
            new ModifiersExternalExample().PrintModifiers();
        }

        class ModifiersExternalInheritor : Modifiers
        {
            public void PrintExternalInheritorModifiers()
            {
                Console.WriteLine("Вывод внутри класса-наследника в другой сборке");
                Console.WriteLine($".Protected = {Protected}");
                Console.WriteLine($".ProtectedInternal = {ProtectedInternal}");
                Console.WriteLine($".Public = {Public}");
                Console.WriteLine();
            }
        }

        class ModifiersExternalExample
        {
            public void PrintModifiers()
            {
                var modifiers = new Modifiers();

                Console.WriteLine("Вывод в другом классе другой сборки");
                Console.WriteLine($".Public = {modifiers.Public}");
                Console.WriteLine();
            }
        }

        #endregion

        #region Properties

        static void usingProperties()
        {
            var person = new PersonWithProps();
            person.Name = "Lucy";
            person.Age = 20;
            person.Secret = -1;
            person.Country = "Middle Earth";

            person.DisplayInfo();

            person.Age = 10;

            person.CountryCode = 25;

            person.DisplayInfo();
        }

        class PersonWithProps
        {
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }

            private int age;
            public int Age
            {
                set
                {
                    if (value < 18)
                    {
                        Console.WriteLine("Возраст должен быть больше 17");
                    }
                    else
                    {
                        age = value;
                    }
                }
                get { return age; }
            }

            private string birthday;
            // свойство только для чтения
            public string Birthday
            {
                get
                {
                    return birthday;
                }
            }

            private int secret;
            // свойство только для записи
            public int Secret
            {
                set
                {
                    secret = value;
                }
            }

            private int money;
            public int Money
            {
                get
                {
                    return money;
                }

                private set
                {
                    money = value;
                }
            }

            public string Country { get; set; }

            public int CountryCode { get; set; } = 23;

            public string State { private set; get; }

            public string City { get; } = "Moscow";

            private string street;

            // эквивалентно public string Street { get { return street; } }
            public string Street => street;

            public PersonWithProps()
            {
                street = "Lenina";
                State = "Alabama";
                Money = 1000;
                birthday = "1 Jan 1970";
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"Name {Name} age {Age} birthday {Birthday}");
                Console.WriteLine($"Secret is {secret}");
                Console.WriteLine($"Count of money {Money}$");
                Console.WriteLine($"{CountryCode} {Country}, {State} state, {City}, {Street} street");
            }
        }

        #endregion

        #region Static and Constants

        static void printStatic()
        {
            var sc1 = new StaticConstructor();
            var sc2 = new StaticConstructor();
            var sc3 = new StaticConstructor();
            var sc4 = new StaticConstructor();
            var sc5 = new StaticConstructor();
            var sc6 = new StaticConstructor();
            var sc7 = new StaticConstructor();

            StaticConstructor.DisplayCounter();
        }

        class StaticConstructor
        {
            static StaticConstructor()
            {
                Console.WriteLine("Создан первый объект со статическим конструктором");
            }

            private static int counter = 0;
            public StaticConstructor()
            {
                counter++;
            }

            public static void DisplayCounter()
            {
                Console.WriteLine($"Создано {counter} объектов StaticConstructor");
            }
        }

        static void printConst()
        {
            const int hundred = 100;
            Console.WriteLine($"Локальная константа {hundred}");

            Console.WriteLine($"Константа Пи в классе {MathLib.PI}");

            Console.WriteLine($"Статическое поле только для чтения, первое обращение {MathLib.M}");

            var math = new MathLib(45);
            Console.WriteLine($"Нестатическое поле только для чтения {math.K}");

            Console.WriteLine($"Статическое поле только для чтения после создания объекта {MathLib.M}");
        }

        class MathLib
        {
            public const double PI = 3.141;
            public const double E = 2.81;

            public readonly int K = 23;

            public static readonly int M = 101;

            public MathLib(int _k)
            {
                Console.WriteLine($"Нестатическое поле только для чтения - конструктор {K}");
                K = _k; // поле для чтения может быть инициализировано или изменено в конструкторе после компиляции
            }

            static MathLib()
            {
                Console.WriteLine($"Статическое поле только для чтения - конструктор {M}");
                M = 102;
            }
        }

        #endregion

        #region Operators

        static void usingOperatorsOverloading()
        {
            Counter c1 = new Counter { Value = 23 };
            Counter c2 = new Counter { Value = 45 };
            bool result = c1 > c2;
            Console.WriteLine(result); // false

            Counter c3 = c1 + c2;
            Console.WriteLine(c3.Value);  // 23 + 45 = 68

            int d = c1 + 27; // 50
            Console.WriteLine(d);

            Counter cPreInc = new Counter() { Value = 10 };
            Console.WriteLine($"{cPreInc.Value}");      // 10
            Console.WriteLine($"{(++cPreInc).Value}");  // 20
            Console.WriteLine($"{cPreInc.Value}");      // 20

            Counter cPostInc = new Counter() { Value = 10 };
            Console.WriteLine($"{cPostInc.Value}");      // 10
            Console.WriteLine($"{(cPostInc++).Value}");  // 10
            Console.WriteLine($"{cPostInc.Value}");      // 20

            CounterTrueFalse counter = new CounterTrueFalse() { Value = 0 };
            if (counter)
                Console.WriteLine(true);
            else
                Console.WriteLine(false);
        }

        class Counter
        {
            public int Value { get; set; }

            public static Counter operator +(Counter c1, Counter c2)
            {
                return new Counter { Value = c1.Value + c2.Value };
            }
            public static int operator +(Counter c1, int val)
            {
                return c1.Value + val;
            }
            public static bool operator >(Counter c1, Counter c2)
            {
                return c1.Value > c2.Value;
            }
            public static bool operator <(Counter c1, Counter c2)
            {
                return c1.Value < c2.Value;
            }
            public static Counter operator ++(Counter c1)
            {
                return new Counter { Value = c1.Value + 10 };
            }
        }

        class CounterTrueFalse
        {
            public int Value { get; set; }

            public static bool operator true(CounterTrueFalse c1)
            {
                return c1.Value != 0;
            }
            public static bool operator false(CounterTrueFalse c1)
            {
                return c1.Value == 0;
            }
        }

        #endregion

        #region Nulls

        static void checkingNulls()
        {
            User user = new User();
            string companyName = user?.Phone?.Company?.Name ?? "не установлено";
            Console.WriteLine(companyName);
        }

        class User
        {
            public Phone Phone { get; set; }
        }

        class Phone
        {
            public Company Company { get; set; }
        }

        class Company
        {
            public string Name { get; set; }
        }

        #endregion
    }
}
