using System;
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

        private float mileage;
        private float gas;

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
        
        /// <summary>
        /// get the Begin date of the bus
        /// </summary>
        public string BeginDate
        {
            get => beginDate.ToShortDateString();
        }

        /// <summary>
        /// getter for the reg
        /// </summary>
        public int Reg
        {
            get => reg;
        }

        /// <summary>
        /// returns the reg as a string type
        /// </summary>
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


        /// <summary>
        /// Getter and setter for mileage
        /// </summary>
        public float Mileage
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
        
        /// <summary>
        /// Getter and setter for gas
        /// </summary>
        public float Gas
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

        /// <summary>
        /// Getter and setter for mileageSinceService
        /// </summary>
        public float MileageSinceService
        {
            get => serviceDetails.mileageSinceService;
            private set
            {
                serviceDetails.mileageSinceService = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Getter and setter for the last service date
        /// </summary>
        public String ServiceDate
        {
            get => serviceDetails.lastServiceDate.ToShortDateString();
            private set
            {
                DateTime.TryParse(value, out serviceDetails.lastServiceDate);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// get and set the current State of the bus
        /// </summary>
        public State BusState
        {
            get => busState; 
            set
            {
                busState = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// set ang get the colour of the current state of the bus
        /// </summary>
        public String BusStateColor
        {
            get => busStateColor;
            set
            {
                busStateColor = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// current state of the bus in words
        /// </summary>
        public String BusStateString
        {
            get => busStateString;
            set
            {
                busStateString = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// updates the respected fields after a ride 
        /// </summary>
        /// <param name="value"></param>
        public void setDrivingValues(int value)
        {
            Mileage += value;
            Gas -= value;
            MileageSinceService += value;
        }
        
        /// <summary>
        /// after a service updates the values respective to the service
        /// </summary>
        public void setServicingValue()
        {
            Gas = 1200;
            MileageSinceService = 0;
            ServiceDate = DateTime.Today.ToShortDateString();
        }

        /// <summary>
        /// checks the bus and provides it with it state
        /// </summary>
        public void setBusState()
        {
            if ((DateTime.Today - serviceDetails.lastServiceDate).TotalDays > 365 || Gas == 0)
                this.busState = State.NOTREADY;             
            else
                this.busState = State.READY;
            setBusStateColor();
        }

        /// <summary>
        /// throws an exception if the bus can't make the journey
        /// </summary>
        /// <param name="km"></param>
        public void IsReadyToPick(int km)
        {
            if (serviceDetails.mileageSinceService + km > 20000)
                throw new InvalidOperationException("this bus will exceed its mileage allowance in this journey");
            if (Gas - km < 0)
                throw new InvalidOperationException("there isnt enough gas to complete this journey");
            if ((DateTime.Today - serviceDetails.lastServiceDate).TotalDays > 365)
                throw new InvalidOperationException("The last service was more then a  year ago");

        }

        /// <summary>
        /// returns a colour respective to the state
        /// </summary>
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

        /// <summary>
        /// Print bus details in requiered format 
        /// </summary>
        /// <returns>a string in the required format</returns>
        public override string ToString()
        {
            if (beginDate.Year < 2018)
                return String.Format("{0}, {1}", reg.ToString("00-000-00"), beginDate.ToShortDateString());
            else
                return String.Format("{0}, {1}", reg.ToString("000-00-000"), beginDate.ToShortDateString());
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

        #endregion

    }
}
