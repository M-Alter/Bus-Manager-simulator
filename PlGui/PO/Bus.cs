using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Bus : INotifyPropertyChanged
    {

        #region Fields
        private int licenseNum;
        private DateTime fromDate;
        private double totalTrip;
        private double fuelRemain;
        private Enums.BusStatus status;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

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
                NotifyPropertyChanged();
            }
        }

        public string LicenseString { get; private set; }

        public DateTime FromDate
        {
            get { return fromDate; }
            set
            { 
                fromDate = value;
                NotifyPropertyChanged();
            }
        }

        public double TotalTrip
        {
            get { return totalTrip; }
            set 
            {
                totalTrip = value; 
                NotifyPropertyChanged(); 
            }
        }

        public double FuelRemain
        {
            get { return fuelRemain; }
            set 
            { 
                fuelRemain = value; 
                NotifyPropertyChanged();
            }
        }

        public Enums.BusStatus Status
        {
            get { return status; }
            set 
            {
                status = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public override string ToString()
        {
            return LicenseString;
        }

        /// <summary>
        /// gets the name of the property automatically and updates the UI if changed
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
