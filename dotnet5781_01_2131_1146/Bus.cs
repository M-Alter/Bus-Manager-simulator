using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_01_2131_1146
{
    public class Bus
    {
        private int reg;
        private DateTime beginDate;
        private int milege = 0;
        private int gas = 0;

        private int milgeToRepair = 0;
        private DateTime lastRepair;
        private bool isSafe = true;

        public Bus (int reg, DateTime beginDate)
        {
            this.reg = reg;
            this.beginDate = beginDate;
        }

        public int Reg
        {
            get
            {
                return reg;
            }
        }
        public override string ToString()
        {
            int prefix, middle, suffix, temp = reg;
            if (beginDate.Year < 2018)
            {
                prefix = temp % 100; temp /= 100;
                middle = temp % 1000; temp /= 1000;
                suffix = temp;
            }
            else
            {
                prefix = temp % 1000; temp /= 1000;
                middle = temp % 100; temp /= 100;
                suffix = temp;
            }
            string registration = String.Format("{0}-{1}-{2}", suffix, middle, prefix);

            return String.Format("[{0}, {1}]", registration, beginDate.ToShortDateString());
        }

    }
}
