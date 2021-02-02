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
        /// <summary>
        /// license number
        /// </summary>
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

        /// <summary>
        /// return a string instance of the license num
        /// </summary>
        public string LicenseString { get; private set; }

        /// <summary>
        /// date the vehicle came on the street
        /// </summary>
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        /// <summary>
        /// total milege of the vehicle
        /// </summary>
        public double TotalTrip
        {
            get { return totalTrip; }
            set { totalTrip = value; }
        }
    
        /// <summary>
        /// fuel reamianing in the tank
        /// </summary>
        public double FuelRemain
        {
            get { return fuelRemain; }
            set { fuelRemain = value; }
        }

        /// <summary>
        /// current bus status
        /// </summary>
        public Enums.BusStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        public override string ToString()
        {
            return LicenseString;
        }
    }
}
