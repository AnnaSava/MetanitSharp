using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class InterfaceDemo
    {
        public static void Display()
        {
            Person person = new Person();
            Car car = new Car();
            Action(person);
            Action(car);

            Driver driver = new Driver();
            Action(driver);

            HeroAction action = new HeroAction();
            action.Move();
        }

        static void Action(IMovable movable)
        {
            movable.Move();
        }

        interface IMovable
        {
            void Move();
        }
        class Person : IMovable
        {
            public void Move()
            {
                Console.WriteLine("Человек идет");
            }
        }
        struct Car : IMovable
        {
            public void Move()
            {
                Console.WriteLine("Машина едет");
            }
        }

        abstract class PersonAbstract : IMovable
        {
            public abstract void Move();
        }
        class Driver : PersonAbstract
        {
            public override void Move()
            {
                Console.WriteLine("Шофер ведет машину");
            }
        }

        interface IAction
        {
            void Move();
        }
        class BaseAction
        {
            public void Move()
            {
                Console.WriteLine("Move in BaseAction");
            }
        }
        class HeroAction : BaseAction, IAction
        {
        }
    }

    class MultipleInterfaces
    {
        public static void Display()
        {
            Client client = new Client("Tom", 200);
            client.Put(30);
            Console.WriteLine(client.CurrentSum); //230
            client.Withdraw(100);
            Console.WriteLine(client.CurrentSum); //130

            // Все объекты Client являются объектами IAccount 
            IAccount account = new Client("Том", 200);
            account.Put(200);
            Console.WriteLine(account.CurrentSum); // 400

            // Не все объекты IAccount являются объектами Client, необходимо явное приведение
            Client client2 = (Client)account;
            // Интерфейс IAccount не имеет свойства Name, необходимо явное приведение
            string clientName = ((Client)account).Name;

            Console.WriteLine(client2.Name);
            Console.WriteLine(clientName);
        }

        interface IAccount
        {
            int CurrentSum { get; }  // Текущая сумма на счету
            void Put(int sum);      // Положить деньги на счет
            void Withdraw(int sum); // Взять со счета
        }
        interface IClient
        {
            string Name { get; set; }
        }
        class Client : IAccount, IClient
        {
            int _sum; // Переменная для хранения суммы
            public string Name { get; set; }
            public Client(string name, int sum)
            {
                Name = name;
                _sum = sum;
            }

            public int CurrentSum { get { return _sum; } }

            public void Put(int sum) { _sum += sum; }

            public void Withdraw(int sum)
            {
                if (_sum >= sum)
                {
                    _sum -= sum;
                }
            }
        }
    }

    class InterfaceInheritance
    {
        public static void Display()
        {
            RunAction run = new RunAction();
            run.Move();
            run.Run();

            SwimAction swim = new SwimAction();
            swim.Move();
        }

        interface IAction
        {
            void Move();
        }
        interface IRunAction : IAction
        {
            void Run();
        }
        class RunAction : IRunAction
        {
            public void Move()
            {
                Console.WriteLine("Move");
            }
            public void Run()
            {
                Console.WriteLine("Run");
            }
        }

        interface ISwimAction : IAction
        {
            new void Move();
        }

        class SwimAction : ISwimAction
        {
            public void Move()
            {
                Console.WriteLine("Swim");
            }
        }
    }

    class OverrideInInterface
    {
        public static void Display()
        {
            BaseAction action1 = new HeroAction();
            action1.Move();            // Move in HeroAction

            IAction action2 = new HeroAction();
            action2.Move();             // Move in HeroAction

            action1.Move2();            // Move2 in BaseAction
            action2.Move2();            // Move2 in BaseAction

            BaseAction action3 = new HeroActionRealized();
            action3.Move3();            // Move3 in BaseAction

            IAction action4 = new HeroActionRealized();
            action4.Move3();            // Move3 in HeroActionRealized
        }

        interface IAction
        {
            void Move();

            void Move2();

            void Move3();
        }
        class BaseAction : IAction
        {
            public virtual void Move()
            {
                Console.WriteLine("Move in BaseAction");
            }

            public void Move2()
            {
                Console.WriteLine("Move2 in BaseAction");
            }

            public void Move3()
            {
                Console.WriteLine("Move3 in BaseAction");
            }
        }
        class HeroAction : BaseAction
        {
            public override void Move()
            {
                Console.WriteLine("Move in HeroAction");
            }

            public new void Move2()
            {
                Console.WriteLine("Move2 in HeroAction");
            }
        }

        class HeroActionRealized : BaseAction, IAction
        {
            public new void Move3()
            {
                Console.WriteLine("Move3 in HeroActionRealized");
            }
        }
    }

    class ExplicitImplementation
    {
        public static void Display()
        {
            simpleExample();
            studyExample();
            actionExample();
        }

        static void simpleExample()
        {
            BaseClass action = new BaseClass();
            ((IAction)action).Move();   // необходимо приведение к типу IAction

            // или так
            IAction action2 = new BaseClass();
            action2.Move();
        }

        static void studyExample()
        {
            Person person = new Person();
            person.Study();

            Student student = new Student();
            ((ISchool)student).Study();
            ((IUniversity)student).Study();

            ISchool student2 = new Student();
            student2.Study();

            IUniversity student3 = new Student();
            student3.Study();
        }

        static void actionExample()
        {
            HeroAction action1 = new HeroAction();
            action1.Move();            // Move in BaseAction
            ((IAction)action1).Move(); // Move in HeroAction

            IAction action2 = new HeroAction();
            action2.Move();             // Move in HeroAction
        }

        interface IAction
        {
            void Move();
        }
        class BaseClass : IAction
        {
            void IAction.Move()
            {
                Console.WriteLine("Move in BaseClass");
            }
        }

        class BaseAction : IAction
        {
            public void Move()
            {
                Console.WriteLine("Move in BaseAction");
            }
        }
        class HeroAction : BaseAction, IAction
        {
            void IAction.Move()
            {
                Console.WriteLine("Move in HeroAction");
            }
        }

        class Person : ISchool, IUniversity
        {
            public void Study()
            {
                Console.WriteLine("Учеба в школе или в университете");
            }
        }

        class Student : ISchool, IUniversity
        {
            void ISchool.Study()
            {
                Console.WriteLine("Учеба в школе");
            }
            void IUniversity.Study()
            {
                Console.WriteLine("Учеба в университете");
            }
        }

        interface ISchool
        {
            void Study();
        }

        interface IUniversity
        {
            void Study();
        }
    }

    class GenericInterfaces
    {
        public static void Display()
        {
            Client account1 = new Client("Tom", 200);
            Client account2 = new Client("Bob", 300);
            Transaction<Client> transaction = new Transaction<Client>();
            transaction.Operate(account1, account2, 150);

            IClientAccount account3 = new ClientAccount("Alice", 400);
            IClientAccount account4 = new ClientAccount("Kate", 500);
            Transaction<IClientAccount> operation = new Transaction<IClientAccount>();
            operation.Operate(account3, account4, 200);

            IUser<int> user1 = new User<int>(6789);
            Console.WriteLine(user1.Id);    // 6789

            IUser<string> user2 = new User<string>("12345");
            Console.WriteLine(user2.Id);    // 12345

            IUser<int> user3 = new IntUser(3579);
            Console.WriteLine(user3.Id);
        }

        class Transaction<T> where T : IAccount, IClient
        {
            public void Operate(T acc1, T acc2, int sum)
            {
                if (acc1.CurrentSum >= sum)
                {
                    acc1.Withdraw(sum);
                    acc2.Put(sum);
                    Console.WriteLine($"{acc1.Name} : {acc1.CurrentSum}\n{acc2.Name} : {acc2.CurrentSum}");
                }
            }
        }

        interface IAccount
        {
            int CurrentSum { get; } // Текущая сумма на счету
            void Put(int sum);      // Положить деньги на счет
            void Withdraw(int sum); // Взять со счета
        }
        interface IClient
        {
            string Name { get; set; }
        }
        class Client : IAccount, IClient
        {
            int _sum; // Переменная для хранения суммы
            public Client(string name, int sum)
            {
                Name = name;
                _sum = sum;
            }

            public string Name { get; set; }
            public int CurrentSum
            {
                get { return _sum; }
            }
            public void Put(int sum)
            {
                _sum += sum;
            }
            public void Withdraw(int sum)
            {
                if (sum <= _sum)
                {
                    _sum -= sum;
                }
            }
        }

        interface IClientAccount : IAccount, IClient
        {

        }
        class ClientAccount : IClientAccount
        {
            int _sum;
            public ClientAccount(string name, int sum)
            {
                _sum = sum; Name = name;
            }
            public int CurrentSum { get { return _sum; } }

            public string Name { get; set; }

            public void Put(int sum)
            {
                _sum += sum;
            }
            public void Withdraw(int sum)
            {
                if (_sum >= sum) _sum -= sum;
            }
        }

        interface IUser<T>
        {
            T Id { get; }
        }
        class User<T> : IUser<T>
        {
            T _id;
            public User(T id)
            {
                _id = id;
            }
            public T Id { get { return _id; } }
        }

        class IntUser : IUser<int>
        {
            int _id;
            public IntUser(int id)
            {
                _id = id;
            }
            public int Id { get { return _id; } }
        }
    }

    class Clonable
    {
        public static void Display()
        {
            Person p1 = new Person { Name = "Tom", Age = 23 };
            Person p2 = (Person)p1.Clone();
            p2.Name = "Alice";
            Console.WriteLine(p1.Name); // Tom

            User u1 = new User { Name = "Tom", Age = 23 };
            User u2 = (User)u1.Clone();
            u2.Name = "Alice";
            Console.WriteLine(u1.Name); // Tom
        }

        class Person : ICloneable
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public object Clone()
            {
                return new Person { Name = this.Name, Age = this.Age };
            }

            public object Clone2()
            {
                return this.MemberwiseClone();
            }
        }

        class User : ICloneable
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
    }

    class ClonableComplex
    {
        public static void Display()
        {
            Person p1 = new Person { Name = "Tom", Age = 23, Work = new Company { Name = "Microsoft" } };
            Person p2 = (Person)p1.Clone();
            p2.Work.Name = "Google";
            p2.Name = "Alice";
            Console.WriteLine(p1.Name); // Tom
            Console.WriteLine(p1.Work.Name); // Google - а должно быть Microsoft

            PersonDeep p3 = new PersonDeep { Name = "Tom", Age = 23, Work = new Company { Name = "Microsoft" } };
            PersonDeep p4 = (PersonDeep)p3.Clone();
            p4.Work.Name = "Google";
            p4.Name = "Alice";
            Console.WriteLine(p3.Name);
            Console.WriteLine(p3.Work.Name);
        }

        class Person : ICloneable
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Company Work { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        class PersonDeep : ICloneable
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Company Work { get; set; }

            public object Clone()
            {
                Company company = new Company { Name = this.Work.Name };
                return new PersonDeep
                {
                    Name = this.Name,
                    Age = this.Age,
                    Work = company
                };
            }
        }

        class Company
        {
            public string Name { get; set; }
        }
    }

    class Comparable
    {
        public static void Display()
        {
            Person p1 = new Person { Name = "Bill", Age = 34 };
            Person p2 = new Person { Name = "Tom", Age = 23 };
            Person p3 = new Person { Name = "Alice", Age = 21 };

            Person[] people = new Person[] { p1, p2, p3 };
            Array.Sort(people);

            foreach (Person p in people)
            {
                Console.WriteLine("{0} - {1}", p.Name, p.Age);
            }

            Array.Sort(people, new PeopleComparer());

            foreach (Person p in people)
            {
                Console.WriteLine("{0} - {1}", p.Name, p.Age);
            }
        }

        class Person : IComparable
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int CompareTo(object o)
            {
                Person p = o as Person;
                if (p != null)
                    return this.Name.CompareTo(p.Name);
                else
                    throw new Exception("Невозможно сравнить два объекта");
            }
        }

        class PeopleComparer : IComparer<Person>
        {
            public int Compare(Person p1, Person p2)
            {
                if (p1.Name.Length > p2.Name.Length)
                    return 1;
                else if (p1.Name.Length < p2.Name.Length)
                    return -1;
                else
                    return 0;
            }
        }
    }

    class CovarianceContravariance
    {
        public static void Display()
        {
            IBank<DepositAccount> depositBank = new Bank<DepositAccount>();
            Account acc1 = depositBank.CreateAccount(34);

            IBank<Account> ordinaryBank = new Bank<DepositAccount>(); // без out в интерфейсе здесь ошибка
            // или так
            // IBank<Account> ordinaryBank = depositBank;
            Account acc2 = ordinaryBank.CreateAccount(45);

            ITransaction<Account> accTransaction = new Transaction<Account>();
            accTransaction.DoOperation(new Account(), 400);

            ITransaction<DepositAccount> depAccTransaction = new Transaction<Account>(); // без in в интерфейсе здесь ошибка
            accTransaction.DoOperation(new DepositAccount(), 450);

            depAccTransaction.DoOperation(new DepositAccount(), 450);            
        }

        interface IBank<out T>
        {
            T CreateAccount(int sum);
        }

        class Bank<T> : IBank<T> where T : Account, new()
        {
            public T CreateAccount(int sum)
            {
                T acc = new T();  // создаем счет
                acc.DoTransfer(sum);
                return acc;
            }
        }

        interface ITransaction<in T>
        {
            void DoOperation(T account, int sum);
        }

        class Transaction<T> : ITransaction<T> where T : Account
        {
            public void DoOperation(T account, int sum)
            {
                account.DoTransfer(sum);
            }
        }

        class Account
        {
            public virtual void DoTransfer(int sum)
            {
                Console.WriteLine($"Клиент положил на счет {sum} долларов");
            }
        }
        class DepositAccount : Account
        {
            public override void DoTransfer(int sum)
            {
                Console.WriteLine($"Клиент положил на депозитный счет {sum} долларов");
            }
        }
    }
}
