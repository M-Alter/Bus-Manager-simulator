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

        /// <summary>
        /// c"tor
        /// </summary>
        public BusCollection()
        {
            BusLines = new List<BusLine>();
        }

        /// <summary>
        /// adds a busLine to the collection
        /// </summary>
        /// <param name="busLine">busLine to add</param>
        public void Add(BusLine busLine)
        {
            if (lines.ContainsKey(busLine.LineNumber) && lines[busLine.LineNumber] == 2)
            {
                throw new BusException("This line already exists twice");
            }
            if (lines.ContainsKey(busLine.LineNumber))
                if (lines[busLine.LineNumber] == 2)
                    throw new BusException("this line exists alreaady twice");
                else if (lines[busLine.LineNumber] == 1)
                    if (!(busLine.Begin.GetNumber == this[busLine.LineNumber].End.GetNumber) || !(busLine.End.GetNumber == this[busLine.LineNumber].Begin.GetNumber))
                        throw new BusException(string.Format("This is a return route, route must begin with {0} and end with {1}", this[busLine.LineNumber].End.GetNumber, this[busLine.LineNumber].Begin.GetNumber));
                    else
                        lines[busLine.LineNumber] = 2;
            BusLines.Add(busLine);
            if (!lines.ContainsKey(busLine.LineNumber))
                lines.Add(busLine.LineNumber, 1);
        }

        /// <summary>
        /// adds a stop to an existing busLine
        /// </summary>
        /// <param name="line">Line to add the stop to</param>
        public void AddStop(int line)
        {
            foreach (var item in BusLines)
            {
                if (item.LineNumber == line)
                {
                    item.AddStop();
                    return;
                }
            }
            throw new BusException("This line doesn't exist");
        }

        /// <summary>
        /// removes a line from the bus company
        /// </summary>
        public void RemoveLine()
        {
            int indexBegin, indexEnd;
            bool success;
            Console.WriteLine("This is all the line in this company:");
            foreach (var item in BusLines)
                Console.WriteLine(item.LineNumber);
            Console.WriteLine("\nWhich line do you want to remove?");
            success = int.TryParse(Console.ReadLine(), out indexBegin);
            if (!success)
                throw new BusException("Wrong input");
            if (lines.ContainsKey(indexBegin) && lines[indexBegin] == 2)
            {
                int i = 1;
                BusLine b1 = default(BusLine), b2 = default(BusLine);
                Console.WriteLine("There is two ways for this line, which one do you want?");
                foreach (var item in BusLines)
                {
                    if (item.LineNumber == indexBegin)
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
                success = int.TryParse(Console.ReadLine(), out indexEnd);
                if (success)
                    success = (indexEnd == 1 || indexEnd == 2);
                if (!success)
                    throw new BusException("Wrong input");
                if (indexEnd == 1)
                {
                    BusLines.Remove(b1);
                }
                else
                {
                    BusLines.Remove(b2);
                }
                lines[indexBegin] = 1;
            }
            else if (lines.ContainsKey(indexBegin) && lines[indexBegin] == 1)
            {
                BusLines.Remove(this[indexBegin]);
                lines.Remove(indexBegin);
            }
            else
                throw new BusException("This line doesn't exist in this company");
        }

        /// <summary>
        /// removes a stop from the exisitng Busline
        /// </summary>
        /// <param name="line">BusLine to remove the stop from</param>
        public void RemoveStop(int line)
        {
            foreach (var item in BusLines)
            {
                if (item.LineNumber == line)
                {
                    item.RemoveStop();
                    return;
                }
            }
            throw new BusException("This line doesn't exist");
        }

        /// <summary>
        /// prints all the Buslines that pass this stop
        /// </summary>
        /// <param name="stopNumber">stop to print</param>
        public void BusStopLines(int stopNumber)
        {
            bool flag = false;
            foreach (var item in BusLines)
                if (item.Contains(stopNumber))
                {
                    flag = true;
                    Console.WriteLine(item.LineNumber + " " + item.Area);
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
            Console.WriteLine("1:GENERAL\n2: JERUSALEM\n3: NORTH\n4: SOUTH\n5: CENTER");
        }

        public IEnumerator GetEnumerator()
        {
            return new BusEnumerator(BusLines, BusLines.Count);
        }


        public void PrintSorted()
        {
            BusLines.Sort();
            foreach (var item in BusLines)
                Console.WriteLine(item.LineNumber + " Total Travel Time: " + item.TotalTravelTime());
        }

        public void GetRoutes(BusStop begin, BusStop end)
        {
            List<BusLine> result;
            result = new List<BusLine>();
            foreach (BusLine item in BusLines)
                try
                {
                    result.Add(item.SubLine(begin, end));
                }
                catch (BusException) { }
            result.Sort();
            result.Reverse();
            foreach (BusLine item in result)
            {
                Console.WriteLine("{0}: {1}", item.LineNumber, item.TotalTravelTime());
            }

        }
    }



}