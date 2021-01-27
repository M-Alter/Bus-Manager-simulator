using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DL
{
    class DalXml : IDL
    {
        #region singelton
        
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() {        } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Files
        
        string LinesFilePath = @"xml\Lines.xml";
        string BusFilePath = @"xml\Buses.xml";
        string StationsFilePath = @"xml\Stations.xml";
        string TripFilePath = @"xml\Trips.xml";
        string LineTripFilePath = @"xml\LineTrips.xml";
        string LineStationsFilePath = @"xml\LineStations.xml";
        string UserFilePath = @"xml\Users.xml";
        string AdjacentStationsFilePath = @"xml\AdjacentStations.xml";
        string SerialNumberGeneratorPath = @"xml\SerialNumberGenerator.xml";
        



        #endregion

        /// <summary>
        /// add a set of adjacent stations
        /// </summary>
        /// <param name="adjacentStations">set of sttions to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddAdjacentStations(AdjacentStations adjacentStations)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);
            //create a new element to add to the file
            //add the main tag 
            XElement AdjacentStationsElem = new XElement("AdjacentStations",
                //add the elements
                new XElement("Station1", adjacentStations.Station1),
                new XElement("Station2", adjacentStations.Station2),
                new XElement("Distance", adjacentStations.Distance),
                //split the time to three sets
                new XElement("Time",
                    new XElement("Hour", adjacentStations.Time.Hours),
                    new XElement("Min", adjacentStations.Time.Minutes),
                    new XElement("Sec", adjacentStations.Time.Seconds)
                    )
                );
            //add the new element to the root element
            rootElem.Add(AdjacentStationsElem);
            //save the file
            return XmlTools.SaveFile(rootElem, AdjacentStationsFilePath);
        }

        /// <summary>
        /// add a new bus
        /// </summary>
        /// <param name="bus">bus to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddBus(Bus bus)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(BusFilePath);
            //create a new element to add to the file
            //add the main tag 
            XElement busElem = new XElement("Bus",
                //add the elements
                new XElement("LicenseNum", bus.LicenseNum),
                new XElement("FromDate", bus.FromDate),
                new XElement("TotalTrip", bus.TotalTrip),
                new XElement("FuelRemain", bus.FuelRemain),
                new XElement("Status", bus.Status.ToString())
                );
            //add the new element to the root element
            rootElem.Add(busElem);
            //save the file
            return XmlTools.SaveFile(rootElem, BusFilePath);
        }

        /// <summary>
        /// add a new line
        /// </summary>
        /// <param name="line">line to add</param>
        /// <returns><true if added successfully/returns>
        public bool AddLine(Line line)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);
            //create a new element to add to the file
            //add the main tag 
            XElement lineElem = new XElement("Line",
                //add the elements
                new XElement("PersonalId", line.PersonalId),
                new XElement("LineNumber", line.LineNumber),
                new XElement("Area", line.Area.ToString()),
                new XElement("FirstStation", line.FirstStation),
                new XElement("LastStation", line.LastStation),
                new XElement("IsActive", line.IsActive.ToString())
                );
            //add the new element to the root element
            rootElem.Add(lineElem);
            //save the file
            return XmlTools.SaveFile(rootElem, LinesFilePath);
        }

        /// <summary>
        /// add a new station to a line
        /// </summary>
        /// <param name="lineStation">LineStation to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddLineStation(LineStation lineStation)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);
            //create a new element to add to the file
            //add the main tag 
            XElement lineStationElem = new XElement("LineStation",
                //add the elements
                new XElement("LineId", lineStation.LineId),
                new XElement("StationCode", lineStation.StationCode),
                new XElement("LineStationIndex", lineStation.LineStationIndex),
                new XElement("PrevStation", lineStation.PrevStation),
                new XElement("NextStation", lineStation.NextStation)
                );
            //add the new element to the root element
            rootElem.Add(lineStationElem);
            //save the file
            return XmlTools.SaveFile(rootElem, LineStationsFilePath);
        }

        /// <summary>
        /// add a station
        /// </summary>
        /// <param name="station">station to add</param>
        /// <returns>true if added successfully</returns>
        public bool AddStation(Station station)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);
            //create a new element to add to the file
            //add the main tag 
            XElement stationElem = new XElement("Station",
                //add the elements
                new XElement("Code", station.Code),
                new XElement("Name", station.Name),
                new XElement("Longitude", station.Longitude),
                new XElement("Lattitude", station.Lattitude)
                );
            //add the new element to the root element
            rootElem.Add(stationElem);
            //save the file
            return XmlTools.SaveFile(rootElem, StationsFilePath);
        }

        public void RemoveBus(int licenseNum)
        {
            //get the file 
            XElement rootElem = XmlTools.LoadFile(BusFilePath);
            var temp = from item in rootElem.Elements()
                       where int.Parse(item.Element("LicenseNum").Value) == licenseNum
                       select item;
            if (temp != null)
            {
                temp.Remove();
            }
            else
            {
                throw new BusException(licenseNum, " bus wasn't found ");
            }

        }

        public void RemoveStation(int code)
        {
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);
            var temp = from item in rootElem.Elements()
                       where int.Parse(item.Element("Code").Value) == code
                       select item;
            if (temp != null)
            {
                temp.Remove();
            }
        }

        /// <summary>
        /// gets a new serial number that hasn't yet existed in the system
        /// </summary>
        /// <returns></returns>
        public int GenerateLinePersonalId()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(SerialNumberGeneratorPath);
            //get the value
            int serialNum = int.Parse(rootElem.Element("LineNumberGenerator").Value);
            //save the next value to the file
            rootElem.Element("LineNumberGenerator").SetValue(serialNum + 1);
            //save the file
            XmlTools.SaveFile(rootElem, SerialNumberGeneratorPath);
            return serialNum;
        }

        /// <summary>
        /// get details of 2 adjacent stations
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>AdjacentStations</returns>
        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            return (from adj in rootElem.Elements()
                        //find the element
                    where int.Parse(adj.Element("Station1").Value) == first && int.Parse(adj.Element("Station2").Value) == second
                    //create each instance using the CreateAdjInstatnce from the Xmltools class
                    select XmlTools.CreateAdjInstatnce(adj)).FirstOrDefault();
        }

        /// <summary>
        /// get all adjacent stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            return from adj in rootElem.Elements()
                       //create an instance using the CreateAdjInstatnce from the Xmltools class
                   select XmlTools.CreateAdjInstatnce(adj);
        }

        /// <summary>
        /// get all the busses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBuses()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(BusFilePath);

            return from bus in rootElem.Elements()
                       //create each instance using the CreateBusInstatnce from the Xmltools class
                   select XmlTools.CreateBusInstatnce(bus);
        }

        ///
        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            XElement rootElem = XmlTools.LoadFile(BusFilePath);

            return from bus in rootElem.Elements()
                       //create each instance using the CreateBusInstatnce from the Xmltools class
                   where predicate(XmlTools.CreateBusInstatnce(bus))
                   select XmlTools.CreateBusInstatnce(bus);
        }

        /// <summary>
        /// get all the lines
        /// </summary>
        /// <returns>a collection Enumerable of all the lines</returns>
        public IEnumerable<Line> GetAllLines()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return from line in rootElem.Elements()
                       //create each instance using the CreateLineInstatnce from the Xmltools class
                   select XmlTools.CreateLineInstatnce(line);
        }

        /// <summary>
        /// get all the stations
        /// </summary>
        /// <returns>an Enumerable collection of all the stations</returns>
        public IEnumerable<Station> GetAllStations()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);

            return from station in rootElem.Elements()
                       //create each instance using the CreateStationInstatnce from the Xmltools class
                   select XmlTools.CreateStationInstatnce(station);
        }

        public IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get all the users
        /// </summary>
        /// <returns>an Emunerable collection of all the users</returns>
        public IEnumerable<User> GetAllUsers()
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(UserFilePath);

            return from user in rootElem.Elements()
                       //create each instance using the CreateUserInstatnce from the Xmltools class
                   select XmlTools.CreateUserInstatnce(user);
        }

        /// <summary>
        /// get a bus with a specific license number
        /// </summary>
        /// <param name="licenseNum">license number to find</param>
        /// <returns>a Bus with licenseNum instance if found or default(Bus) if not</returns>
        public Bus GetBus(int licenseNum)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(BusFilePath);

            return (from bus in rootElem.Elements()
                    where int.Parse(bus.Element("LicenseNum").Value) == licenseNum
                    //create each instance using the CreateBusInstatnce from the Xmltools class
                    select XmlTools.CreateBusInstatnce(bus)).FirstOrDefault();
        }

        /// <summary>
        /// get a line with personal id
        /// </summary>
        /// <param name="personalId">id of the line</param>
        /// <returns>a Line with personalID instance if found or default(Line) if not</returns>
        public Line GetLine(int personalId)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return (from line in rootElem.Elements()
                    where int.Parse(line.Element("PersonalId").Value) == personalId
                    //create each instance using the CreateLineInstatnce from the Xmltools class
                    select XmlTools.CreateLineInstatnce(line)).FirstOrDefault();
        }

        /// <summary>
        /// get all the station IDs that a line passes through
        /// </summary>
        /// <param name="lineId">personal ID of the line</param>
        /// <returns>an Enumerable collection of all the Station IDs</returns>
        public IEnumerable<int> GetLineStations(int lineId)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return from lineStation in rootElem.Elements()
                       //check if this line passes at the station
                   where int.Parse(lineStation.Element("LineId").Value) == lineId
                   //sort the collevtion according to the index in the trip
                   orderby int.Parse(lineStation.Element("LineStationIndex").Value)
                   //and collect the station ID
                   select int.Parse(lineStation.Element("StationCode").Value);
        }

        /// <summary>
        /// get the next station of a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns>the station ID that line lineId will pass after station stationCode if there is a next station or null if there isn't</returns>
        public int GetNextStation(int lineId, int stationCode)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return (from lineStation in rootElem.Elements()
                        //check also if the station isn't the last station
                    where int.Parse(lineStation.Element("LineId").Value) == lineId && int.Parse(lineStation.Element("StationCode").Value) == stationCode && int.Parse(lineStation.Element("NextStation").Value) != 0
                    select int.Parse(lineStation.Element("NextStation").Value)).FirstOrDefault();
        }

        /// <summary>
        /// get a station with station ID Code
        /// </summary>
        /// <param name="code">Code (ID) of the Station</param>
        /// <returns>a station which Code is code if found or default(Station) if not</returns>
        public Station GetStation(int code)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);

            return (from station in rootElem.Elements()
                    where int.Parse(station.Element("Code").Value) == code
                    //create each instance using the CreateStationInstatnce from the Xmltools class
                    select XmlTools.CreateStationInstatnce(station)).FirstOrDefault();
        }

        /// <summary>
        /// get all the lines that pass in this station
        /// </summary>
        /// <param name="code">code of the station</param>
        /// <returns>an Enumerable collection of all the line IDs that pass at this station</returns>
        public IEnumerable<int> GetStationLines(int code)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return from lineStation in rootElem.Elements()
                   where int.Parse(lineStation.Element("StationCode").Value) == code
                   //sort the lines from lowest to highest
                   orderby int.Parse(lineStation.Element("LineId").Value)
                   select int.Parse(lineStation.Element("LineId").Value);
        }

        /// <summary>
        /// check if a exists
        /// </summary>
        /// <param name="lineId">line to check</param>
        /// <returns>true if the line exists</returns>
        public bool LineExists(int lineId)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return (from line in rootElem.Elements()
                    where int.Parse(line.Element("PersonalId").Value) == lineId
                    //if found select the first true
                    select true).FirstOrDefault();
        }

        public bool RemoveAllLineStation(int lineID)
        {
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            var temp = from line in rootElem.Elements()
                       where int.Parse(line.Element("LineId").Value) == lineID
                       select line;
            if (temp != null)
            {
                temp.Remove();
                return true;
            }
            return false;
        }

        public bool RemoveLine(int lineId, int lastStation)
        {
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            var temp = from line in rootElem.Elements()
                       where int.Parse(line.Element("PersonalId").Value) == lineId
                       select line;
            if (temp != null)
            {
                temp.Remove();
                return true;
            }
            return false;
        }

        /// <summary>
        /// update Time or Distance between 2 adjacent Stations
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns>true if updated successfully</returns>
        public bool UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            //find the instance with these 2 stations
            var findAdj = (from adj in rootElem.Elements()
                           where int.Parse(adj.Element("Station1").Value) == adjacentStations.Station1 && int.Parse(adj.Element("Station2").Value) == adjacentStations.Station2
                           select adj).FirstOrDefault();
            //update the Distance value
            findAdj.Element("Distance").SetValue(adjacentStations.Distance);
            //update the Hours Min Sec values respectively
            findAdj.Element("Time").SetElementValue("Hour", adjacentStations.Time.Hours);
            findAdj.Element("Time").SetElementValue("Min", adjacentStations.Time.Minutes);
            findAdj.Element("Time").SetElementValue("Sec", adjacentStations.Time.Seconds);
            //save the file
            XmlTools.SaveFile(rootElem, AdjacentStationsFilePath);
            return true;
        }

        public void UpdateBus()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// validates a non-case-sensative username to a case-sensative password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>true if found a match</returns>
        public bool ValidatePassword(string userName, string password)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(UserFilePath);

            return (from user in rootElem.Elements()
                        //try matching the non-case-sensative username to the case-sensative password
                    where user.Element("UserName").Value.ToLower() == userName.ToLower() && user.Element("Password").Value == password
                    //select the first true
                    select true).FirstOrDefault();
        }

        public IEnumerable<LineTrip> GetLineSchedules()
        {
            XElement rootElem = XmlTools.LoadFile(LineTripFilePath);

            return from ls in rootElem.Elements()
                   select XmlTools.CreateLineTripInstance(ls);
        }

        public bool UpdateLine(int lineId, int firstStation, int lastStation)
        {
            //load the file
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);


            //find the instance with the personal id
            var findLine = (from line in rootElem.Elements()
                       where int.Parse(line.Element("PersonalId").Value) == lineId
                       select line).FirstOrDefault();
            //update the first and last stations value
            findLine.Element("FirstStation").SetValue(firstStation);
            findLine.Element("LastStation").SetValue(lastStation);          
            //save the file
            XmlTools.SaveFile(rootElem, LinesFilePath);
            return true;
        }

        public IEnumerable<TimeSpan> GetLineSchedules(int lineId)
        {
            XElement rootElem = XmlTools.LoadFile(LineTripFilePath);

            return from ls in rootElem.Elements()
                   where int.Parse(ls.Element("LineId").Value) == lineId
                   select new TimeSpan(int.Parse(ls.Element("StartAt").Element("Hour").Value), int.Parse(ls.Element("StartAt").Element("Min").Value), int.Parse(ls.Element("StartAt").Element("Sec").Value));
        }

        public bool AddLineTrip(int lineId, TimeSpan startTime)
        {
            XElement rootElem = XmlTools.LoadFile(LineTripFilePath);

            XElement ltElem = new XElement("LineTrip",
                new XElement("LineId", lineId),
                new XElement("StartAt",
                    new XElement("Hour", startTime.Hours),
                    new XElement("Min", startTime.Minutes),
                    new XElement("Sec", startTime.Seconds)
                    ),
                new XElement("Id", $"{lineId}{startTime.Hours}{startTime.Minutes}{startTime.Seconds}")
                );

            rootElem.Add(ltElem);
            return XmlTools.SaveFile(rootElem, LineTripFilePath);
        }

        public bool RemoveLineTrip(int lineId, TimeSpan time)
        {
            XElement rootElem = XmlTools.LoadFile(LineTripFilePath);
            string id = $"{lineId}{time.Hours}{time.Minutes}{time.Seconds}";
            var temp = from lineTrip in rootElem.Elements()
                       where int.Parse(lineTrip.Element("Id").Value) == int.Parse(id)
                       select lineTrip;
            if (temp != null)
            {
                temp.Remove();
                return true;
            }
            return false;
        }
    }
}
