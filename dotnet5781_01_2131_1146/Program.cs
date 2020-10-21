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
            List<Bus> buses = new List<Bus> {};

            for (int i = 0; i  < 5; i++)
                buses.Add(new Bus(12345670 + i, new DateTime(2020,01,01)));
            
            int option = -1;
            do
            {
                Console.WriteLine(@"Please enter your choice
1 = Add a bus;
2 = Pick a bus;
3 = Perform a maintenance;
4 = Print details;
0 = EXIT;");

                bool success = int.TryParse( Console.ReadLine(),out option);

                switch (option)
                {
                    case 1:
                        addBus(buses);                        
                        break;
                    case 2:
                        pickBus(buses);
                        break;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
            } while (option != 0 );
        }

        private static void pickBus(List<Bus> buses)
        {
            int getReg;
            bool success;
            try
            {
                Console.WriteLine("please enter the registration number:");
                success = int.TryParse(Console.ReadLine(), out getReg);
                if (!success)
                    throw new Exception("Please enter a valid number!");
                if (getReg > 99999999 || getReg < 1000000)
                    throw new Exception("Registration number should be 7 or 8 digits");
                
                Bus bus = findBuses(buses, getReg);
                if (bus == null)
                    throw new Exception("This bus was not founded");
                //// Should add random correctly////
                //Random r = new Random(DateTime.Now.Millisecond);
                //int longDrive = r;
                //Console.WriteLine(longDrive);


            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message);
                return;
            }

        }

        private static Bus findBuses(List<Bus> buses, int reg)
        {
            Bus bus = null;
            foreach (Bus item in buses)
            {
                if (item.Reg == reg)
                {
                    bus = item;
                }
            }
            return bus;
        }

        public static void printAll(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine(bus);
            }
        }

        // Function to add a new bus to the list
        private static void addBus(List<Bus> buses)
        {
            int getReg;
            DateTime beginDate;
            bool success;
            try
            {
                // Receive registration and begin date
                Console.WriteLine("please enter the begin date in format (DD-MM-YYYY):");
                success = DateTime.TryParse(Console.ReadLine(), out beginDate);
                if (!success)
                    throw new Exception("Please enter a valid Date!");
                if (!(beginDate <= DateTime.Today))
                    throw new Exception("invalid date");

                Console.WriteLine("please enter the registration number:");
                success = int.TryParse(Console.ReadLine(), out getReg);
                if (!success)
                    throw new Exception("Please enter a valid number!");
                if (beginDate.Year >= 2018)
                    if (getReg > 99999999 || getReg < 10000000)
                        throw new Exception("Registration number since 2018 should be 8 digits");
                else if (beginDate.Year < 2018)
                    if (getReg > 9999999 || getReg < 1000000)
                        throw new Exception("Registration number until 2017 should be 7 digits");
            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message);
                return;
            }

            buses.Add(new Bus(getReg, beginDate));
            Console.WriteLine("The bus added succefully");
        }
    
    }
}
