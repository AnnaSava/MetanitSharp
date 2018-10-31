using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharpLib
{
    public class Modifiers
    {
        // поле доступно только из текущего класса
        private int Private;

        // доступно из текущего класса и производных классов
        protected int Protected;

        // доступно в любом месте программы
        internal int Internal;

        // доступно в любом месте программы и из классов-наследников
        protected internal int ProtectedInternal;

        // доступно в любом месте программы, а также для других программ и сборок
        public int Public;

        // доступно из текущего класса и производных классов, которые определены в том же проекте
        protected private int ProtectedPrivate;

        public Modifiers()
        {
            Private = 1;
            Protected = 2;
            Internal = 3;
            ProtectedInternal = 4;
            Public = 5;
            ProtectedPrivate = 6;
        }

        public void PrintModifiersFields()
        {
            Console.WriteLine("Вывод внутри класса");
            Console.WriteLine($".Private = {Private}");
            Console.WriteLine($".Protected = {Protected}");
            Console.WriteLine($".Internal = {Internal}");
            Console.WriteLine($".ProtectedInternal = {ProtectedInternal}");
            Console.WriteLine($".Public = {Public}");
            Console.WriteLine($".ProtectedPrivate = {ProtectedPrivate}");
            Console.WriteLine();
        }
    }

    public class ModifiersInternalInheritor : Modifiers
    {
        public void PrintInternalInheritorModifiers()
        {
            Console.WriteLine("Вывод внутри класса-наследника в той же сборке");
            Console.WriteLine($".Protected = {Protected}");
            Console.WriteLine($".Internal = {Internal}");
            Console.WriteLine($".ProtectedInternal = {ProtectedInternal}");
            Console.WriteLine($".Public = {Public}");
            Console.WriteLine($".ProtectedPrivate = {ProtectedPrivate}");
            Console.WriteLine();
        }
    }

    public class ModifiersInternalExample
    {
        public void PrintModifiers()
        {
            var modifiers = new Modifiers();

            Console.WriteLine("Вывод в другом классе той же сборки");
            Console.WriteLine($".Internal = {modifiers.Internal}");
            Console.WriteLine($".ProtectedInternal = {modifiers.ProtectedInternal}");
            Console.WriteLine($".Public = {modifiers.Public}");
            Console.WriteLine();
        }
    }
}
