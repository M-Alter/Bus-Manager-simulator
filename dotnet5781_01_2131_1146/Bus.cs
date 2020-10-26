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
        private int mileage = 0;
        private int gas = 1200;

        private int mileageSinceService = 0;
        private int mileageAtService = 0;
        private DateTime lastServiceDate;
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
        
        public int Mileage
        {
            get
            {
                return mileage;
            }
            private set
            {
                mileage = value;
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

        public int MileageSinceService
        {
            get
            {
                return mileageSinceService;
            }
            private set
            {
                mileageSinceService = value;      
            }
        }


        public int MileageAtService
        {
            get
            {
                return mileageAtService;
            }
            private set
            {
                mileageAtService = value;
            }
        }

        public bool IsSafe
        {
            get
            {
                if ((DateTime.Now - lastServiceDate).TotalDays > 365 )
                {
                   this.isSafe = false;
                }
                return this.isSafe;
            }
            private set
            {
                isSafe = value;
            }
        }

        public void setDrivingValues(int value)
        {
            Mileage += value;
            Gas -= value;
            MileageSinceService += value;          


        }


        public void Service()
        {
            lastServiceDate = DateTime.Today;
            mileageSinceService = 0;
            mileageAtService = mileage;
            isSafe = true;
        }

        public void Refuel()
        {
            gas = 1200;
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
