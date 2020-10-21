using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_01_2131_1146
{
    class Bus
    {
        private string reg;
        private DateTime beginDate;
        private int milege;
        private bool isSafe = true;

        public Bus (string reg, DateTime beginDate)
        {
            this.reg = reg;
            this.beginDate = beginDate;
        }

        internal static void add(int getReg, DateTime beginDate)
        {

        }
    }
}
