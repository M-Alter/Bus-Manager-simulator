﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace dotnet5781_03B_2131_1146
{
    public class Bus 
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

        private bool isSafe = true;
        #endregion

        #region C'tor
        // Constractor of class
        public Bus(int reg, DateTime beginDate, int mileage = 0, int gas = 1200, DateTime dateTime = default(DateTime))
        {
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
                    mileage = value;
                else
                    Console.WriteLine("mileage cant be negative");
            }
        }

        // Getter and setter for gas
        public int Gas
        {
            get => gas;
            private set
            {
                if (value <= 1200 || value > 0)
                    gas = value;
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
            }
        }

        // Getter and setter for the last service date
        public DateTime ServiceDate 
        {
            get => serviceDetails.lastServiceDate;
            private set
            {
                serviceDetails.lastServiceDate = value;
            }
        }

        //bus stete
        public State BusState
        {
            get { return busState; }
            set { busState = value; }
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
        // Update the values after Refuel
        public void Refuel()
        {
            this.BusState = State.REFUELING;
            Thread.Sleep(2 * HOUR);
            this.BusState = State.READY;
            Console.WriteLine("Your vehicle has been refueled successfully");
        }
        // Print bus details in requiered format 
        public override string ToString()
        {
            if (beginDate.Year < 2018)
                return String.Format("{0}, {1}", reg.ToString("00-000-00"), beginDate.ToShortDateString());
            else
                return String.Format("{0}, {1}", reg.ToString("000-00-000"), beginDate.ToShortDateString());
        }

        #endregion

    }
}
