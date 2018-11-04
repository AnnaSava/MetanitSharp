using System;

namespace MetanitSharp
{
    class IndexerDemo
    {
        public static void Display()
        {
            People people = new People();
            people[0] = new Person { Name = "Tom" };
            people[1] = new Person { Name = "Bob" };

            Person tom = people[0];
            Console.WriteLine(tom?.Name);
        }

        class Person
        {
            public string Name { get; set; }
        }
        class People
        {
            Person[] data;
            public People()
            {
                data = new Person[5];
            }
            // индексатор
            public Person this[int index]
            {
                get
                {
                    return data[index];
                }
                set
                {
                    data[index] = value;
                }
            }
        }
    }

    class IndexerAttrName
    {
        public static void Display()
        {
            User bob = new User();
            bob["name"] = "Bob";
            bob["email"] = "bobekvilmovskiy@gmail.ru";

            Console.WriteLine(bob["name"]); // Mr/Ms. Bob
        }

        class User
        {
            string name;
            string email;
            string phone;

            public string this[string propname]
            {
                get
                {
                    switch (propname)
                    {
                        case "name": return "Mr/Ms. " + name;
                        case "email": return email;
                        case "phone": return phone;
                        default: return null;
                    }
                }
                set
                {
                    switch (propname)
                    {
                        case "name":
                            name = value;
                            break;
                        case "email":
                            email = value;
                            break;
                        case "phone":
                            phone = value;
                            break;
                    }
                }
            }
        }
    }

    class IndexerMany
    {
        public static void Display()
        {
            Matrix matrix = new Matrix();
            Console.WriteLine(matrix[0, 0]);
            matrix[0, 0] = 111;
            Console.WriteLine(matrix[0, 0]);
        }

        class Matrix
        {
            private int[,] numbers = new int[,] { { 1, 2, 4 }, { 2, 3, 6 }, { 3, 4, 8 } };
            public int this[int i, int j]
            {
                get
                {
                    return numbers[i, j];
                }
                set
                {
                    numbers[i, j] = value;
                }
            }
        }
    }

    class IndexerOverloading
    {
        public static void Display()
        {
            People people = new People();
            people[0] = new Person { Name = "Tom" };
            people[1] = new Person { Name = "Bob" };

            Console.WriteLine(people[0].Name);      // Tom
            Console.WriteLine(people["Bob"].Name);  // Bob
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        class People
        {
            Person[] data;
            public People()
            {
                data = new Person[5];
            }
            public Person this[int index]
            {
                get
                {
                    return data[index];
                }
                set
                {
                    data[index] = value;
                }
            }
            public Person this[string name]
            {
                get
                {
                    Person person = null;
                    foreach (var p in data)
                    {
                        if (p?.Name == name)
                        {
                            person = p;
                            break;
                        }
                    }
                    return person;
                }
            }
        }
    }

    class InheritanceDemo
    {
        public static void Display()
        {
            Person p = new Person { Name = "Tom" };
            p.Display();
            p = new Employee { Name = "Sam" };
            p.Display();

            Person p2 = new Person("Bill");
            p2.Display();
            Employee emp2 = new Employee("Tom", "Microsoft");
            emp2.Display();
        }

        class Person
        {
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public Person() { }
            public Person(string name)
            {
                Name = name;
            }
            public void Display()
            {
                Console.WriteLine(Name);
            }
        }

        class Employee : Person
        {
            public string Company { get; set; }

            public Employee() { }

            public Employee(string name, string company)
                : base(name)
            {
                Company = company;
            }
        }
    }

    class InheritanceConstructors
    {
        public static void Display()
        {
            Employee tom = new Employee("Tom", 22, "Microsoft");
        }

        class Person
        {
            string name;
            int age;

            public Person(string name)
            {
                this.name = name;
                Console.WriteLine("Person(string name)");
            }
            public Person(string name, int age) : this(name)
            {
                this.age = age;
                Console.WriteLine("Person(string name, int age)");
            }
        }
        class Employee : Person
        {
            string company;

            public Employee(string name, int age, string company) : base(name, age)
            {
                this.company = company;
                Console.WriteLine("Employee(string name, int age, string company)");
            }
        }
    }

    class CastingTypes
    {
        public static void Display()
        {
            upcasting();
            downcasting();
            castingAs();
            castingTryCatch();
            castingIs();
        }

