using BLAPI;
using BO;
using DalApi;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    internal class BLImp : IBL
    {
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        IDL dl = DLFactory.GetDL();
        static Random r = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// get all the buses
        /// </summary>
        /// <returns>a collection of all the buses grouped by the years</returns>
        public IEnumerable<IGrouping<int, Bus>> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                       //call the get bus functin with LisenceNum parameter
                   let bus = GetBus(item.LicenseNum)
                   group bus by bus.FromDate.Year into fromYear
                   orderby fromYear.Key
                   select fromYear;
        }

        /// <summary>
        /// gets the requested bus
        /// </summary>
        /// <param name="license">license num of the bus</param>
        /// <returns>the bus with license "license"</returns>
        public Bus GetBus(int license)
        {
            Bus bus = new Bus();
            var tempBus = dl.GetBus(license);
            tempBus.CopyPropertiesTo(bus);
            return bus;
        }

        /// <summary>
        /// get all the buses that conform to the predicate function
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            return from item in dl.GetAllBuses()
                       //call the get bus functin with LisenceNum parameter
                   let bus = GetBus(item.LicenseNum)
                   where predicate(bus)
                   select bus;
        }

        /// <summary>
        /// gets all the lines available
        /// </summary>
        /// <returns>a collection of all the lines</returns>
        public IEnumerable<Line> GetAllLines()
        {
            return from item in dl.GetAllLines()
                       //call the GetLine function with the lines personal id
                   let line = GetLine(item.PersonalId)
                   select line;
        }

        /// <summary>
        /// get all the available stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                       //call the GetStation with the station code
                   let station = GetStation(item.Code)
                   select station;
        }

        /// <summary>
        /// gets a single station
        /// </summary>
        /// <param name="code">code number of the station</param>
        /// <returns>return the station that has the requested code</returns>
        public Station GetStation(int code)
        {
            Station station = new Station();
            //create a temporary statoin
            var tempStation = dl.GetStation(code);
            //copy all properties over
            tempStation.CopyPropertiesTo(station);
            //get all the lines that pass through this station
            station.LinesAtStation = from lines in dl.GetStationLines(code)
                                         //get the line number (previously lines was the lines personal id)
                                     let lineNumber = dl.GetLine(lines).LineNumber
                                     //get the lines last station name
                                     let lastStation = dl.GetStation(dl.GetLine(lines).LastStation).Name
                                     //sort by the line numbers
                                     orderby lineNumber
                                     //add a new StationLine instant with the requested info
                                     select new StationLine { LineNumber = lineNumber, LastStation = lastStation };
            return station;
        }

        /// <summary>
        /// get a single line
        /// </summary>
        /// <param name="id">lines personal id</param>
        /// <returns>a line with the personal id == id</returns>
        public Line GetLine(int id)
        {
            Line line = new Line();
            //create a temporary line
            var tempLine = dl.GetLine(id);
            //copy all properties over
            tempLine.CopyPropertiesTo(line);
            //get the first stations name
            line.FirstStationName = dl.GetStation(line.FirstStation).Name;
            //get the last stations name
            line.LastStationName = dl.GetStation(line.LastStation).Name;
            //get all the station id's that this line passes through
            var stationIDs = from numbers in dl.GetLineStations(line.PersonalId)
                             select numbers;
            int index = 1;
            //get the station of each station id
            line.Stations = from numbers in stationIDs
                                //get the stations name
                            let name = dl.GetStation(numbers).Name
                            //get the next stations station id
                            let next = dl.GetNextStation(id, numbers)
                            //get the time and distance between the 2 stations
                            let doAdjacentStations = dl.GetAdjacentStations(numbers, next)
                            //sort the station with the indexes
                            orderby index
                            //if the statoin is the last station then give it a value of zero
                            select new LineStation() { Station = numbers, StationName = name, Index = index++, TimeToNext = (doAdjacentStations == default(DO.AdjacentStations) ? new TimeSpan(0) : doAdjacentStations.Time), Distance = (doAdjacentStations == default(DO.AdjacentStations) ? 0.0 : doAdjacentStations.Distance) };
            // get all times of the line
            line.Timing = dl.GetLineSchedules(line.PersonalId);
            return line;
        }

        /// <summary>
        /// add a new bus 
        /// </summary>
        /// <param name="bus">bus to add</param>
        /// <returns>true if the bus was added successfully</returns>
        public bool AddBus(Bus bus)
        {
            const int MIN_SEVEN = 1000000;
            const int MAX_SEVEN = 9999999;
            const int MIN_EIGHT = 10000000;
            const int MAX_EIGHT = 99999999;
            var busBO = dl.GetBus(bus.LicenseNum);
            if (busBO != default(DO.Bus))
            {
                throw new BusException(bus.LicenseNum, "This bus number already exist");
            }
            if ((bus.FromDate.Year >= 2018 && bus.LicenseNum < MIN_EIGHT) ||
                (bus.FromDate.Year >= 2018 && bus.LicenseNum > MAX_EIGHT) ||
                (bus.FromDate.Year < 2018 && bus.LicenseNum > MAX_SEVEN) ||
                (bus.FromDate.Year < 2018 && bus.LicenseNum < MIN_SEVEN))
                throw new BusException(bus.LicenseNum, "License num length doesn't match begin date!");
            if (bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new BusException(bus.LicenseNum, "Gas should be between 0 to 1200!");
            if (bus.TotalTrip < 0)
                throw new BusException(bus.LicenseNum, "Milege can't be negative");
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            return dl.AddBus(busDO);
        }

        /// <summary>
        /// delete a bus
        /// </summary>
        /// <param name="licenseNum">lisence num of bus to remove</param>
        public void DeleteBus(int licenseNum)
        {
            dl.RemoveBus(licenseNum);
        }

        /// <summary>
        /// adds a new station
        /// </summary>
        /// <param name="station"></param>
        /// <returns>true if added successfully</returns>
        public bool AddStation(Station station)
        {
            double longitude = r.NextDouble() + r.Next(29, 34);
            double lattitude = r.NextDouble() + r.Next(31, 36);
            var stationBO = dl.GetStation(station.Code);
            station.Longitude = longitude;
            station.Lattitude = lattitude;
            //check if staiton exists already
            if (stationBO != default(DO.Station))
            {
                throw new StationException(station.Code, "This station already exist");
            }
            if (station.Code < 10000 || station.Code > 99999)
            {
                throw new StationException(station.Code, "Station number must be 5 digits");
            }
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            return dl.AddStation(stationDO);
        }

        //===============================================================================================
        /// <summary>
        /// adds a new line 
        /// </summary>
        /// <param name="line">line to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddLine(Line line)
        {
            //need to add method to get all line numbers

            //check if line exists already
            //if (dl.LineExists(line.PersonalId))
            //    throw new LineException(line.PersonalId, "This line already exist");
            if (line.LineNumber > 999 || line.LineNumber < 1)
                throw new LineException(line.PersonalId, "Line number should be between 1 - 999");
            DO.Line lineDO = new DO.Line();
            //copy all the properties over
            line.CopyPropertiesTo(lineDO);
            //------------------------------------------------------------------------------------------------------------
            lineDO.PersonalId = dl.GenerateLinePersonalId();  //needs to be updated to come from the dal layer
            //------------------------------------------------------------------------------------------------------------
            int index = 0;
            int[] stationArray = new int[line.Stations.Count()];
            //add all the LineStations to the array
            foreach (var item in line.Stations)
                stationArray[index++] = item.Station;

            // add a list to all the adjacent staions
            List<AdjacentStations> adjacentStations = new List<AdjacentStations>();
            for (int i = 0; i < stationArray.Length - 1; i++)
            {
                if (dl.GetAdjacentStations(stationArray[i], stationArray[i + 1]) == default(DO.AdjacentStations))
                {
                    adjacentStations.Add(new AdjacentStations { Station1 = stationArray[i], Station1Name = dl.GetStation(stationArray[i]).Name, Station2 = stationArray[i + 1], Station2Name = dl.GetStation(stationArray[i + 1]).Name, /*Distance = r.NextDouble() * (100) + 1, Time = new TimeSpan(r.Next(0, 23), r.Next(0, 59), r.Next(0, 59))*/ });
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = stationArray[i], Station2 = stationArray[i + 1], Distance = r.NextDouble() * (10) + 1, Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59)) });
                }
            }
            if (adjacentStations.Count > 0)
            {
                AdjacentStations[] AdjacentStationsArray = new AdjacentStations[adjacentStations.Count];
                for (int i = 0; i < adjacentStations.Count; i++)
                {
                    AdjacentStationsArray[i] = GetAdjacentStations(adjacentStations[i].Station1, adjacentStations[i].Station2);
                }
                throw new AdjacentStationsExceptions("these adjacent stations are missing some info", AdjacentStationsArray);
            }

            //add the LineStations
            for (int i = 0; i < stationArray.Length; i++)
            {
                //if this is the first station in the list than previos station = 0 
                if (i == 0)
                    dl.AddLineStation(new DO.LineStation { LineId = lineDO.PersonalId, LineStationIndex = 1, StationCode = stationArray[0], NextStation = stationArray[1], PrevStation = 0 });
                //if this is the last station in the list then the NextStation = 0 
                else if (i == stationArray.Length - 1)
                    dl.AddLineStation(new DO.LineStation { LineId = lineDO.PersonalId, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = 0, PrevStation = stationArray[i - 1] });
                else
                    dl.AddLineStation(new DO.LineStation { LineId = lineDO.PersonalId, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = stationArray[i + 1], PrevStation = stationArray[i - 1] });
            }


            return dl.AddLine(lineDO);
        }
        //===============================================================================================

        /// <summary>
        /// gets user names that are or aren't admins 
        /// </summary>
        /// <param name="admin">true if you want admin usernames of false for non admin usernames</param>
        /// <returns>a collection of usernames</returns>
        public IEnumerable<string> GetAllUserNames(bool admin)
        {
            return from item in dl.GetAllUsers()
                       //where the previleges match the requsted
                   where item.Admin == admin
                   //sort the usernames alphabetically
                   orderby item.UserName
                   select item.UserName;
        }

        /// <summary>
        /// check if the username and password match 
        /// </summary>
        /// <param name="userName">username to check</param>
        /// <param name="password">password to match</param>
        /// <returns>true if match was succsessfull</returns>
        public bool ValidatePassword(string userName, string password)
        {
            return dl.ValidatePassword(userName, password);
        }

        /// <summary>
        /// sends an email to emailAddress with a reminder of the password of the username account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress">email address to send the password to</param>
        public void ResendPassword(string userName, string emailAddress)
        {
            //create a new message
            var message = new MimeMessage();
            //add the source of the message
            message.From.Add(new MailboxAddress("Bus Manager", "busmanager.2131.1146@gmail.com"));
            //add the destination of the messeg
            message.To.Add(new MailboxAddress(userName, emailAddress));
            //add a subject to the message
            message.Subject = "Password reminder";
            //add a body to the message(plain text (not HTML))
            message.Body = new TextPart("plain")
            {
                Text = string.Format(@"Hey {0},
***this is an automated email, please do not reply to this email***


The password for your account is 
===============================
                {1}           
===============================
-- Bus Manager", userName, dl.GetAllUsers().Where(u => u.UserName.ToLower() == userName.ToLower()).Select(u => u.Password).FirstOrDefault())
            };

            //use resources only as long as needed and dipose right away
            using (var client = new SmtpClient())
            {
                //connect to email server
                client.Connect("smtp.gmail.com", 465, true);

                //authenticate 
                client.Authenticate("busmanager.2131.1146", "21311146");

                try
                {
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to send message", ex);
                }
            }
        }

        /// <summary>
        /// removes a station fron the line 
        /// </summary>
        /// <param name="lineId">line to be removed from</param>
        /// <param name="stationToRemove">station to remove</param>
        /// <returns>true if removed successfully</returns>
        public bool RemoveStationFromLine(int lineId, int stationToRemove)
        {
            Line line = GetLine(lineId);
            if (line.Stations.Count() < 3)
                throw new LineStationException("A line can't have less than 2 stations\nyou might want to Remove the line", line.LineNumber);
            int[] stationArray = new int[line.Stations.Count() - 1];
            int index = 0;

            //add all the stations to the array but the station to be deleted
            foreach (var item in line.Stations)
            {
                if (item.Station != stationToRemove)
                    stationArray[index++] = item.Station;
            };

            line.Stations = null;

            //remove all the lines linestations
            dl.RemoveAllLineStation(lineId);

            //update the first and last station
            dl.UpdateLine(line.PersonalId, stationArray[0], stationArray[stationArray.Length-1]);


            //add all the linestations in the array
            for (int i = 0; i < stationArray.Length; i++)
            {
                if (i == 0)
                    dl.AddLineStation(new DO.LineStation { LineId = line.PersonalId, LineStationIndex = 1, StationCode = stationArray[0], NextStation = stationArray[1], PrevStation = 0 });
                else if (i == stationArray.Length - 1)
                    dl.AddLineStation(new DO.LineStation { LineId = line.PersonalId, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = 0, PrevStation = stationArray[i - 1] });
                else
                    dl.AddLineStation(new DO.LineStation { LineId = line.PersonalId, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = stationArray[i + 1], PrevStation = stationArray[i - 1] });
            }

            //==========================================================================================================
            // add a list to all the adjacent staions
            List<AdjacentStations> adjacentStations = new List<AdjacentStations>();
            for (int i = 0; i < stationArray.Length - 1; i++)
            {
                if (dl.GetAdjacentStations(stationArray[i], stationArray[i + 1]) == default(DO.AdjacentStations))
                {
                    adjacentStations.Add(new AdjacentStations { Station1 = stationArray[i], Station1Name = dl.GetStation(stationArray[i]).Name, Station2 = stationArray[i + 1], Station2Name = dl.GetStation(stationArray[i + 1]).Name, /*Distance = r.NextDouble() * (100) + 1, Time = new TimeSpan(r.Next(0, 23), r.Next(0, 59), r.Next(0, 59))*/ });
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = stationArray[i], Station2 = stationArray[i + 1], Distance = r.NextDouble() * (10) + 1, Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59)) });
                }
            }
            if (adjacentStations.Count > 0)
            {
                AdjacentStations[] AdjacentStationsArray = new AdjacentStations[adjacentStations.Count];
                for (int i = 0; i < adjacentStations.Count; i++)
                {
                    AdjacentStationsArray[i] = GetAdjacentStations(adjacentStations[i].Station1, adjacentStations[i].Station2);
                }
                throw new AdjacentStationsExceptions("these adjacent stations are missing some info", AdjacentStationsArray);
            }
            //==========================================================================================================

            //line = GetLine(line.PersonalId);
            return true;
        }

        /// <summary>
        /// gets all stations that conform the predicate function given
        /// </summary>
        /// <param name="predicate">condition to conform</param>
        /// <returns>a collection of stations that conform to predicate</returns>
        public IEnumerable<Station> GetAllStations(Predicate<Station> predicate)
        {
            return from item in dl.GetAllStations()
                       //get the station as BO
                   let station = GetStation(item.Code)
                   // check if the station conforms predicate
                   where predicate(station)
                   select station;
        }

        /// <summary>
        /// get the details of the 2 adjacent stations
        /// </summary>
        /// <param name="first">first station</param>
        /// <param name="second">second station</param>
        /// <returns>all the details between these two stations</returns>
        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            DO.AdjacentStations doAdjacentStations = dl.GetAdjacentStations(first, second);
            AdjacentStations adjacentStations = new AdjacentStations();
            doAdjacentStations.CopyPropertiesTo(adjacentStations);

            //add the stations names to the instance
            adjacentStations.Station1Name = GetStation(adjacentStations.Station1).Name;
            adjacentStations.Station2Name = GetStation(adjacentStations.Station2).Name;
            return adjacentStations;
        }

        /// <summary>
        /// gets all the adjacent stations
        /// </summary>
        /// <returns>all the adjacent stations</returns>
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from item in dl.GetAllAdjacentStations()
                       //get instance as BO
                   let current = GetAdjacentStations(item.Station1, item.Station2)
                   select current;
        }

        /// <summary>
        /// adds a new adjacent stations
        /// </summary>
        /// <param name="adj">instatnce to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddAdjacentStations(AdjacentStations adj)
        {
            return dl.AddAdjacentStations((DO.AdjacentStations)adj.CopyPropertiesToNew(typeof(DO.AdjacentStations)));
        }

        /// <summary>
        /// remove a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="lastStation"></param>
        /// <returns>true if removed successfully</returns>
        public bool RemoveLine(int lineId)
        {
            dl.RemoveAllLineStation(lineId);
            return dl.RemoveLine(lineId);
        }

        /// <summary>
        /// updates the adjacent stations details
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns>true if updates successfully</returns>
        public bool UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            var temp = dl.GetAdjacentStations(adjacentStations.Station1, adjacentStations.Station2);
            temp.Time = adjacentStations.Time;
            temp.Distance = adjacentStations.Distance;
            return dl.UpdateAdjacentStations(temp);
        }

        public void StartSimulator(TimeSpan startTime, int rate, Action<TimeSpan> updateTime)
        {
            ClockSimulator.Instance.ClockObserver += updateTime;
            ClockSimulator.Instance.Start(startTime, rate);
        }

        public void StopSimulator()
        {
            ClockSimulator.Instance.Stop();
        }

        public void SetStationPanel(int station, Action<LineTiming> updateBus)
        {
            TripSimulator.Instance.StationId = station;
            TripSimulator.Instance.BusObserver += updateBus;
        }

        /// <summary>
        /// add a new start time to a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool AddLineTrip(int lineId, TimeSpan time)
        {
            return dl.AddLineTrip(lineId, time);
        }

        /// <summary>
        /// to remove a line trip from a line
        /// </summary>
        /// <param name="personalId"></param>
        /// <param name="time"></param>
        public bool RemoveLineTrip(int lineId, TimeSpan time)
        {

            return dl.RemoveLineTrip(lineId, time);
        }

        public bool UpdateLine(Line line, int station, int index)
        {
            if (station > 99999 || station < 10000)
            {
                throw new StationException(station, "Station must be 5 digits");
            }
            if (!(line.Stations.FirstOrDefault(st => st.Station == station) == null))
            {
                throw new StationException(station, $"Station {station} existed already");
            }
            var lookup = GetAllStations().ToDictionary(itemKeySelector => itemKeySelector.Code);
            try
            {
                var result = lookup[station];
            }
            catch (Exception ex)
            {

                throw new StationException(station, $"Station {station} doesn't exist");
            }
            
            return true;
        }
    }
}
