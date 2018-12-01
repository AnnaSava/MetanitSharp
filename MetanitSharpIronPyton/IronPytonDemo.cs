using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharpIronPyton
{
    public class IronPytonDemo
    {
        public static void Display()
        {
            demo();
            demoFile();
            demoScriptScope();
            demoGetFunction();
        }

        static string pythonPath = @"C:/Metanit/Python/";

        static void demo()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.Execute("print 'hello, world'");
        }

        static void demoFile()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(pythonPath + "hello.py");
        }

        static void demoScriptScope()
        {
            int yNumber = 22;
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("y", yNumber);
            engine.ExecuteFile(pythonPath + "hello2.py", scope);
            dynamic xNumber = scope.GetVariable("x");
            dynamic zNumber = scope.GetVariable("z");
            Console.WriteLine("Сумма {0} и {1} равна: {2}", xNumber, yNumber, zNumber);
        }

        static void demoGetFunction()
        {
            Console.WriteLine("Введите число:");
            int x = Int32.Parse(Console.ReadLine());

            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            engine.ExecuteFile(pythonPath + "factorial.py", scope);
            dynamic function = scope.GetVariable("factorial");
            // вызываем функцию и получаем результат
            dynamic result = function(x);
            Console.WriteLine(result);
        }
    }
}
