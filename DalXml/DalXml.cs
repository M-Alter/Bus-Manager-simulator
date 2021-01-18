using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dal
{
    class DalXml : IDL
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Files
        string LinesFilePath = @"xml\Lines.xml";
        string BusFilePath = @"xml\Buses.xml";
        string StationsFilePath = @"xml\Stations.xml";
        string TripFilePath = @"xml\Trips.xml";
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
            throw new NotImplementedException();
        }

        public void RemoveStation(int code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets a new serial number that hasn't yet existed in the system
        /// </summary>
        /// <returns></returns>
        public int GenerateLinePersonalId()
        {
            XElement rootElem = XmlTools.LoadFile(SerialNumberGeneratorPath);
            int serialNum = int.Parse(rootElem.Element("LineNumberGenerator").Value);
            rootElem.Element("LineNumberGenerator").SetValue(serialNum + 1);
            return serialNum;
        }

        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            return (from adj in rootElem.Elements()
                    where int.Parse(adj.Element("Station1").Value) == first && int.Parse(adj.Element("Station2").Value) == second
                    select XmlTools.CreateAdjInstatnce(adj)).FirstOrDefault();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            return from adj in rootElem.Elements()
                   select XmlTools.CreateAdjInstatnce(adj);
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            XElement rootElem = XmlTools.LoadFile(BusFilePath);

            return from bus in rootElem.Elements()
                   select XmlTools.CreateBusInstatnce(bus);
        }

        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return from line in rootElem.Elements()
                   select XmlTools.CreateLineInstatnce(line);
        }

        public IEnumerable<Station> GetAllStations()
        {
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);

            return from station in rootElem.Elements()
                   select XmlTools.CreateStationInstatnce(station);
        }

        public IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            XElement rootElem = XmlTools.LoadFile(UserFilePath);

            return from user in rootElem.Elements()
                   select XmlTools.CreateUserInstatnce(user);
        }

        public Bus GetBus(int licenseNum)
        {
            XElement rootElem = XmlTools.LoadFile(BusFilePath);

            return (from bus in rootElem.Elements()
                    where int.Parse(bus.Element("LicenseNum").Value) == licenseNum
                    select XmlTools.CreateBusInstatnce(bus)).FirstOrDefault();
        }

        public Line GetLine(int id)
        {
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return (from line in rootElem.Elements()
                    where int.Parse(line.Element("PersonalId").Value) == id
                    select XmlTools.CreateLineInstatnce(line)).FirstOrDefault();
        }

        public IEnumerable<int> GetLineStations(int lineId)
        {
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return from lineStation in rootElem.Elements()
                   where int.Parse(lineStation.Element("LineId").Value) == lineId
                   orderby int.Parse(lineStation.Element("LineStationIndex").Value)
                   select int.Parse(lineStation.Element("StationCode").Value);
        }

        public int GetNextStation(int lineId, int stationCode)
        {
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return (from lineStation in rootElem.Elements()
                    where int.Parse(lineStation.Element("LineId").Value) == lineId && int.Parse(lineStation.Element("StationCode").Value) == stationCode
                    select int.Parse(lineStation.Element("NextStation").Value)).FirstOrDefault();
        }

        public Station GetStation(int code)
        {
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);

            return (from station in rootElem.Elements()
                    where int.Parse(station.Element("Code").Value) == code
                    select XmlTools.CreateStationInstatnce(station)).FirstOrDefault();
        }

        public IEnumerable<int> GetStationLines(int code)
        {
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);

            return from lineStation in rootElem.Elements()
                   where int.Parse(lineStation.Element("StationCode").Value) == code
                   orderby int.Parse(lineStation.Element("LineId").Value)
                   select int.Parse(lineStation.Element("LineId").Value);
        }

        public bool LineExists(int lineId)
        {
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);

            return (from line in rootElem.Elements()
                    where int.Parse(line.Element("PersonalId").Value) == lineId
                    select true).FirstOrDefault();
        }

        public bool RemoveAllLineStation(int lineID)
        {
            throw new NotImplementedException();
        }

        public bool RemoveLine(int lineId, int lastStation)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement rootElem = XmlTools.LoadFile(AdjacentStationsFilePath);

            var findAdj = (from adj in rootElem.Elements()
            where int.Parse(adj.Element("Station1").Value) == adjacentStations.Station1 && int.Parse(adj.Element("Station2").Value) == adjacentStations.Station2
            select adj).FirstOrDefault();

            findAdj.Element("Distance").SetValue(adjacentStations.Distance);
            findAdj.Element("Time").SetElementValue("Hour", adjacentStations.Time.Hours);
            findAdj.Element("Time").SetElementValue("Min", adjacentStations.Time.Minutes);
            findAdj.Element("Time").SetElementValue("Sec", adjacentStations.Time.Seconds);
            return true;
        }

        public void UpdateBus()
        {
            throw new NotImplementedException();
        }

        public bool ValidatePassword(string userName, string password)
        {

            XElement rootElem = XmlTools.LoadFile(UserFilePath);

            return (from user in rootElem.Elements()
                    where user.Element("UserName").Value.ToLower() == userName.ToLower() && user.Element("Password").Value == password
                    select true).FirstOrDefault();
        }
    }
}
