using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_00_2131_1146
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2131();
            Welcome1146();
            Console.ReadKey();
        }


        static partial void Welcome1146();

        private static void Welcome2131()
        {
            Console.WriteLine("Enter yout name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first conosole application", name);
        }
    }
}
