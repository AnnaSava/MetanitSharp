using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    static class ExceptionsHandling
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
                    case 't':
                        usingTryCatch();
                        usingTryCatchType();
                        usingTryCatchVariable();
                        usingTryCatchWhen();
                        usingTryParse();
                        break;
                    case 'e':
                        usingExceptionClass();
                        catchIndexOutOfRange();
                        catchInvalidCast();
                        catchNullReference();
                        break;
                    case 'c':
                        OwnExceptionDemo.Display();
                        OwnExceptionClass.Display();
                        OwnExceptionClassVal.Display();
                        break;
                    case 's':
                        SearchCatch.Display();
                        break;
                    case 'h':
                        usingThrow();
                        usingCatchThrow();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("T - конструкция try-catch-finally");
            Console.WriteLine("E - класс Exception");
            Console.WriteLine("C - создание классов исключений");
            Console.WriteLine("S - поиск блока catch");
            Console.WriteLine("H - использование throw");
            Console.WriteLine("X - выход из раздела");
        }

        #region Try Catch Demo

        static void usingTryCatch()
        {
            try
            {
                int x = 5;
                int y = x / 0;
                Console.WriteLine($"Результат: {y}");
            }
            catch
            {
                Console.WriteLine("Возникло исключение!");
            }
            finally
            {
                Console.WriteLine("Блок finally");
            }
            Console.WriteLine("Конец примера");            
        }

        static void usingTryCatchType()
        {
            try
            {
                int x = 5;
                int y = x / 0;
                Console.WriteLine($"Результат: {y}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Возникло исключение DivideByZeroException");
            }
        }

        static void usingTryCatchVariable()
        {
            try
            {
                int x = 5;
                int y = x / 0;
                Console.WriteLine($"Результат: {y}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
            }

        }

        static void usingTryCatchWhen()
        {
            int x = 1;
            int y = 0;

            try
            {
                int result = x / y;
            }
            catch (DivideByZeroException) when (y == 0 && x == 0)
            {
                Console.WriteLine("y не должен быть равен 0");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void usingTryParse()
        {
            Console.WriteLine("Введите число");
            int x;
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out x))
            {
                x *= x;
                Console.WriteLine("Квадрат числа: " + x);
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
        }

        #endregion

        #region Exception

        static void usingExceptionClass()
        {
            try
            {
                int x = 5;
                int y = x / 0;
                Console.WriteLine($"Результат: {y}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
        }

        static void catchIndexOutOfRange()
        {
            try
            {
                int[] numbers = new int[4];
                numbers[7] = 9;     // IndexOutOfRangeException

                int x = 5;
                int y = x / 0;  // DivideByZeroException - не возникнет, т.к. выполнение прекратится еще на предыдущем этапе
                Console.WriteLine($"Результат: {y}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Возникло исключение DivideByZeroException");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void catchInvalidCast()
        {
            try
            {
                object obj = "you";
                int num = (int)obj;     // InvalidCastException
                Console.WriteLine($"Результат: {num}");
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Возникло исключение InvalidCastException");
            }
        }

        static void catchNullReference()
        {
            try
            {
                object obj = null;
                obj.GetHashCode();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Возникло исключение NullReferenceException");
            }
        }

        #endregion

        static void usingThrow()
        {
            try
            {
                Console.Write("Введите строку: ");
                string message = Console.ReadLine();
                if (message.Length > 6)
                {
                    throw new Exception("Длина строки больше 6 символов");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
        static void usingCatchThrow()
        {
            try
            {
                try
                {
                    Console.Write("Введите строку: ");
                    string message = Console.ReadLine();
                    if (message.Length > 6)
                    {
                        throw new Exception("Длина строки больше 6 символов");
                    }
                }
                catch
                {
                    Console.WriteLine("Возникло исключение");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
