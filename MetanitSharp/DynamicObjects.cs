using MetanitSharpIronPyton;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class DynamicObjects
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
                    case 'o':
                        usingDynamic();
                        break;
                    case 'e':
                        usingExpandoObject();
                        break;
                    case 'd':
                        usingDynamicObject();
                        break;
                    case 'p':
                        IronPytonDemo.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("O - динамические объекты");
            Console.WriteLine("E - ExpandoObject");
            Console.WriteLine("D - DynamicObject");
            Console.WriteLine("P - IronPyton");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingDynamic()
        {
            dynamic x = 3; // здесь x - целочисленное int
            Console.WriteLine(x);

            x = "Привет мир"; // x - строка
            Console.WriteLine(x);

            x = new Person() { Name = "Tom", Age = 23 }; // x - объект Person
            Console.WriteLine(x);

            PersonDyn person1 = new PersonDyn() { Name = "Том", Age = 27 };
            Console.WriteLine(person1);
            Console.WriteLine(person1.getSalary(28.09, "int"));

            dynamic person2 = new PersonDyn() { Name = "Билл", Age = "Двадцать два года" };
            Console.WriteLine(person2);
            Console.WriteLine(person2.getSalary(30, "string"));
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public override string ToString()
            {
                return Name + ", " + Age.ToString();
            }
        }

        class PersonDyn
        {
            public string Name { get; set; }
            public dynamic Age { get; set; }

            // выводим зарплату в зависимости от переданного формата
            public dynamic getSalary(dynamic value, string format)
            {
                if (format == "string")
                {
                    return value + " рублей";
                }
                else if (format == "int")
                {
                    return value;
                }
                else
                {
                    return 0.0;
                }
            }

            public override string ToString()
            {
                return Name + ", " + Age.ToString();
            }
        }

        static void usingExpandoObject()
        {
            dynamic viewbag = new ExpandoObject();
            viewbag.Name = "Tom";
            viewbag.Age = 46;
            viewbag.Languages = new List<string> { "english", "german", "french" };
            Console.WriteLine("{0} - {1}", viewbag.Name, viewbag.Age);
            foreach (var lang in viewbag.Languages)
                Console.WriteLine(lang);

            // объявляем метод
            viewbag.IncrementAge = (Action<int>)(x => viewbag.Age += x);
            viewbag.IncrementAge(6); // увеличиваем возраст на 6 лет
            Console.WriteLine("{0} - {1}", viewbag.Name, viewbag.Age);

            viewbag.IncrementAge = "No method here anymore";
            Console.WriteLine(viewbag.IncrementAge);

            try
            {
                viewbag.IncrementAge(6);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void usingDynamicObject()
        {
            dynamic person = new PersonObject();
            person.Name = "Tom";
            person.Age = 23;
            Func<int, int> Incr = delegate (int x) { person.Age += x; return person.Age; };
            person.IncrementAge = Incr;
            Console.WriteLine("{0} - {1}", person.Name, person.Age); // Tom - 23
            person.IncrementAge(4);
            Console.WriteLine("{0} - {1}", person.Name, person.Age); // Tom - 27
        }

        class PersonObject : DynamicObject
        {
            Dictionary<string, object> members = new Dictionary<string, object>();

            // установка свойства
            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                members[binder.Name] = value;
                return true;
            }
            // получение свойства
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = null;
                if (members.ContainsKey(binder.Name))
                {
                    result = members[binder.Name];
                    return true;
                }
                return false;
            }
            // вызов метода
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                dynamic method = members[binder.Name];
                result = method((int)args[0]);
                return result != null;
            }
        }
    }
}
