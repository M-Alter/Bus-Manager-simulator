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


        #endregion
        public bool AddAdjacentStations(AdjacentStations adjacentStations)
        {
            throw new NotImplementedException();
        }

        public bool AddBus(Bus bus)
        {
            XElement rootElem = XmlTools.LoadFile(BusFilePath);
            XElement busElem = new XElement("Bus",
                new XElement("LicenseNum", bus.LicenseNum),     //check if needs to be parsed to string
                new XElement("FromDate", bus.FromDate),
                new XElement("TotalTrip", bus.TotalTrip),
                new XElement("FuelRemain", bus.FuelRemain),
                new XElement("Status", bus.Status.ToString())
                );
            rootElem.Add(busElem);
            return XmlTools.SaveFile(rootElem, BusFilePath);
        }

        public bool AddLine(Line line)
        {
            XElement rootElem = XmlTools.LoadFile(LinesFilePath);
            XElement lineElem = new XElement("Line",
                new XElement("PersonalId", line.PersonalId),     //check if needs to be parsed to string
                new XElement("LineNumber", line.LineNumber),     //check if needs to be parsed to string
                new XElement("Area", line.Area.ToString()),
                new XElement("FirstStation", line.FirstStation),
                new XElement("LastStation", line.LastStation),
                new XElement("IsActive", line.IsActive.ToString())
                );
            rootElem.Add(lineElem);
            return XmlTools.SaveFile(rootElem, LinesFilePath);
        }

        public bool AddLineStation(LineStation lineStation)
        {
            XElement rootElem = XmlTools.LoadFile(LineStationsFilePath);
            XElement lineStationElem = new XElement("LineStation",
                new XElement("LineId", lineStation.LineId),     //check if needs to be parsed to string
                new XElement("StationCode", lineStation.StationCode),
                new XElement("LineStationIndex", lineStation.LineStationIndex),
                new XElement("PrevStation", lineStation.PrevStation),
                new XElement("NextStation", lineStation.NextStation)
                );
            rootElem.Add(lineStationElem);
            return XmlTools.SaveFile(rootElem, LineStationsFilePath);
        }

        public bool AddStation(Station station)
        {
            XElement rootElem = XmlTools.LoadFile(StationsFilePath);
            XElement stationElem = new XElement("Station",
                new XElement("Code", station.Code),     //check if needs to be parsed to string
                new XElement("Name", station.Name),
                new XElement("Longitude", station.Longitude),
                new XElement("Lattitude", station.Lattitude)
                );
            rootElem.Add(stationElem);
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

        public int GenerateLinePersonalId()
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool LineExists(int lineId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
