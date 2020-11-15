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


        public void PrintAreas()
        {
            Console.WriteLine("1:General\n2: Jerusalem\n3: North\n4: South\n5: Center");
        }

        public IEnumerator GetEnumerator()
        {
            return new BusEnumerator(BusLines, BusLines.Count);
        }
    }


    
}