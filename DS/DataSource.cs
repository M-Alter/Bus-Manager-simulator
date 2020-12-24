﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;

namespace DS
{
    public class DataSource
    {
        public static List<Bus> BusList;
        public static List<Station> StationList;

        /// <summary>
        /// C'tor that runs before an instance is created
        /// </summary>
        static DataSource()
        {
            InitAllLists();
        }

        private static void InitAllLists()
        {
            BusList = new List<Bus>();
            StationList = new List<Station>();
        }
    }
}
