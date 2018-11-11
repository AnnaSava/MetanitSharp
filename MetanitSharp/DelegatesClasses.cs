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
}
