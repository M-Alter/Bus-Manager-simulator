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
        // Constractor of class
        public Bus(int reg, DateTime beginDate)
        {
            this.reg = reg;
            this.beginDate = beginDate;
            this.lastServiceDate = beginDate;
        }
        // Getter for beginDate
        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
        }
        // Getter for reg
        public int Reg
        {
            get
            {
                return reg;
            }
        }
        // Getter and setter for mileage
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
        // Getter and setter for gas
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
        // Getter and setter for mileageSinceService
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
        // Getter and setter for mileageAtService
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
        // Getter and setter for isSafe
        public bool IsSafe
        {
            get
            {
                if ((DateTime.Now - lastServiceDate).TotalDays > 365)
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
        // Update the values after driving 
        public void setDrivingValues(int value)
        {
            Mileage += value;
            Gas -= value;
            MileageSinceService += value;
        }
        // Update the values after a service
        public void Service()
        {
            lastServiceDate = DateTime.Today;
            mileageSinceService = 0;
            mileageAtService = mileage;
            isSafe = true;
            Console.WriteLine("Your vehicle has been serviced successfully");
        }
        // Update the values after Refuel
        public void Refuel()
        {
            gas = 1200;
            Console.WriteLine("Your vehicle has been refueled successfully");
        }
        // Print bus details in requiered format 
        public override string ToString()
        {
            int prefix, middle, suffix, temp = reg;
            if (beginDate.Year < 2018)
            {
                //prefix = temp % 100; temp /= 100;
                //middle = temp % 1000; temp /= 1000;
                //suffix = temp;
                return reg.ToString("00-000-00");
            }
            else
            {
                //prefix = temp % 1000; temp /= 1000;
                //middle = temp % 100; temp /= 100;
                //suffix = temp;
                return reg.ToString("000-00-000");
            }
            //string registration = String.Format("{0}-{1}-{2}", suffix, middle, prefix);

            //return String.Format("[{0}, {1}]", registration, beginDate.ToShortDateString());
        }

    }
}
