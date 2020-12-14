﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace dotnet5781_03B_2131_1146
{
    public class Bus : INotifyPropertyChanged
    {
        #region Fields
        private const int MAX_GAS = 1200;

        private const int MINUTE = 100;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;

        private static List<int> regNumbers = new List<int>();

        private readonly int reg;
        private readonly DateTime beginDate;
        private ServiceDetails serviceDetails;

        private int mileage;
        private int gas;

        private State busState = State.READY;
        private String busStateColor = "LawnGreen";
        private String busStateString = "Ready";
        private bool isSafe = true;
        static Random r = new Random(DateTime.Now.Millisecond);

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region C'tor
        // Constractor of class
        public Bus(int reg, DateTime beginDate, int mileage = 0, int gas = 1200, DateTime dateTime = default(DateTime))
        {
            //if (reg < 1000000 || reg > 99999999)
            //    throw new Exception("Invalid number");
            if (!regNumbers.Contains(reg))
            {
                this.reg = reg;
                this.beginDate = beginDate;
                if (dateTime == default(DateTime))
                    this.serviceDetails.lastServiceDate = DateTime.Now;
                else
                    this.serviceDetails.lastServiceDate = dateTime;
                regNumbers.Add(reg);
                this.Mileage = mileage;
                this.serviceDetails.mileageSinceService = mileage;
                Gas = gas;
                this.setBusState();
            }
            else
                throw new Exception("this reg number exists already");
        }
        #endregion

        #region Properties
        // Getter for beginDate
        public string BeginDate
        {
            get => beginDate.ToShortDateString();
        }

        //Getter for reg
        public int Reg
        {
            get => reg;
        }

        public string RegString
        {
            get
            {
                if (beginDate.Year < 2018)
                {
                    return reg.ToString("00-000-00");
                }
                return reg.ToString("000-00-000");
            }
        }

        // Getter and setter for mileage
        public int Mileage
        {
            get => mileage;
            private set
            {
                if (value > 0)
                {
                    mileage = value;
                    NotifyPropertyChanged();
                }
                else
                    Console.WriteLine("mileage cant be negative");
            }
        }

        // Getter and setter for gas
        public int Gas
        {
            get => gas;
            set
            {
                if (value <= 1200 || value > 0)
                {
                    gas = value;
                    NotifyPropertyChanged();
                }
                else
                    Console.WriteLine("you have enter an invalid gas amount");
            }
        }

        // Getter and setter for mileageSinceService
        public int MileageSinceService
        {
            get => serviceDetails.mileageSinceService;
            private set
            {
                serviceDetails.mileageSinceService = value;
                NotifyPropertyChanged();
            }
        }

        // Getter and setter for the last service date
        public String ServiceDate
        {
            get => serviceDetails.lastServiceDate.ToShortDateString();
            private set
            {
                DateTime.TryParse(value, out serviceDetails.lastServiceDate);
                NotifyPropertyChanged();
            }
        }

        //bus stete
        public State BusState
        {
            get { return busState; }
            set
            {
                busState = value;
                NotifyPropertyChanged();
            }
        }

        public String BusStateColor
        {
            get { return busStateColor; }
            set
            {
                busStateColor = value;
                NotifyPropertyChanged();
            }
        }

        public String BusStateString
        {
            get { return busStateString; }
            set
            {
                busStateString = value;
                NotifyPropertyChanged();
            }
        }

        // Getter and setter for isSafe
        public bool IsSafe
        {
            get
            {
                if ((DateTime.Now - serviceDetails.lastServiceDate).TotalDays > 365)
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

        #endregion

        #region Methods
        // Update the values after driving 
        public void setDrivingValues(int value)
        {
            Mileage += value;
            Gas -= value;
            MileageSinceService += value;
        }

        public void setBusState()
        {
            if ((DateTime.Today - serviceDetails.lastServiceDate).TotalDays > 365)
            {
                this.busState = State.NOTREADY;
                setBusStateColor();
            }
        }

        public void IsReadyToPick(int km)
        {
            if (serviceDetails.mileageSinceService + km > 20000)
                throw new InvalidOperationException("this bus will exceed its mileage allowance in this journey");
            if (Gas - km < 0)
                throw new InvalidOperationException("there isnt enough gas to complete this journey");
            if ((DateTime.Today - serviceDetails.lastServiceDate).TotalDays > 365)
                throw new InvalidOperationException("The last service was more then a  year ago");

        }

        public void setBusStateColor()
        {
            switch (BusState)
            {
                case State.READY:
                    BusStateColor = "LawnGreen";
                    BusStateString = "Ready";
                    break;
                case State.BUSY:
                    BusStateColor = "Red";
                    BusStateString = "Busy";
                    break;
                case State.REFUELING:
                    BusStateColor = "Orange";
                    break;
                case State.SERVICING:
                    BusStateColor = "Gray";
                    break;
                case State.NOTREADY:
                    BusStateColor = "Red";
                    BusStateString = "Not ready";
                    break;
                default:
                    break;
            }
        }

        // Update the values after a service
        public void Service()
        {
            this.BusState = State.SERVICING;
            serviceDetails.lastServiceDate = DateTime.Today;
            serviceDetails.mileageSinceService = 0;
            Gas = MAX_GAS;
            IsSafe = true;
            Thread.Sleep(DAY);
            Console.WriteLine("Your vehicle has been serviced successfully");
            this.BusState = State.READY;
        }

        // Print bus details in requiered format 
        public override string ToString()
        {
            if (beginDate.Year < 2018)
                return String.Format("{0}, {1}", reg.ToString("00-000-00"), beginDate.ToShortDateString());
            else
                return String.Format("{0}, {1}", reg.ToString("000-00-000"), beginDate.ToShortDateString());
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
