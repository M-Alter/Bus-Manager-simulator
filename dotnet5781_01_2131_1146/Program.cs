using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_01_2131_1146
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus> { };
            int option = -1;
            do
            {
                Console.WriteLine(@"Please enter your choice
1 = Add a bus:
2 = Pick a bus:
3 = Perform a maintenance:
4 = Print details:
0 = EXIT:");
                bool success = int.TryParse( Console.ReadLine(),out option);
                switch (option)
                {
                    case 1:
                        addBus(buses);
                        
                        break;
                    default:
                        break;
                }
            } while (option != 0 );
        }





        private static void addBus(List<Bus> buses)
        {
            int getReg;
            string reg;
            DateTime beginDate;
            bool success;

            Console.WriteLine("please enter the registration number:");
            success = int.TryParse(Console.ReadLine(), out getReg);
            if (!success)
            {
                Console.WriteLine("Please enter a valid number!");
                return;
            }
            Console.WriteLine("please enter the begin date:");
            success = DateTime.TryParse(Console.ReadLine(), out beginDate);
            if (!success)
            {
                Console.WriteLine("Please enter a valid Date! (DD-MM-YYYY)");
                return;
            }
            try
            {
               bool dateGood =  beginDate <= DateTime.Today;
                if (!dateGood)
                {
                    throw new Exception ("invalid date");
                }
            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message); ;
            }
            Bus.add(getReg, beginDate);
        }
    
    }
}
