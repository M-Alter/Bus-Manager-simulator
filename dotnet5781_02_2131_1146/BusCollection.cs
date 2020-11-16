using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotnet5781_02_2131_1146
{
    public class BusCollection : IEnumerable
    {
        private List<BusLine> BusLines;
        private static Dictionary<int, int> lines = new Dictionary<int, int>();


        public BusCollection()
        {
            BusLines = new List<BusLine>();
        }
        public void addLine(BusLine busLine)
        {
            if (lines.ContainsKey(busLine.LineNumber) && lines[busLine.LineNumber] == 2)
            {
                throw new BusException("This line already exists twice");
            }
            if (lines.ContainsKey(busLine.LineNumber))
                lines[busLine.LineNumber] = 2;
            else
                lines.Add(busLine.LineNumber, 1);
            BusLines.Add(busLine);
        }

        public void RemoveLine()
        {
            int input, input2;
            bool success;
            Console.WriteLine("This is all the line in this compeny:");
            foreach (var item in BusLines)
                 Console.WriteLine(item.LineNumber);
            Console.WriteLine("\nWhich line do you want to remove?");
            success = int.TryParse(Console.ReadLine(), out input);
            if (!success)
                throw new BusException("Wrong input");
            if (lines.ContainsKey(input) && lines[input] == 2)
            {
                int i = 1;
                BusLine b1 = default(BusLine), b2 = default(BusLine);
                Console.WriteLine("There is two ways for this line, which one do you want?");
                foreach (var item in BusLines)
                {
                    if (item.LineNumber == input)
                    {
                        Console.WriteLine($"{i}: {item}");
                        if (i == 1)
                        {
                            b1 = item;
                            i++;
                        }
                        else
                            b2 = item;
                    }
                }
                success = int.TryParse(Console.ReadLine(), out input2);
                if (success)
                    success = (input2 == 1 || input2 == 2);
                if (!success)
                    throw new BusException("Wrong input");
                if (input2 == 1)
                {
                    BusLines.Remove(b1);
                }
                else
                {
                    BusLines.Remove(b2);
                }
                lines[input] = 1;
            }
            else if (lines.ContainsKey(input) && lines[input] == 1)
            {
                BusLines.Remove(BusLines[input]);
                lines.Remove(input);
            }
            else
                throw new BusException("This line doesn't exist in this company");
        }

        public void BusStopLines(int stopNumber)
        {
            bool flag = false;
            foreach (var item in BusLines)
                if (item.Contains(stopNumber))
                {
                    flag = true;
                    Console.WriteLine(item);
                }
            if (!flag)
                throw new BusException("This stop has no lines");
        }

        public BusLine this[int index]
        {
            get
            {
                foreach (var item in BusLines)
                    if (item.LineNumber == index)
                        return item;
                throw new BusException("This item doesn't exist");
            }
        }

        public void Print() 
        {
            foreach (var item in BusLines)
            {
                Console.WriteLine(item);
            }
        }

        public static void PrintAreas()
        {
            Console.WriteLine("1:General\n2: Jerusalem\n3: North\n4: South\n5: Center");
        }

        public IEnumerator GetEnumerator()
        {
            return new BusEnumerator(BusLines, BusLines.Count);
        }
    }


    
}