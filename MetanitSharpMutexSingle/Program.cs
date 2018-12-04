using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetanitSharpMutexSingle
{
    class Program
    {
        static void Main(string[] args)
        {
            bool existed;
            // получаем GIUD приложения
            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

            Mutex mutexObj = new Mutex(true, guid, out existed);

            if (existed)
            {
                Console.WriteLine("Приложение работает");
            }
            else
            {
                Console.WriteLine("Приложение уже было запущено. И сейчас оно будет закрыто.");
                Thread.Sleep(3000);
                return;
            }
            Console.ReadLine();
        }
    }
}
