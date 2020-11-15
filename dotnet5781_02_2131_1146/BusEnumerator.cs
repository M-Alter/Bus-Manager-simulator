using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    public class BusEnumerator : IEnumerator
    {
        List<BusLine> BusLines;
        int index = -1;
        int count;
        public BusEnumerator(List<BusLine> BusLines, int count)
        {
            this.BusLines = BusLines;
            this.count = count;
        }
        public object Current
        {
            get { return BusLines[index]; }
        }
        public bool MoveNext()
        {
            ++index;
            if (index >= count)
            {
                index = -1;
                return false;
            }
            return true;
        }
        public void Reset()
        {
            index = -1;
        }



    }
}
