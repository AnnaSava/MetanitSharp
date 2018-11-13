using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    public partial class PartialPerson
    {
        public void Move()
        {
            Console.WriteLine("I am moving");
        }

        partial void DoSomethingElse();

        public void DoSomething()
        {
            Console.WriteLine("Start");
            DoSomethingElse();
            Console.WriteLine("Finish");
        }
    }
}
