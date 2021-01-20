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
    public class Line : INotifyPropertyChanged
    {
        #region Fields
        private int personalId;
        private int lineNumber;
        private Enums.Areas areas;
        private int firstStation;
        private int lastStation;
        private string firstStationName;
        private string lastStationName;
        private bool isActive = true;
        private IEnumerable<LineStation> stations;
        private IEnumerable<TimeSpan> timing;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public int PersonalId
        {
            get { return personalId; }
            set { personalId = value; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public Enums.Areas Area
        {
            get { return areas; }
            set { areas = value; }
        }

        public int FirstStation
        {
            get { return firstStation; }
            set
            {
                firstStation = value;
                NotifyPropertyChanged();
            }
        }

        public string FirstStationName {
            get
            {
                return firstStationName;
            }
            set
            {
                firstStationName = value;
                NotifyPropertyChanged();
            }
        }

        public int LastStation
        {
            get { return lastStation; }
            set
            {
                lastStation = value;
                NotifyPropertyChanged();
            }
        }

        public string LastStationName {
            get
            {
                return lastStationName;
            }
            set
            {
                lastStationName = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<LineStation> Stations {
            get
            {
                return stations;
            }
            set
            {
                stations = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<TimeSpan> Timing{
            get
            {
                return timing;
            }
            set
            {
                timing = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
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
