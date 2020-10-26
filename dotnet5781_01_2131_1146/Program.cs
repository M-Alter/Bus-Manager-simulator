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
                buses.Add(new Bus(12345670 + i, new DateTime(2020, 01, 01)));
            
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
                    case 3:
                        performService(buses);
                        break;
                    case 4:
                        printDetails(buses);
                        break;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
            } while (option != 0 );
        }

        private static void printDetails(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine("Bus: {0}, milege since last service: {1}", bus.Reg, bus.MileageSinceService);
            }
        }

        static private void pickBus(List<Bus> buses)
        {
            int getReg;
            bool success;
            try
            {
                Console.WriteLine("Please enter the registration number:");
                success = int.TryParse(Console.ReadLine(), out getReg);
                if (!success)
                    throw new Exception("Please enter a valid number!");
                if (getReg > 99999999 || getReg < 1000000)
                    throw new Exception("Registration number should be 7 or 8 digits");

                Bus bus = findBuses(buses, getReg);
                if (bus == null)
                    throw new Exception("This bus was not found");
                if (!bus.IsSafe)
                    throw new Exception("This bus is not safe for driving");

                Random rnd = new Random();
                int km = rnd.Next(1,1201);
                Console.WriteLine("Length of the journey is {0}", km);
                if (bus.Gas < km)
                    throw new Exception("There is not enough gas for driving, Please refuel");
                if (bus.MileageSinceService + km > 20000)
                {
                    throw new Exception("This bus will out perform it's mileage if it does this journey, please repair or choose another bus");
                }
                if (!bus.IsSafe)
                {
                    throw new Exception("A year has passed since the last service was done to this bus and therefore is unsafe, please repair or choose another bus");
                }
                // SHould check how to validate that the "lastRepair" is lass then a year
                // Informs the user the drive was succesfuly and update the bus valuse
                Console.WriteLine("You can drive, safe driving!");
                bus.setDrivingValues(km);

                return;
                


            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message);
                return;
            }

        }

        static private Bus findBuses(List<Bus> buses, int reg)
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


        static public void performService(List<Bus> buses)
        {
            string answer;
            int reg;
            bool success;
            try
            {
                Console.WriteLine("Please enter the bus registration you would like to perfrom a service to:");
                success = int.TryParse(Console.ReadLine(), out reg);
                Bus bus = findBuses(buses, reg);
                if (bus == null)
                    throw new Exception("This bus was not found");

                Console.WriteLine("Would you like to perfom a service or a refuel?");
                answer = Console.ReadLine();
                if (answer.Contains("refuel"))
                {
                    bus.Refuel();
                }
                else if (answer.Contains("sevice"))
                {
                    bus.Service();
                }
            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message);
                return;
            }
        }

        static public void printAll(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine("bus: {0}, gas: {1}, milege: {2}, repair: {3}",bus, bus.Gas, bus.Mileage, bus.MileageSinceService);
            }
        }

        // Function to add a new bus to the list
        static private void addBus(List<Bus> buses)
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
