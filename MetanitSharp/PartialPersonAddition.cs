using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    public partial class PartialPerson
    {
        public void Eat()
        {
            Console.WriteLine("I am eating");
        }

        partial void DoSomethingElse()
        {
            Console.WriteLine("I am reading a book");
        }
    }
}
