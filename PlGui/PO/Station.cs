using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Station : INotifyPropertyChanged
    {
        #region Fields
        private int code;
        private string name;
        private double longitude;
        private double lattitude;
        private List<PO.StationLine> linesAtStation;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public int Code
        {
            get { return code; }
            set 
            {    
                code = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set { 
                name = value;
                NotifyPropertyChanged();
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set 
            { 
                longitude = value;
                NotifyPropertyChanged();
            }
        }

        public double Lattitude
        {
            get { return lattitude; }
            set 
            {
                lattitude = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return string.Format($"{Name} {Code}");
        }
        public List<PO.StationLine> LinesAtStation {
            get
            {
                return linesAtStation;
            }
            set 
            {
                linesAtStation = value;
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