        static void upcasting()
        {
            // Upcasting

            Employee employee = new Employee("Tom", "Microsoft");
            Person person = employee;   // преобразование от Employee к Person

            Console.WriteLine(person.Name);

            Person person2 = new Client("Bob", "ContosoBank");   // преобразование от Client к Person

            object person10 = new Employee("Tom", "Microsoft");  // от Employee к object
            object person11 = new Client("Bob", "ContosoBank");  // от Client к object
            object person12 = new Person("Sam");                 // от Person к object
        }

        static void downcasting()
        {
            Employee employee = new Employee("Tom", "Microsoft");
            Person person = employee;   // преобразование от Employee к Person

            // Downcasting

            Employee employee2 = (Employee)person;  // преобразование от Person к Employee

            // Объект Employee также представляет тип object
            object obj = new Employee("Bill", "Microsoft");

            // чтобы обратиться к возможностям типа Employee, приводим объект к типу Employee
            Employee emp = (Employee)obj;

            // преобразование к типу Person для вызова метода Display
            ((Person)obj).Display();
            // либо так
            ((Employee)obj).Display();

            // объект Client также представляет тип Person
            Person person20 = new Client("Sam", "ContosoBank");
            // преобразование от типа Person к Client
            Client client = (Client)person20;
        }

        static void castingAs()
        {
            Person person = new Person("Tom");
            Employee emp = person as Employee;
            if (emp == null)
            {
                Console.WriteLine("Преобразование прошло неудачно");
            }
            else
            {
                Console.WriteLine(emp.Company);
            }
        }

        static void castingTryCatch()
        {
            Person person = new Person("Tom");
            try
            {
                Employee emp = (Employee)person;
                Console.WriteLine(emp.Company);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void castingIs()
        {
            Person person = new Person("Tom");
            if (person is Employee)
            {
                Employee emp = (Employee)person;
                Console.WriteLine(emp.Company);
            }
            else
            {
                Console.WriteLine("Преобразование не допустимо");
            }
        }

        class Person
        {
            public string Name { get; set; }
            public Person(string name)
            {
                Name = name;
            }
            public void Display()
            {
                Console.WriteLine($"Person {Name}");
            }
        }

        class Employee : Person
        {
            public string Company { get; set; }
            public Employee(string name, string company) : base(name)
            {
                Company = company;
            }
        }

        class Client : Person
        {
            public string Bank { get; set; }
            public Client(string name, string bank) : base(name)
            {
                Bank = bank;
            }
        }
    }

    class CastingOverload
    {
        public static void Display()
        {
            intCasting();
            timerCasting();
        }

        static void intCasting()
        {
            Counter counter1 = new Counter { Seconds = 23 };

            int x = (int)counter1;
            Console.WriteLine(x);   // 23

            Counter counter2 = x;
            Console.WriteLine(counter2.Seconds);  // 23
        }

        static void timerCasting()
        {
            Counter counter1 = new Counter { Seconds = 115 };

            Timer timer = counter1;
            Console.WriteLine($"{timer.Hours}:{timer.Minutes}:{timer.Seconds}"); // 0:1:55

            Counter counter2 = (Counter)timer;
            Console.WriteLine(counter2.Seconds);  //115
        }

        class Timer
        {
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public int Seconds { get; set; }
        }
        class Counter
        {
            public int Seconds { get; set; }

            public static implicit operator Counter(int x)
            {
                return new Counter { Seconds = x };
            }

            public static explicit operator int(Counter counter)
            {
                return counter.Seconds;
            }

            public static explicit operator Counter(Timer timer)
            {
                int h = timer.Hours * 3600;
                int m = timer.Minutes * 60;
                return new Counter { Seconds = h + m + timer.Seconds };
            }

            public static implicit operator Timer(Counter counter)
            {
                int h = counter.Seconds / 3600;
                int m = (counter.Seconds - h * 3600) / 60;
                int s = counter.Seconds - h * 3600 - m * 60;
                return new Timer { Hours = h, Minutes = m, Seconds = s };
            }
        }
    }

    class VirtualDemo
    {
        public static void Display()
        {
            Console.WriteLine("Виртуальные методы");
            Person p1 = new Person("Bill", "Gates");
            p1.Display(); // вызов метода Display из класса Person

            Employee p2 = new Employee("Tom", "Smith", "Microsoft");
            p2.Display(); // вызов метода Display из класса Employee

            Client p3 = new Client("Anna", "Domini", "Eternity");
            p3.Display();

            Employee p4 = new Employee("Tom", "Gates", "Gigasoft");
            p4.Display();

            Console.WriteLine("Виртуальные свойства");
            LongCredit credit = new LongCredit { Sum = 6000 };
            credit.Sum = 490;
            Console.WriteLine(credit.Sum);
        }

        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            public virtual void Display()
            {
                Console.WriteLine($"{FirstName} {LastName}");
            }
        }
        class Employee : Person
        {
            public string Company { get; set; }
            public Employee(string firstName, string lastName, string company)
                : base(firstName, lastName)
            {
                Company = company;
            }

            public override void Display()
            {
                Console.WriteLine($"{FirstName} {LastName} работает в {Company}");
            }
        }

        class Client : Person
        {
            public string Company { get; set; }

            public Client(string lastName, string firstName, string company)
                    : base(firstName, lastName)
            {
                Company = company;
            }

            public override void Display()
            {
                base.Display();
                Console.WriteLine($"работает в {Company}");
            }
        }

        class EmployeeSealed : Person
        {
            public string Company { get; set; }

            public EmployeeSealed(string firstName, string lastName, string company)
                        : base(firstName, lastName)
            {
                Company = company;
            }

            public override sealed void Display()
            {
                Console.WriteLine($"{FirstName} {LastName} работает в {Company}");
            }
        }

        class Credit
        {
            public virtual decimal Sum { get; set; }
        }
        class LongCredit : Credit
        {
            private decimal sum;
            public override decimal Sum
            {
                get
                {
                    return sum;
                }
                set
                {
                    if (value > 1000)
                    {
                        sum = value;
                    }
                }
            }
        }
    }

