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
            set 
            { 
                licenseNum = value;
                if (value > 9999999)
                {
                    LicenseString = licenseNum.ToString("000-00-000");
                }
                else
                {
                    LicenseString = licenseNum.ToString("00-000-00");
                }
            }
        }

        public string LicenseString { get; private set; }

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
