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
        private int gas = 1200;

        private int milgeToRepair = 0;
        private DateTime lastRepair;
        private bool isSafe = true;

        public Bus(int reg, DateTime beginDate)
        {
            this.reg = reg;
            this.beginDate = beginDate;
        }

        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
        }

        public int Reg
        {
            get
            {
                return reg;
            }
        }
        
        public int Milege
        {
            get
            {
                return milege;
            }
            private set
            {
                milege = value;
            }
        }
        public int Gas
        {
            get
            {
                return gas;
            }
            private set
            {
                gas = value;
            }
        }

        public int MilgeToRepair
        {
            get
            {
                return milgeToRepair;
            }
            private set
            {
                milgeToRepair = value;
                if (milgeToRepair > 20000)
                    ISSafe = false;                    
            }
        }
        public bool ISSafe
        {
            get
            {
                return isSafe;
            }
            private set
            {
                isSafe = value;
            }
        }

        public void setDrivingValues(int value)
        {
            Milege += value;
            Gas -= value;
            MilgeToRepair += value;          


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