    class HidingDemo
    {
        public static void Display()
        {
            Person bob = new Person("Bob", "Robertson");
            bob.Display();      // Bob Robertson

            Employee tom = new Employee("Tom", "Smith", "Microsoft");
            tom.Display();      // Tom Smith работает в Microsoft

            PersonProp p = new PersonProp();
            p.Name = "Ann";

            EmployeeProp e = new EmployeeProp();
            e.Name = "Mike";

            Console.WriteLine($"{p.Name}");
            Console.WriteLine($"{e.Name}");

            ExampleBase exb = new ExampleBase();
            Console.WriteLine($"x={exb.x} G={ExampleBase.G}");

            ExampleDerived exd = new ExampleDerived();
            Console.WriteLine($"x={exd.x} G={ExampleDerived.G}");
        }

        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public void Display()
            {
                Console.WriteLine($"{FirstName} {LastName}");
            }
        }

        class Employee : Person
        {
            public string Company { get; set; }
            public Employee(string firstName, string lastName, string company)
                    : base(firstName, lastName)
            {
                Company = company;
            }
            public new void Display()
            {
                Console.WriteLine($"{FirstName} {LastName} работает в {Company}");
            }
        }

        class PersonProp
        {
            protected string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }
        class EmployeeProp : PersonProp
        {
            public new string Name
            {
                get { return "Employee " + base.Name; }
                set { name = value; }
            }
        }

        class ExampleBase
        {
            public readonly int x = 10;
            public const int G = 5;
        }
        class ExampleDerived : ExampleBase
        {
            public new readonly int x = 20;
            public new const int G = 15;
        }
    }

    class VirtualVsHidingProperties
    {
        public static void Display()
        {
            ExampleBase exb = new ExampleBase();
            ExampleDerived exd = new ExampleDerived();

            exb.InfoNew = "New info base";
            exb.InfoVirtual = "Virtual info base";
            exb.DisplayInfo();

            exd.InfoNew = "New info derived";
            exd.InfoVirtual = "Virtual info derived";
            exd.DisplayInfo();
            exd.DisplayInfoDerived();
        }

        class ExampleBase
        {
            protected string infoNew;
            public string InfoNew
            {
                get { return infoNew; }
                set { infoNew = value; }
            }

            protected string infoVirtual;
            public virtual string InfoVirtual
            {
                get { return infoVirtual; }
                set { infoVirtual = value; }
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"From base class: \t{InfoNew}");
                Console.WriteLine($"From base class: \t{InfoVirtual}");
            }
        }

        class ExampleDerived : ExampleBase
        {
            public new string InfoNew
            {
                get { return "Information: " + base.InfoNew; }
                set { infoNew = value; }
            }

            public override string InfoVirtual
            {
                get { return "Information: " + base.infoVirtual; }
                set { infoVirtual = value; }
            }

            public void DisplayInfoDerived()
            {
                Console.WriteLine($"From derived class: \t{InfoNew}");
                Console.WriteLine($"From derived class: \t{InfoVirtual}");
            }
        }
    }
}