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
}
