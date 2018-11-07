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

    class BindingDemo
    {
        public static void Display()
        {
            Person tom = new Employee("Tom", "Smith", "Microsoft");
            tom.Display();      // Tom Smith

            tom.DisplayVirtual();      // Tom Smith работает в Microsoft
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

            public virtual void DisplayVirtual()
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

            public override void DisplayVirtual()
            {
                Console.WriteLine($"{FirstName} {LastName} работает в {Company}");
            }
        }
    }

    class AbstractDemo
    {
        public static void Display()
        {
            Client client = new Client("Tom", 500);
            Employee employee = new Employee("Bob", "Apple");
            client.Display();
            employee.Display();


            Person client2 = new Client("Ben", 600);
            Person employee2 = new Employee("Jack", "Операционист");

            client2.Display();
            employee2.Display();
        }

        abstract class Person
        {
            public string Name { get; set; }

            public Person(string name)
            {
                Name = name;
            }

            public abstract void Display();
        }

        class Client : Person
        {
            public int Sum { get; set; }    // сумма на счету

            public Client(string name, int sum)
                : base(name)
            {
                Sum = sum;
            }
            public override void Display()
            {
                Console.WriteLine($"{Name} имеет счет на сумму {Sum}");
            }
        }

        class Employee : Person
        {
            public string Position { get; set; } // должность

            public Employee(string name, string position)
                : base(name)
            {
                Position = position;
            }

            public override void Display()
            {
                Console.WriteLine($"{Position} {Name}");
            }
        }
    }

    class AbstractFigure
    {
        public static void Display()
        {
            var rect = new Rectangle(2, 4);
            Console.WriteLine($"Периметр {rect.Perimeter()}");
            Console.WriteLine($"Площадь {rect.Area()}");
        }

        // абстрактный класс фигуры
        abstract class Figure
        {
            // абстрактный метод для получения периметра
            public abstract float Perimeter();
            // абстрактный метод для получения площади
            public abstract float Area();
        }
        // производный класс прямоугольника
        class Rectangle : Figure
        {
            public float Width { get; set; }
            public float Height { get; set; }

            public Rectangle(float width, float height)
            {
                this.Width = width;
                this.Height = height;
            }
            // переопределение получения периметра
            public override float Perimeter()
            {
                return Width * 2 + Height * 2;
            }
            // переопрелеление получения площади
            public override float Area()
            {
                return Width * Height;
            }
        }
    }

    class ObjectDemo
    {
        public static void Display()
        {
            toString();
            getHashCode();
            getType();
            equals();
        }

        static void toString()
        {
            Person person = new Person { Name = "Tom" };
            Console.WriteLine(person.ToString()); // выведет название класса Person

            Clock clock = new Clock { Hours = 15, Minutes = 34, Seconds = 53 };
            Console.WriteLine(clock.ToString()); // выведет 15:34:53  

            Clock clock2 = new Clock { Hours = 14, Minutes = 24, Seconds = 51 };
            Console.WriteLine(clock2); // выведет 14:24:51
        }

        static void getHashCode()
        {
            Person person = new Person { Name = "Tom" };
            Person person2 = new Person { Name = "Tom" };

            Console.WriteLine($"Hash p1={person.GetHashCode()} p2={person2.GetHashCode()}");

            person.Name = "Bob";
            Console.WriteLine($"Hash p1={person.GetHashCode()} p2={person2.GetHashCode()}");

            Person person3 = person;

            Console.WriteLine($"Hash p1={person.GetHashCode()} p3={person3.GetHashCode()}");

            User user = new User { Name = "Tom" };
            User user2 = new User { Name = "Tom" };

            Console.WriteLine($"Hash u1={user.GetHashCode()} u2={user2.GetHashCode()}");

            User user3 = user;

            Console.WriteLine($"Hash u1={user.GetHashCode()} u3={user3.GetHashCode()}");
        }

        static void getType()
        {
            Person person = new Person { Name = "Tom" };
            Console.WriteLine(person.GetType());    // Person

            object person2 = new Person { Name = "Tom" };
            if (person2.GetType() == typeof(Person))
                Console.WriteLine("Это реально класс Person");
        }

        static void equals()
        {
            Person person1 = new Person { Name = "Tom" };
            Person person2 = new Person { Name = "Bob" };
            Person person3 = new Person { Name = "Tom" };
            bool p1Ep2 = person1.Equals(person2);   // false
            bool p1Ep3 = person1.Equals(person3);   // true
            Console.WriteLine($"p1 equals p2 {p1Ep2}");
            Console.WriteLine($"p1 equals p3 {p1Ep3}");
        }

        class Clock
        {
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public int Seconds { get; set; }
            public override string ToString()
            {
                return $"{Hours}:{Minutes}:{Seconds}";
            }
        }
        class Person
        {
            public string Name { get; set; }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType()) return false;

                Person person = (Person)obj;
                return (this.Name == person.Name);
            }
        }
        class User
        {
            public string Name { get; set; }
        }
    }

    class GenericDemo
    {
        public static void Display()
        {
            objectId();
            genericId();
            defaultId();
            staticField();
            multipleParams();
            genericMethod();
        }

        static void objectId()
        {
            Account account1 = new Account { Sum = 5000 };
            Account account2 = new Account { Sum = 4000 };
            account1.Id = 2;
            account2.Id = "4356";
            int id1 = (int)account1.Id;         // упаковка в значения int в тип Object
            string id2 = (string)account2.Id;   // Распаковка в тип int
            Console.WriteLine(id1);
            Console.WriteLine(id2);
        }

        static void genericId()
        {
            AccountGeneric<int> account1 = new AccountGeneric<int> { Sum = 5000 };
            AccountGeneric<string> account2 = new AccountGeneric<string> { Sum = 4000 };
            account1.Id = 2;        // упаковка не нужна
            account2.Id = "4356";
            int id1 = account1.Id;  // распаковка не нужна
            string id2 = account2.Id;
            Console.WriteLine(id1);
            Console.WriteLine(id2);
        }

        static void defaultId()
        {
            AccountDefault<int> acc1 = new AccountDefault<int>();
            AccountDefault<string> acc2 = new AccountDefault<string>();
            Console.WriteLine(acc1.id);
            Console.WriteLine(acc2.id);
        }

        static void staticField()
        {
            AccountGeneric<int> account1 = new AccountGeneric<int> { Sum = 5000 };
            AccountGeneric<int>.session = 5436;

            AccountGeneric<string> account2 = new AccountGeneric<string> { Sum = 4000 };
            AccountGeneric<string>.session = "45245";

            Console.WriteLine(AccountGeneric<int>.session);      // 5436
            Console.WriteLine(AccountGeneric<string>.session);   // 45245
        }

        static void multipleParams()
        {
            AccountGeneric<int> acc1 = new AccountGeneric<int> { Id = 1857, Sum = 4500 };
            AccountGeneric<int> acc2 = new AccountGeneric<int> { Id = 3453, Sum = 5000 };

            Transaction<AccountGeneric<int>, string> transaction1 = new Transaction<AccountGeneric<int>, string>
            {
                FromAccount = acc1,
                ToAccount = acc2,
                Code = "45478758",
                Sum = 900
            };

            Console.WriteLine($"Транзакция {transaction1.Code} от {transaction1.FromAccount.Id} к {transaction1.ToAccount.Id} на сумму {transaction1.Sum}");
        }

        static void genericMethod()
        {
            int x = 7;
            int y = 25;
            Swap<int>(ref x, ref y);
            Console.WriteLine($"x={x}    y={y}");   // x=25   y=7

            string s1 = "hello";
            string s2 = "bye";
            Swap<string>(ref s1, ref s2);
            Console.WriteLine($"s1={s1}    s2={s2}"); // s1=bye   s2=hello
        }

        class Account
        {
            public object Id { get; set; }
            public int Sum { get; set; }
        }

        class AccountGeneric<T>
        {
            public static T session;

            public T Id { get; set; }
            public int Sum { get; set; }
        }

        class AccountDefault<T>
        {
            // default присваивает ссылочным типам в качестве значения null, а типам значений - значение 0
            public T id = default(T); 
        }

        class Transaction<U, V>
        {
            public U FromAccount { get; set; }  // с какого счета перевод
            public U ToAccount { get; set; }    // на какой счет перевод
            public V Code { get; set; }         // код операции
            public int Sum { get; set; }        // сумма перевода
        }

        static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
    }

    class GenericRestriction
    {
        public static void Display()
        {
            Account acc1 = new Account(1857) { Sum = 4500 };
            Account acc2 = new Account(3453) { Sum = 5000 };
            Transaction<Account> transaction1 = new Transaction<Account>
            {
                FromAccount = acc1,
                ToAccount = acc2,
                Sum = 6900
            };
            transaction1.Execute();
        }

        class Account
        {
            public int Id { get; private set; } // номер счета
            public int Sum { get; set; }
            public Account(int _id)
            {
                Id = _id;
            }
        }

        class Transaction<T> where T : Account
        {
            public T FromAccount { get; set; }  // с какого счета перевод
            public T ToAccount { get; set; }    // на какой счет перевод
            public int Sum { get; set; }        // сумма перевода

            public void Execute()
            {
                if (FromAccount.Sum > Sum)
                {
                    FromAccount.Sum -= Sum;
                    ToAccount.Sum += Sum;
                    Console.WriteLine($"Счет {FromAccount.Id}: {FromAccount.Sum}$ \nСчет {ToAccount.Id}: {ToAccount.Sum}$");
                }
                else
                {
                    Console.WriteLine($"Недостаточно денег на счете {FromAccount.Id}");
                }
            }
        }
    }

    class GenericGenericRestriction
    {
        public static void Display()
        {
            Account<int> acc1 = new Account<int>(1857) { Sum = 10500 };
            Account<int> acc2 = new Account<int>(3453) { Sum = 5000 };

            Transaction<Account<int>> transaction1 = new Transaction<Account<int>>
            {
                FromAccount = acc1,
                ToAccount = acc2,
                Sum = 6900
            };
            transaction1.Execute();

            Transact<Account<int>>(acc1, acc2, 900);
        }

        class Account<T>
        {
            public T Id { get; private set; } // номер счета
            public int Sum { get; set; }
            public Account(T _id)
            {
                Id = _id;
            }
        }
        class Transaction<T> where T : Account<int>
        {
            public T FromAccount { get; set; }  // с какого счета перевод
            public T ToAccount { get; set; }    // на какой счет перевод
            public int Sum { get; set; }        // сумма перевода

            public void Execute()
            {
                if (FromAccount.Sum > Sum)
                {
                    FromAccount.Sum -= Sum;
                    ToAccount.Sum += Sum;
                    Console.WriteLine($"Счет {FromAccount.Id}: {FromAccount.Sum}$ \nСчет {ToAccount.Id}: {ToAccount.Sum}$");
                }
                else
                {
                    Console.WriteLine($"Недостаточно денег на счете {FromAccount.Id}");
                }
            }
        }

        static void Transact<T>(T acc1, T acc2, int sum) where T : Account<int>
        {
            if (acc1.Sum > sum)
            {
                acc1.Sum -= sum;
                acc2.Sum += sum;
            }
            Console.WriteLine($"acc1: {acc1.Sum}   acc2: {acc2.Sum}");
        }
    }

    class GenericStandardRestrictions
    {
        public static void Display()
        {
            WithStruct<int> gen1 = new WithStruct<int>();
            WithClass<Person> gen2 = new WithClass<Person>();
            WithPublicConstructor<Person> gen3 = new WithPublicConstructor<Person>();

            Console.WriteLine(gen1.GetType());
            Console.WriteLine(gen2.GetType());
            Console.WriteLine(gen3.GetType());            

            Transaction<Account> transaction = new Transaction<Account>();

            Console.WriteLine(transaction.GetType());
        }

        class WithStruct<T> where T : struct
        { }

        class WithClass<T> where T : class
        { }

        class WithPublicConstructor<T> where T : new()
        { }

        interface IAccount
        {
            int CurrentSum { get; set; }
        }
        class Person
        {
            public string Name { get; set; }
        }

        class Account : Person, IAccount
        {
            public int CurrentSum { get; set; }
        }

        class Transaction<T> where T : Person, IAccount, new()
        {

        }
    }

    class GenericMultipleParams
    {
        public static void Display()
        {
            Transaction<Account<int>, int> transaction = new Transaction<Account<int>, int>();
            Console.WriteLine(transaction.GetType());
        }

        class Account<T>
        {
            public T Id { get; private set; } // номер счета
            public int Sum { get; set; }
            public Account(T _id)
            {
                Id = _id;
            }
        }

        class Transaction<U, V>
        where U : Account<int>
        where V : struct
        {

        }
    }

    class GenericInheritance
    {
        public static void Display()
        {
            Account<string> acc1 = new Account<string>("34");
            Account<int> acc2 = new UniversalAccount<int>(45);
            UniversalAccount<int> acc3 = new UniversalAccount<int>(33);
            Console.WriteLine(acc1.Id);
            Console.WriteLine(acc2.Id);
            Console.WriteLine(acc3.Id);

            StringAccount acc4 = new StringAccount("438767");
            Account<string> acc5 = new StringAccount("43875");
            Console.WriteLine(acc4.Id);
            Console.WriteLine(acc5.Id);

            IntAccount<string> acc7 = new IntAccount<string>(5) { Code = "r4556" };
            Account<int> acc8 = new IntAccount<long>(7) { Code = 4587 };
            Console.WriteLine(acc7.Id);
            Console.WriteLine(acc8.Id);

            MixedAccount<string, int> acc9 = new MixedAccount<string, int>("456") { Code = 356 };
            Account<string> acc10 = new MixedAccount<string, int>("9867") { Code = 35678 };
            Console.WriteLine(acc9.Id);
            Console.WriteLine(acc10.Id);

            AccountWithClass<string> acc11 = new AccountWithClass<string>("34");
            AccountWithClass<string> acc12 = new UniversalAccountWithClass<string>("45");
            Console.WriteLine(acc11.Id);
            Console.WriteLine(acc12.Id);
        }

        class Account<T>
        {
            public T Id { get; private set; }
            public Account(T _id)
            {
                Id = _id;
            }
        }

        class UniversalAccount<T> : Account<T>
        {
            public UniversalAccount(T id) : base(id)
            {

            }
        }

        class StringAccount : Account<string>
        {
            public StringAccount(string id) : base(id)
            {
            }
        }

        class IntAccount<T> : Account<int>
        {
            public T Code { get; set; }
            public IntAccount(int id) : base(id)
            {
            }
        }

        class MixedAccount<T, K> : Account<T>
            where K : struct
        {
            public K Code { get; set; }
            public MixedAccount(T id) : base(id)
            {

            }
        }

        class AccountWithClass<T> where T : class
        {
            public T Id { get; private set; }
            public AccountWithClass(T _id)
            {
                Id = _id;
            }
        }
        class UniversalAccountWithClass<T> : AccountWithClass<T>
            where T : class
        {
            public UniversalAccountWithClass(T id) : base(id)
            {

            }
        }
    }
}