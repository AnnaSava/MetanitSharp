using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharpFactorial
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
                throw new ArgumentNullException("args");
            int number = 0;
            if (!Int32.TryParse(args[0], out number))
                throw new ArgumentException("не возможно преобразовать строку в число");
            if (number < 1)
                throw new ArgumentException("число не должно быть меньше 1");
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            Console.WriteLine("Факториал числа {0} равен {1}", number, result);
        }
    }
}
