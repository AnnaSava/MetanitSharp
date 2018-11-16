using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MetanitSharp
{
    namespace AccountSpace
    {
        class Account
        {
            public int Id { get; private set; }
            public Account(int _id)
            {
                Id = _id;
            }

            public void Display()
            {
                Console.WriteLine($"Account Id={Id}");
            }
        }

        namespace ClientSpace
        {
            class Client
            {
                public int Id { get; private set; }
                public Client(int _id)
                {
                    Id = _id;
                }

                public void Display()
                {
                    Console.WriteLine($"Client Id={Id}");
                }
            }

            namespace VipSpace
            {
                class VipClient
                {
                    public int Id { get; private set; }
                    public VipClient(int _id)
                    {
                        Id = _id;
                    }

                    public static void DisplayVip(VipClient client)
                    {
                        Console.WriteLine($"VipClient Id={client.Id}");
                    }
                }
            }
        }
    }

    namespace StaticSpace
    {
        class Message
        {
            public static void ShowMyMessage(String text)
            {
                Console.WriteLine(text);
            }
        }
    }

    class Geometry
    {
        public static double GetArea(double radius)
        {
            return PI * radius * radius; // Math.PI
        }
    }

    namespace ExtentionMethodDemo
    {
        public static class StringExtension
        {
            public static int CharCount(this string str, char c)
            {
                int counter = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == c)
                        counter++;
                }
                return counter;
            }

            public static T To<T>(this string text)
            {
                try
                {
                    return (T)Convert.ChangeType(text, typeof(T));
                }
                catch
                {
                    return default(T);
                }
            }

        }
    }

    class AnonymousTypes
    {
        public static void Display()
        {
            var user = new { Name = "Tom", Age = 34 };
            var student = new { Name = "Alice", Age = 21 };
            var manager = new { Name = "Bob", Age = 26, Company = "Microsoft" };

            Console.WriteLine(user.GetType().Name); // <>f__AnonymousType0'2
            Console.WriteLine(student.GetType().Name); // <>f__AnonymousType0'2
            Console.WriteLine(manager.GetType().Name); // <>f__AnonymousType1'3

            User tom = new User { Name = "Tom" };
            int age = 34;
            var person = new { tom.Name, age }; // инициализатор с проекцией
            Console.WriteLine(person.Name);
            Console.WriteLine(person.age);

            var people = new[]
            {
                new { Name = "Tom" },
                new { Name = "Bob" }
            };
            foreach (var p in people)
            {
                Console.WriteLine(p.Name);
            }

            Console.WriteLine("Передача анонимного объекта через параметры метода");
            DataFromAnon(tom);

            var tommy = new { Name = "Tom", Age = 34 };
            DataFromAnonDynamic(tommy);
        }

        // Пример из комментов, с рефлексией
        static void DataFromAnon(object o)
        {
            Console.WriteLine(o.GetType().GetProperty("Name").GetValue(o, null));
        }

        // Пример из комментов, с ключевым словом dynamic
        static void DataFromAnonDynamic(dynamic parameter)
        {
            Console.WriteLine(parameter.Name + " " + parameter.Age);
        }

        class User
        {
            public string Name { get; set; }
        }
    }

    class LocalFunction
    {
        public static void Display()
        {
            var result = SumOfPositive(new int[] { -3, -2, -1, 0, 1, 2, 3 });
            Console.WriteLine(result);  // 6
            result = SumOfNegative(new int[] { -3, -2, -1, 0, 1, 2, 3 });
            Console.WriteLine(result);  // -6

            var concat = ConcatNumbers(new int[] { -3, -2, -1, 0, 1, 2, 3 });
            Console.WriteLine(concat);

            result = SumOfPositiveFunc(new int[] { -3, -2, -1, 0, 1, 2, 3 });
            Console.WriteLine(result);  // 6
        }

        static int SumOfPositive(int[] numbers)
        {
            int limit = 0;
            // локальная функция
            bool IsMoreThan(int number)
            {
                return number > limit;
            }

            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (IsMoreThan(numbers[i]))
                {
                    result += numbers[i];
                }
            }

            return result;
        }

        static int SumOfNegative(int[] numbers)
        {
            int limit = 0;

            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (IsLessThan(numbers[i]))
                {
                    result += numbers[i];
                }
            }

            // локальная функция
            bool IsLessThan(int number)
            {
                return number < limit;
            }

            return result;
        }

        static String ConcatNumbers(int[] numbers)
        {
            string result = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                Concat(numbers[i]);
            }
            return result;

            void Concat(int number)
            {
                result += number.ToString();
            }
        }

        // Пример из комментов и использованием делегата Func
        static int SumOfPositiveFunc(int[] numbers)
        {
            int limit = 0;

            Func<int, bool> IsMoreThan = number => number > limit;

            int result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (IsMoreThan(numbers[i]))
                {
                    result += numbers[i];
                }
            }

            return result;
        }
    }

    class PatternMatching
    {
        public static void Display()
        {
            Employee emp = new Manager(); //Employee();
            UseEmployee(emp);
            UseEmployeeSwitch(emp);
            UseEmployeeSwitchWhen(emp);

            Employee emp2 = new Employee();
            UseEmployee(emp2);
            UseEmployeeSwitch(emp2);
            UseEmployeeSwitchWhen(emp2);

            Employee emp3 = null;
            UseEmployee(emp3);
            UseEmployeeSwitch(emp3);
            UseEmployeeSwitchWhen(emp3);

            Employee emp4 = new Manager(true);
            UseEmployee(emp4);
            UseEmployeeSwitch(emp4);
            UseEmployeeSwitchWhen(emp4);
        }

        static void UseEmployee(Employee emp)
        {
            if (emp is Manager manager && manager.IsOnVacation == false)
            {
                manager.Work();
            }
            else
            {
                Console.WriteLine("Преобразование не допустимо");
            }
        }

        static void UseEmployeeSwitch(Employee emp)
        {
            switch (emp)
            {
                case Manager manager:
                    manager.Work();
                    break;
                case null:
                    Console.WriteLine("Объект не задан");
                    break;
                default:
                    Console.WriteLine("Объект не менеджер");
                    break;
            }
        }

        static void UseEmployeeSwitchWhen(Employee emp)
        {
            switch (emp)
            {
                case Manager manager when manager.IsOnVacation == false:
                    manager.Work();
                    break;
                case Manager manager when manager.IsOnVacation == true:
                    Console.WriteLine("Менеджер в отпуске!");
                    break;
                case null:
                    Console.WriteLine("Объект не задан");
                    break;
                default:
                    Console.WriteLine("Объект не менеджер");
                    break;
            }
        }

        class Employee
        {
            public virtual void Work()
            {
                Console.WriteLine("Да работаю я, работаю");
            }
        }

        class Manager : Employee
        {
            public override void Work()
            {
                Console.WriteLine("Отлично, работаем дальше");
            }
            public bool IsOnVacation { get; set; }

            public Manager() { }

            public Manager(bool isOnVacation)
            {
                IsOnVacation = isOnVacation;
            }
        }
    }

    class DeconstructorDemo
    {
        public static void Display()
        {
            Person person = new Person { Name = "Tom", Age = 33 };

            (string name, int age) = person;

            Console.WriteLine(name);    // Tom
            Console.WriteLine(age);     // 33

            person.Deconstruct(out string _name, out int _age);

            Console.WriteLine(_name);    // Tom
            Console.WriteLine(_age);     // 33
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Deconstruct(out string name, out int age)
            {
                name = this.Name;
                age = this.Age;
            }
        }
    }

    class NullableTypes
    {
        public static void Display()
        {
            demo();
            checkIfNull();
            valueOrDefault();
            equality();
            casting();
        }

        static void demo()
        {
            int? x = 7;
            Console.WriteLine(x.Value); // 7
            Nullable<State> state = new State() { Name = "Narnia" };
            Console.WriteLine(state.Value.Name);    // Narnia
        }

        static void checkIfNull()
        {
            int? x = null;
            if (x.HasValue)
                Console.WriteLine(x.Value);
            else
                Console.WriteLine("x is equal to null");

            State? state = null;
            if (state.HasValue)
                Console.WriteLine(state.Value.Name);
            else
                Console.WriteLine("state is equal to null");
        }

        static void valueOrDefault()
        {
            int? figure = null;
            Console.WriteLine(figure.GetValueOrDefault()); // по умолчанию используется 0
            Console.WriteLine(figure.GetValueOrDefault(10)); // по умолчанию используется 10
        }

        static void equality()
        {
            int? x1 = null;
            int? x2 = null;
            if (x1 == x2)
                Console.WriteLine("объекты равны");
            else
                Console.WriteLine("объекты не равны");
        }

        static void casting()
        {
            // явное преобразование от T? к T
            int? x1 = null;
            if (x1.HasValue)
            {
                int x2 = (int)x1;
                Console.WriteLine(x2);
            }

            // неявное преобразование от T к T?
            int y1 = 4;
            int? y2 = y1;
            Console.WriteLine(y2);

            // неявные расширяющие преобразования от V к T?
            int z1 = 4;
            long? z2 = z1;
            Console.WriteLine(z2);

            // явные сужающие преобразования от V к T?
            long m1 = 4;
            int? m2 = (int?)m1;
            Console.WriteLine(m2);

            // Подобным образом работают преобразования от V? к T?
            long? n1 = 4;
            int? n2 = (int?)n1;
            Console.WriteLine(n2);

            // явные преобразования от V? к T
            long? k1 = 4;
            int k2 = (int)k1;
            Console.WriteLine(k2);
        }

        struct State
        {
            public string Name { get; set; }
        }
    }

    class ReferenceVars
    {
        public static void Display()
        {
            demoRef();
            changeRef();
            returnRef();
        }

        static void demoRef()
        {
            int x = 5;
            ref int xRef = ref x;
            Console.WriteLine(x); // 5
            xRef = 125;
            Console.WriteLine(x); // 125
            x = 625;
            Console.WriteLine(xRef); // 625
        }

        static void changeRef()
        {
            int a = 5;
            int b = 8;
            ref int pointer = ref a;
            pointer = 34;
            //pointer = ref b;    // до версии 7.3 так делать было нельзя
            //pointer = 6;
        }

        static void returnRef()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
            ref int numberRef = ref Find(4, numbers); // ищем число 4 в массиве
            numberRef = 9; // заменяем 4 на 9
            Console.WriteLine(numbers[3]); // 9

            int a = 5;
            int b = 8;
            ref int pointer = ref Max(ref a, ref b);
            pointer = 34;   // меняем значением максимального числа
            Console.WriteLine($"a: {a}  b: {b}"); // a: 5   b: 34
        }

        static ref int Find(int number, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                {
                    return ref numbers[i]; // возвращаем ссылку на адрес, а не само значение
                }
            }
            throw new IndexOutOfRangeException("число не найдено");
        }

        static ref int Max(ref int n1, ref int n2)
        {
            if (n1 > n2)
                return ref n1;
            else
                return ref n2;
        }
    }
}
