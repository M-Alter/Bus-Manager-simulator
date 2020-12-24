using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {

        #region Fields
        private int licenseNum;
        private DateTime fromDate;
        private double totalTrip;
        private double fuelRemain;
        private Enums.BusStatus status;
        #endregion

        #region Properties
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

        public Enums.BusStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

    }
}
