using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    class Bus
    {
        private int licenseNum;
        private DateTime fromDate;
        private double totalTrip;
        private double fuelRemain;
        private BusStatus status;

        public int LicenseNum
        {
            get { return licenseNum; }
            set { licenseNum = value; }
        }


        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }


        public double TotalTrip
        {
            get { return totalTrip; }
            set { totalTrip = value; }
        }


        public double FuelRemain
        {
            get { return fuelRemain; }
            set { fuelRemain = value; }
        }


        public BusStatus Status
        {
            get { return status; }
            set { status = value; }
        }



    }
}
