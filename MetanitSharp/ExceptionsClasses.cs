using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class OwnExceptionDemo
    {
        public static void Display()
        {
            try
            {
                Person p = new Person { Name = "Tom", Age = 17 };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        class Person
        {
            private int age;
            public string Name { get; set; }
            public int Age
            {
                get { return age; }
                set
                {
                    if (value < 18)
                    {
                        throw new Exception("Лицам до 18 регистрация запрещена");
                    }
                    else
                    {
                        age = value;
                    }
                }
            }
        }
    }

    class OwnExceptionClass
    {
        public static void Display()
        {
            try
            {
                Person p = new Person { Name = "Tom", Age = 17 };
            }
            catch (PersonException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        class Person
        {
            private int age;
            public string Name { get; set; }
            public int Age
            {
                get { return age; }
                set
                {
                    if (value < 18)
                        throw new PersonException("Лицам до 18 регистрация запрещена");
                    else
                        age = value;
                }
            }
        }

        class PersonException : Exception
        {
            public PersonException(string message)
                : base(message)
            { }
        }
    }

    class OwnExceptionClassVal
    {
        public static void Display()
        {
            try
            {
                Person p = new Person { Name = "Tom", Age = 13 };
            }
            catch (PersonException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Некорректное значение: {ex.Value}");
            }
        }

        class Person
        {
            public string Name { get; set; }
            private int age;
            public int Age
            {
                get { return age; }
                set
                {
                    if (value < 18)
                        throw new PersonException("Лицам до 18 регистрация запрещена", value);
                    else
                        age = value;
                }
            }
        }

        class PersonException : ArgumentException
        {
            public int Value { get; }
            public PersonException(string message, int val)
                : base(message)
            {
                Value = val;
            }
        }
    }

    class SearchCatch
    {
        public static void Display()
        {
            try
            {
                TestClass.Method1();
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Catch в Display : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally в Display");
            }
            Console.WriteLine("Конец метода Display");
        }

        class TestClass
        {
            public static void Method1()
            {
                try
                {
                    Method2();
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Catch в Method1 : {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Блок finally в Method1");
                }
                Console.WriteLine("Конец метода Method1");
            }
            static void Method2()
            {
                try
                {
                    int x = 8;
                    int y = x / 0;
                }
                finally
                {
                    Console.WriteLine("Блок finally в Method2");
                }
                Console.WriteLine("Конец метода Method2");
            }
        }
    }
}
