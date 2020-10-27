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
            // Add 5 bus to the list
            for (int i = 0; i < 5; i++)
                buses.Add(new Bus(12345670 + i, new DateTime(2020, 10-i, i+1)));
            
            int option = -1;
            // Main program
            do
            {
                Console.WriteLine(@"Please enter your choice
1 = Add a bus;
2 = Pick a bus;
3 = Perform a maintenance;
4 = Print details;
0 = EXIT;");

                bool success = int.TryParse(Console.ReadLine(), out option);

                switch (option)
                {
                    // Add bus to the list
                    case 1:
                        addBus(buses);
                        break;
                    // Pick a bus for driving
                    case 2:
                        pickBus(buses);
                        break;
                    // Preform a service to handle the bus
                    case 3:
                        performService(buses);
                        break;
                    // Print details of buses
                    case 4:
                        printDetails(buses);
                        break;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
            } while (option != 0);
        }
        // Function to print all buses details and mileage to next treatment
        private static void printDetails(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine("Bus: {0}, milege since last service: {1}", bus, bus.MileageSinceService);
            }
        }
        // Function to pick a bus for trip
        static private void pickBus(List<Bus> buses)
        {
            int getReg;
            bool success;
            try
            {
                Console.WriteLine("Please enter the registration number:");
                success = int.TryParse(Console.ReadLine(), out getReg);
                // Validate that is a numeric
                if (!success)
                    throw new Exception("Please enter a valid number!");
                // Validate that is a 7 or 8 digits
                if (getReg > 99999999 || getReg < 1000000)
                    throw new Exception("Registration number should be 7 or 8 digits");
                // Looking the bus in list
                Bus bus = findBuses(buses, getReg);
                if (bus == null)
                    throw new Exception("This bus was not found");
                if (!bus.IsSafe)
                    throw new Exception("This bus is not safe for driving");
                // Pick a random km for travel 
                Random rnd = new Random();
                int km = rnd.Next(1, 1201);
                Console.WriteLine("Length of the journey is {0}", km);
                // Validate that the bus can drive 
                if (bus.Gas < km)
                    throw new Exception("There is not enough gas for this journey, Please refuel");
                if (bus.MileageSinceService + km > 20000)
                {
                    throw new Exception("This bus will out perform it's mileage allowance if it does this journey, please repair or choose another bus");
                }
                if (!bus.IsSafe)
                {
                    throw new Exception("A year has passed since the last service was done to this bus and therefore is unsafe, please repair or choose another bus");
                }
                // Informs the user the drive was succesfuly and update the bus values
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
        // Function to find a bus in list
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
        // Function for treatment in bus
        static public void performService(List<Bus> buses)
        {
            string answer;
            int reg;
            bool success;
            try
            {
                // Validate that the bus is correct and exist
                Console.WriteLine("Please enter the bus registration you would like to perfrom a service to:");
                success = int.TryParse(Console.ReadLine(), out reg);
                Bus bus = findBuses(buses, reg);
                if (bus == null)
                    throw new Exception("This bus was not found");
                // Suggest a service
                Console.WriteLine("Would you like to perfom a service or a refuel?");
                answer = Console.ReadLine();
                // Make the treatment by user choice
                if (answer.Contains("refuel"))
                {
                    bus.Refuel();
                }
                else if (answer.Contains("service"))
                {
                    bus.Service();
                }
                else
                {
                    throw new Exception("Invalid service request");
                }
            }
            catch (Exception problem)
            {
                Console.WriteLine(problem.Message);
                return;
            }
        }
        // Function to print all bus details (it was in use at the developing)
        static public void printAll(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine("bus: {0}, gas: {1}, milege: {2}, repair: {3}", bus, bus.Gas, bus.Mileage, bus.MileageSinceService);
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
                // Receive begin date
                Console.WriteLine("please enter the begin date in format (DD-MM-YYYY):");
                success = DateTime.TryParse(Console.ReadLine(), out beginDate);
                // Validate the details are correct
                if (!success)
                    throw new Exception("Please enter a valid Date!");
                if (!(beginDate <= DateTime.Today))
                    throw new Exception("invalid date");
                // Receive registration number
                Console.WriteLine("please enter the registration number:");
                success = int.TryParse(Console.ReadLine(), out getReg);
                // Validate the details are correct
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
            // If all right, add the bus to the list
            buses.Add(new Bus(getReg, beginDate));
            Console.WriteLine("The bus added succefully");
        }

    }
}
