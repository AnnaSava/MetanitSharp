using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class DelegateDemo
    {
        delegate void Message(); // 1. Объявляем делегат

        public static void Display()
        {
            Message msg; // 2. Создаем переменную делегата
            if (DateTime.Now.Hour < 12)
            {
                msg = GoodMorning; // 3. Присваиваем этой переменной адрес метода
            }
            else
            {
                msg = GoodEvening;
            }
            msg(); // 4. Вызываем метод
        }

        private static void GoodMorning()
        {
            Console.WriteLine("Good Morning");
        }
        private static void GoodEvening()
        {
            Console.WriteLine("Good Evening");
        }
    }

    class MathForDelegate
    {
        public int Sum(int x, int y) { return x + y; }
    }

    class DelegateWithParams
    {
        delegate int Operation(int x, int y);

        public static void Display()
        {
            // присваивание адреса метода через контруктор
            Operation operation = Add; // делегат указывает на метод Add
            int result = operation(4, 5); // фактически Add(4, 5)
            Console.WriteLine($"operation=Add\t\t{result}");

            operation = Multiply; // теперь делегат указывает на метод Multiply
            result = operation(4, 5); // фактически Multiply(4, 5)
            Console.WriteLine($"operation=Multiply\t{result}");

            Operation operation2 = operation;
            result = operation2(4, 5);
            Console.WriteLine($"operation2=operation\t{result}");

            operation2 = Subtract;
            result = operation2(4, 5);
            Console.WriteLine($"operation2=Substract\t{result}");

            result = operation(4, 5);
            Console.WriteLine($"operation\t\t{result}");

            MathForDelegate math = new MathForDelegate();
            Operation operation3 = math.Sum;
            result = operation3(4, 5);     // math.Sum(4, 5)
            Console.WriteLine($"operation3=math.Sum\t{result}");  // 9

            Operation operation4 = new Operation(Add);
            result = operation4(4, 5);
            Console.WriteLine($"operation4=new Operation(Add)\t{result}");
        }

        private static int Add(int x, int y)
        {
            return x + y;
        }
        private static int Multiply(int x, int y)
        {
            return x * y;
        }
        private static int Subtract(int x, int y)
        {
            return x - y;
        }
    }

    class DelegateMultiple
    {
        delegate void Message();
        delegate int Operation(int x, int y);

        public static void Display()
        {
            Console.WriteLine("Добавление методов в делегат");
            Message msg = Hello;
            msg += HowAreYou;  // теперь mes1 указывает на два метода
            msg(); // вызываются оба метода - Hello и HowAreYou

            Console.WriteLine("\n+=Hello +=Hello");
            msg += Hello;
            msg += Hello;
            msg();

            Console.WriteLine("\nУдаление методов из делегата");
            Message msg1 = Hello;
            msg1 += HowAreYou;
            Message msg2 = HowAreYou;
            Message msg3 = msg1 + msg2;
            msg3(); // вызываются все методы из msg1 и msg2
            msg3 -= HowAreYou;  // удаляем метод HowAreYou
            Console.WriteLine("\n-=HowAreYou");
            msg3(); // вызывается метод Hello

            Console.WriteLine("\nВозвращается значение последнего метода из списка вызова");
            Operation op = Subtract;
            op += Multiply;
            op += Add;
            Console.WriteLine(op(7, 2));    // Add(7,2) = 9
        }

        private static void Hello()
        {
            Console.WriteLine("Hello");
        }
        private static void HowAreYou()
        {
            Console.WriteLine("How are you?");
        }

        private static int Add(int x, int y) { return x + y; }
        private static int Subtract(int x, int y) { return x - y; }
        private static int Multiply(int x, int y) { return x * y; }
    }

    class DelegateInvoke
    {
        delegate int Operation(int x, int y);
        delegate void Message();

        public static void Display()
        {
            Message mes = Hello;
            mes.Invoke();
            Operation op = Add;
            int result = op.Invoke(3, 4);
            Console.WriteLine(result);

            emptyDelegateTryCatch();
            emptyDelegateInvoke();
        }

        static void emptyDelegateTryCatch()
        {
            try
            {
                Message mes = null;
                mes();        // ! Ошибка: делегат равен null
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Operation op = Add;
                op -= Add;      // делегат op пуст
                op(3, 4);       // !Ошибка: делегат равен null
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void emptyDelegateInvoke()
        {
            Console.WriteLine("До вызова пустых делегатов");
            Message mes = null;
            mes?.Invoke();        // ошибки нет, делегат просто не вызывается

            Operation op = Add;
            op -= Add;          // делегат op пуст
            op?.Invoke(3, 4);   // ошибки нет, делегат просто не вызывается
            Console.WriteLine("После вызова пустых делегатов");
        }

        private static void Hello() { Console.WriteLine("Hello"); }
        private static int Add(int x, int y) { return x + y; }
    }

    class DelegateAsParam
    {
        delegate void GetMessage();

        public static void Display()
        {
            if (DateTime.Now.Hour < 12)
            {
                Show_Message(GoodMorning);
            }
            else
            {
                Show_Message(GoodEvening);
            }
        }

        private static void Show_Message(GetMessage _del)
        {
            _del?.Invoke();
        }
        private static void GoodMorning()
        {
            Console.WriteLine("Good Morning");
        }
        private static void GoodEvening()
        {
            Console.WriteLine("Good Evening");
        }
    }

    class GenericDelegates
    {
        delegate T Operation<T, K>(K val);

        public static void Display()
        {
            Operation<decimal, int> op = Square;
            Console.WriteLine(op(5));

            Operation<String, char> op2 = ConcatChars;
            Console.WriteLine(op2('q'));
        }

        static decimal Square(int n)
        {
            return n * n;
        }

        static String ConcatChars(char ch)
        {
            return ch.ToString() + ch.ToString();
        }
    }

    class DelegateAccountExample
    {

        public static void Display()
        {
            exampleSimple();
            exampleColor();
        }

        static void exampleSimple()
        {
            // создаем банковский счет
            Account account = new Account(200);
            // Добавляем в делегат ссылку на метод Show_Message
            // а сам делегат передается в качестве параметра метода RegisterHandler
            account.RegisterHandler(new Account.AccountStateHandler(Show_Message));
            // Два раза подряд пытаемся снять деньги
            account.Withdraw(100);
            account.Withdraw(150);
        }

        static void exampleColor()
        {
            Console.WriteLine("\nПример с цветом");

            Account account = new Account(200);
            Account.AccountStateHandler colorDelegate = new Account.AccountStateHandler(Color_Message);

            // Добавляем в делегат ссылку на методы
            account.RegisterHandler(new Account.AccountStateHandler(Show_Message));
            account.RegisterHandler(colorDelegate);
            // Два раза подряд пытаемся снять деньги
            account.Withdraw(100);
            account.Withdraw(150);

            // Удаляем делегат
            account.UnregisterHandler(colorDelegate);
            account.Withdraw(50);
        }

        private static void Show_Message(String message)
        {
            Console.WriteLine(message);
        }

        private static void Color_Message(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }

        class Account
        {
            // Объявляем делегат
            public delegate void AccountStateHandler(string message);
            // Создаем переменную делегата
            AccountStateHandler _del;

            // Регистрируем делегат
            public void RegisterHandler(AccountStateHandler del)
            {
                _del += del; // добавляем делегат
            }

            // Отмена регистрации делегата
            public void UnregisterHandler(AccountStateHandler del)
            {
                _del -= del; // удаляем делегат
            }

            int _sum; // Переменная для хранения суммы

            public Account(int sum)
            {
                _sum = sum;
            }

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

                    if (_del != null)
                        _del($"Сумма {sum} снята со счета");
                }
                else
                {
                    if (_del != null)
                        _del("Недостаточно денег на счете");
                }
            }
        }
    }

    class EventAccountExample
    {
        public static void Display()
        {
            Account account = new Account(200);
            // Добавляем обработчики события
            account.Added += Show_Message;
            account.Withdrawn += Show_Message;

            account.Withdraw(100);
            // Удаляем обработчик события
            account.Withdrawn -= Show_Message;

            account.Withdraw(50);
            account.Put(150);

            account.Added += new Account.AccountStateHandler(Show_Message);

            account.Put(300);
            account.Added -= Show_Message;
        }

        private static void Show_Message(string message)
        {
            Console.WriteLine(message);
        }

        class Account
        {
            // Объявляем делегат
            public delegate void AccountStateHandler(string message);
            // Событие, возникающее при выводе денег
            public event AccountStateHandler Withdrawn;
            // Событие, возникающее при добавление на счет
            public event AccountStateHandler Added;

            int _sum; // Переменная для хранения суммы

            public Account(int sum)
            {
                _sum = sum;
            }

            public int CurrentSum
            {
                get { return _sum; }
            }

            public void Put(int sum)
            {
                _sum += sum;
                if (Added != null)
                    Added($"На счет поступило {sum}");
            }
            public void Withdraw(int sum)
            {
                if (sum <= _sum)
                {
                    _sum -= sum;
                    if (Withdrawn != null)
                        Withdrawn($"Сумма {sum} снята со счета");
                }
                else
                {
                    if (Withdrawn != null)
                        Withdrawn("Недостаточно денег на счете");
                }
            }
        }
    }

    class EventAccountArgsExample
    {
        public static void Display()
        {
            Account account = new Account(200);
            // Добавляем обработчики события
            account.Added += Show_Message;
            account.Withdrawn += Show_Message;

            account.Withdraw(100);
            // Удаляем обработчик события
            account.Withdrawn -= Show_Message;

            account.Withdraw(50);
            account.Put(150);
        }

        private static void Show_Message(object sender, AccountEventArgs e)
        {
            Console.WriteLine($"Сумма транзакции: {e.Sum}");
            Console.WriteLine(e.Message);
        }

        class AccountEventArgs
        {
            // Сообщение
            public string Message { get; }
            // Сумма, на которую изменился счет
            public int Sum { get; }

            public AccountEventArgs(string mes, int sum)
            {
                Message = mes;
                Sum = sum;
            }
        }

        class Account
        {
            // Объявляем делегат
            public delegate void AccountStateHandler(object sender, AccountEventArgs e);
            // Событие, возникающее при выводе денег
            public event AccountStateHandler Withdrawn;
            // Событие, возникающее при добавлении на счет
            public event AccountStateHandler Added;

            int _sum; // Переменная для хранения суммы

            public Account(int sum)
            {
                _sum = sum;
            }

            public int CurrentSum
            {
                get { return _sum; }
            }

            public void Put(int sum)
            {
                _sum += sum;
                if (Added != null)
                    Added(this, new AccountEventArgs($"На счет поступило {sum}", sum));
            }
            public void Withdraw(int sum)
            {
                if (_sum >= sum)
                {
                    _sum -= sum;
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs($"Сумма {sum} снята со счета", sum));
                }
                else
                {
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs("Недостаточно денег на счете", sum));
                }
            }
        }
    }

    class AnonymousMethodsDemo
    {
        delegate int Operation(int x, int y);

        delegate void MessageHandler(string message);

        public static void Display()
        {
            MessageHandler handler = delegate (string msg)
            {
                Console.WriteLine(msg);
            };
            handler("hello world!");

            ShowMessage("hello!", delegate (string msg)
            {
                Console.WriteLine(msg);
            });

            MessageHandler handler2 = delegate
            {
                Console.WriteLine("анонимный метод");
            };
            handler2("hello world!");    // анонимный метод

            Operation operation = delegate (int x, int y)
            {
                return x + y;
            };
            int d = operation(4, 5);
            Console.WriteLine(d);       // 9

            int z = 8;
            Operation operation2 = delegate (int x, int y)
            {
                return x + y + z;
            };
            int f = operation2(4, 5);
            Console.WriteLine(f);       // 17
        }

        static void ShowMessage(string message, MessageHandler handler)
        {
            handler(message);
        }
    }

    class AnonymousMethodsHandler
    {
        public static void Display()
        {
            Account account = new Account(200);
            // Добавляем обработчики события
            account.Added += delegate (object sender, AccountEventArgs e)
            {
                Console.WriteLine($"Сумма транзакции: {e.Sum}");
                Console.WriteLine(e.Message);
            };
            account.Put(230);
        }

        delegate void AccountStateHandler(object sender, AccountEventArgs e);
        class AccountEventArgs
        {
            public string Message { get; }
            public int Sum { get; }
            public AccountEventArgs(string message, int sum)
            {
                Message = message; Sum = sum;
            }
        }
        class Account
        {
            int _sum;
            public event AccountStateHandler Added;
            public event AccountStateHandler Withdrawn;
            public Account(int sum)
            {
                _sum = sum;
            }
            public void Put(int sum)
            {
                _sum += sum;
                if (Added != null) Added(this,
                    new AccountEventArgs($"На счет пришло {sum}", sum));
            }
            public void Withdraw(int sum)
            {
                if (_sum >= sum)
                {
                    _sum -= sum;
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs($"Со счета снято {sum}", sum));
                }
                else
                {
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs("На счете недостаточно средств", 0));
                }
            }
        }
    }

    class LambdaDemo
    {
        delegate int Operation(int x, int y);

        delegate int Square(int x); // объявляем делегат, принимающий int и возвращающий int

        delegate void Hello(); // делегат без параметров

        delegate void ChangeHandler(ref int x);
        public static void Display()
        {
            Operation operation = (x, y) => x + y;
            Console.WriteLine(operation(10, 20));       // 30
            Console.WriteLine(operation(40, 20));       // 60

            Square square = i => i * i; // объекту делегата присваивается лямбда-выражение

            int z = square(6); // используем делегат
            Console.WriteLine(z); // выводит число 36

            Hello hello1 = () => Console.WriteLine("Hello");
            Hello hello2 = () => Console.WriteLine("Welcome");
            hello1();       // Hello
            hello2();       // Welcome


            int t = 9;
            ChangeHandler ch = (ref int n) => n = n * 2;
            ch(ref t);
            Console.WriteLine(t);   // 18

            Hello message = () => Show_Message();
            message();
        }

        private static void Show_Message()
        {
            Console.WriteLine("Привет мир!");
        }
    }

    class LambdaHandler
    {
        delegate void AccountStateHandler(object sender, AccountEventArgs e);
        public static void Display()
        {
            Account account = new Account(100);
            account.Added += (sender, e) =>
            {
                Console.WriteLine($"Сумма транзакции: {e.Sum}");
                Console.WriteLine(e.Message);
            };
            account.Put(200);
            account.Put(109);
        }

        class AccountEventArgs
        {
            public string Message { get; }
            public int Sum { get; }
            public AccountEventArgs(string message, int sum)
            {
                Message = message; Sum = sum;
            }
        }
        class Account
        {
            int _sum;
            public event AccountStateHandler Added;
            public event AccountStateHandler Withdrawn;
            public Account(int sum)
            {
                _sum = sum;
            }
            public void Put(int sum)
            {
                _sum += sum;
                if (Added != null) Added(this,
                    new AccountEventArgs($"На счет пришло {sum}", sum));
            }
            public void Withdraw(int sum)
            {
                if (_sum >= sum)
                {
                    _sum -= sum;
                    if (Withdrawn != null)
                        Withdrawn(this, new AccountEventArgs($"Со счета снято {sum}", sum));
                }
                else
                {
                    if (Withdrawn != null)
                        Withdrawn(this,
                            new AccountEventArgs("На счете недостаточно средств", 0));
                }
            }
        }
    }

    class LambdaAsArgument
    {
        delegate bool IsEqual(int x);

        public static void Display()
        {
            int[] integers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // найдем сумму чисел больше 5
            int result1 = Sum(integers, x => x > 5);
            Console.WriteLine(result1); // 30

            // найдем сумму четных чисел
            int result2 = Sum(integers, x => x % 2 == 0);
            Console.WriteLine(result2);  //20

            // сумма чисел, чьи квадраты больше 20
            int result3 = Sum(integers, x => (x * x) > 20);
            Console.WriteLine(result3); //35
        }

        private static int Sum(int[] numbers, IsEqual func)
        {
            int result = 0;
            foreach (int i in numbers)
            {
                if (func(i))
                    result += i;
            }
            return result;
        }
    }

    class DelegateCoContraVariance
    {
        delegate Person PersonFactory(string name);
        delegate void ClientInfo(Client client);

        public static void Display()
        {
            PersonFactory personDel;
            personDel = BuildClient; // ковариантность
            Person p = personDel("Tom");
            Console.WriteLine(p.Name);

            ClientInfo clientInfo = GetPersonInfo; // контравариантность
            Client client = new Client("Alice");
            clientInfo(client);

            clientInfo((Client)p);

            Client client2 = (Client)personDel("Ann");
            clientInfo(client2);
        }

        private static Client BuildClient(string name)
        {
            return new Client(name);
        }

        private static void GetPersonInfo(Person p)
        {
            Console.WriteLine(p.Name);
        }

        class Person
        {
            public string Name { get; set; }

            public Person(String name)
            {
                Name = name;
            }
        }
        class Client : Person
        {
            public Client(String name) : base(name) { }
        }
    }

    class DelegateCoContraVarianceGeneric
    {
        delegate T Builder<out T>(string name);

        delegate void GetInfo<in T>(T item);

        public static void Display()
        {
            Builder<Client> clientBuilder = GetClient;
            Builder<Person> personBuilder1 = clientBuilder;     // ковариантность
            Builder<Person> personBuilder2 = GetClient;         // ковариантность

            Person p = personBuilder1("Tom"); // вызов делегата
            p.Display(); // Client: Tom

            GetInfo<Person> personInfo = PersonInfo;
            GetInfo<Client> clientInfo = personInfo;      // контравариантность

            Client client = new Client { Name = "Tom" };
            clientInfo(client); // Client: Tom
        }

        private static Person GetPerson(string name)
        {
            return new Person(name);
        }
        private static Client GetClient(string name)
        {
            return new Client(name);
        }

        private static void PersonInfo(Person p) => p.Display();
        private static void ClientInfo(Client cl) => cl.Display();

        class Person
        {
            public string Name { get; set; }
            public virtual void Display() => Console.WriteLine($"Person {Name}");

            public Person() { }

            public Person(String name)
            {
                Name = name;
            }
        }
        class Client : Person
        {
            public override void Display() => Console.WriteLine($"Client {Name}");

            public Client() { }

            public Client(String name) : base(name) { }
        }
    }

    class DonNetDelegates
    {
        public static void Display()
        {
            action();
            predicate();
            func();
        }        

        static void action()
        {
            Action<int, int> op;
            op = Add;
            Operation(10, 6, op);

            Operation(10, 6, Substract);

            Operation(10, 6, (x, y) => Console.WriteLine($"Произведение чисел: {x * y}"));            
        }

        static void predicate()
        {
            Predicate<int> isPositive = delegate (int x) { return x > 0; };

            Console.WriteLine(isPositive(20));
            Console.WriteLine(isPositive(-20));

            Predicate<int> isNegative = x => x < 0;

            Console.WriteLine(isNegative(20));
            Console.WriteLine(isPositive(-20));            
        }

        static void func()
        {
            Func<int, int> retFunc = Factorial;
            int n1 = GetInt(6, retFunc);
            Console.WriteLine(n1);  // 720

            int n2 = GetInt(6, x => x * x);
            Console.WriteLine(n2); // 36

            int n3 = GetInt(6, x => x + x);
            Console.WriteLine(n3);
        }

        static void Operation(int x1, int x2, Action<int, int> op)
        {
            if (x1 > x2)
                op(x1, x2);
        }

        static void Add(int x1, int x2)
        {
            Console.WriteLine("Сумма чисел: " + (x1 + x2));
        }

        static void Substract(int x1, int x2)
        {
            Console.WriteLine("Разность чисел: " + (x1 - x2));
        }

        static int GetInt(int x1, Func<int, int> retF)
        {
            int result = 0;
            if (x1 > 0)
                result = retF(x1);
            return result;
        }
        static int Factorial(int x)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
