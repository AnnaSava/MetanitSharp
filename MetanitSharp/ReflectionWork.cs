using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ReflectionWork
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
                        getType();
                        break;
                    case 'm':
                        getMembers();
                        break;
                    case 'd':
                        getMethods();
                        getMethodsArr();
                        break;
                    case 'c':
                        getConstructors();
                        break;
                    case 'f':
                        getFieldsAndProps();
                        break;
                    case 'i':
                        getInterfaces();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("T - получение типа");
            Console.WriteLine("М - информация о типе");
            Console.WriteLine("D - информация о методах");
            Console.WriteLine("C - информация о конструкторах");
            Console.WriteLine("F - информация о полях и свойствах");
            Console.WriteLine("I - информация об интерфейсах");
            Console.WriteLine("X - выход из раздела");
        }

        static void getType()
        {
            Type myType = typeof(User);
            Console.WriteLine(myType.ToString());

            User user = new User("Tom", 30);
            myType = user.GetType();
            Console.WriteLine(myType.ToString());

            myType = Type.GetType("MetanitSharp.ReflectionWork", false, true);
            Console.WriteLine(myType.ToString());

            myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);
            Console.WriteLine(myType.ToString());

            myType = Type.GetType("MetanitSharpLib.OuterEntity, MetanitSharpLib", false, true);
            Console.WriteLine(myType.ToString());
        }

        static void getMembers()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            foreach (MemberInfo mi in myType.GetMembers())
            {
                Console.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);
            }
        }

        static void getMethods()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            Console.WriteLine("Методы:");
            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual ";
                Console.Write(modificator + method.ReturnType.Name + " " + method.Name + " (");
                //получаем все параметры
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }
        }

        static void getMethodsArr()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            MethodInfo[] methods = myType.GetMethods(BindingFlags.DeclaredOnly
            | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(method.Name);
            }
        }

        static void getConstructors()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            Console.WriteLine("Конструкторы:");
            foreach (ConstructorInfo ctor in myType.GetConstructors())
            {
                Console.Write(myType.Name + " (");
                // получаем параметры конструктора
                ParameterInfo[] parameters = ctor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }
        }

        static void getFieldsAndProps()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            Console.WriteLine("Поля:");
            foreach (FieldInfo field in myType.GetFields())
            {
                Console.WriteLine("{0} {1}", field.FieldType, field.Name);
            }

            Console.WriteLine("Свойства:");
            foreach (PropertyInfo prop in myType.GetProperties())
            {
                Console.WriteLine("{0} {1}", prop.PropertyType, prop.Name);
            }
        }

        static void getInterfaces()
        {
            Type myType = Type.GetType("MetanitSharp.ReflectionWork+User", false, true);

            Console.WriteLine("Реализованные интерфейсы:");
            foreach (Type i in myType.GetInterfaces())
            {
                Console.WriteLine(i.Name);
            }
        }

        class User : IUser, IHuman
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public User(string n, int a)
            {
                Name = n;
                Age = a;
            }
            public void Display()
            {
                Console.WriteLine("Имя: {0}  Возраст: {1}", this.Name, this.Age);
            }
            public int Payment(int hours, int perhour)
            {
                return hours * perhour;
            }

            public static int GetLastId()
            {
                return 100;
            }

            public class City
            {

            }
        }

        interface IUser { }

        interface IHuman { }
    }
}
