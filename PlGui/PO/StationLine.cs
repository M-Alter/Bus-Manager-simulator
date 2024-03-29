﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class StationLine : INotifyPropertyChanged
    {

        private int lineNumber;
        private string lastStation;


        public event PropertyChangedEventHandler PropertyChanged;
        public int LineNumber
        {
            get
            {
                return lineNumber;
            }
            set
            {
                lineNumber = value;
                NotifyPropertyChanged();
            }

        }
        public string LastStation {
            get
            {
                return lastStation;
            }
            set
            {
                lastStation = value;
                NotifyPropertyChanged();
            }
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
