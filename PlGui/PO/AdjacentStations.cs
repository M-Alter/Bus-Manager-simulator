using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PO
{
    public class AdjacentStations : INotifyPropertyChanged
    {
        #region Fields
        private int station1;
        private string station1Name;
        private string station2Name;
        private int station2;
        private double distance;
        private TimeSpan time;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public int Station1
        {
            get { return station1; }
            set 
            { 
                station1 = value;
                NotifyPropertyChanged();
            }
        }

        public string Station1Name
        {
            get
            {
                return station1Name;
            }
            set
            {
                station1Name = value;
                NotifyPropertyChanged();
            }
        }
        public string Station2Name
        {
            get
            {
                return station2Name;
            }
            set
            {
                station2Name = value;
                NotifyPropertyChanged();
            }
        }

        public int Station2
        {
            get { return station2; }
            set
            {
                station2 = value;
                NotifyPropertyChanged();
            }
        }

        public double Distance
        {
            get { return distance; }
            set 
            { 
                distance = value;
                NotifyPropertyChanged();

            }
        }

        public TimeSpan Time
        {
            get { return time; }
            set 
            { 
                time = value;
                NotifyPropertyChanged();

            }
        }
        #endregion

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
