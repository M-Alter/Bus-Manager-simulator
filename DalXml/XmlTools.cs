using System;
using System.IO;
using System.Xml.Linq;

namespace DO
{
    public class XmlTools
    {

        public static XElement LoadFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return XElement.Load(path);
                }
                else
                {
                    XElement rootElement = new XElement(path);
                    rootElement.Save(path);
                    return rootElement;
                }
            }
            catch (FileLoadException e)
            {
                throw new FileLoadException(e.Message, path);
            }
            catch (Exception e)
            {
                throw new FileLoadException(e.Message, path);
            }
        }




        internal static bool SaveFile(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
            return false;
        }


        internal static Bus CreateBusInstatnce(XElement bus)
        {
            return new Bus()
            {
                LicenseNum = int.Parse(bus.Element("LicenseNum").Value),
                FromDate = DateTime.Parse(bus.Element("FromDate").Value),
                FuelRemain = double.Parse(bus.Element("FuelRemain").Value),
                TotalTrip = int.Parse(bus.Element("TotalTrip").Value),
                Status = (Enums.BusStatus)Enum.Parse(typeof(Enums.BusStatus), bus.Element("Status").Value)
            };
        }

        internal static Line CreateLineInstatnce(XElement line)
        {
            return new Line()
            {
                PersonalId = int.Parse(line.Element("PersonalId").Value),
                LineNumber = int.Parse(line.Element("LineNumber").Value),
                FirstStation = int.Parse(line.Element("FirstStation").Value),
                LastStation = int.Parse(line.Element("LastStation").Value),
                Area = (Enums.Areas)Enum.Parse(typeof(Enums.Areas), line.Element("Area").Value),
                IsActive = bool.Parse(line.Element("IsActive").Value)
            };
        }

        internal static LineStation CreateLineStationInstatnce(XElement lineStation)
        {
            return new LineStation()
            {
                LineId = int.Parse(lineStation.Element("LineId").Value),
                StationCode = int.Parse(lineStation.Element("StationCode").Value),
                LineStationIndex = int.Parse(lineStation.Element("LineStationIndex").Value),
                PrevStation = int.Parse(lineStation.Element("PrevStation").Value),
                NextStation = int.Parse(lineStation.Element("NextStation").Value)
            };
        }

        internal static Station CreateStationInstatnce(XElement station)
        {
            return new Station()
            {
                Code = int.Parse(station.Element("Code").Value),
                Name = station.Element("Name").Value,
                Lattitude = double.Parse(station.Element("Lattitude").Value),
                Longitude = double.Parse(station.Element("Longitude").Value)
            };
        }

        internal static User CreateUserInstatnce(XElement user)
        {
            return new User()
            {
                UserName = user.Element("UserName").Value,
                Password = user.Element("Password").Value,
                Admin = bool.Parse(user.Element("Admin").Value)
            };
        }

        internal static AdjacentStations CreateAdjInstatnce(XElement adj)
        {
            return new AdjacentStations()
            {
                Station1 = int.Parse(adj.Element("Station1").Value),
                Station2 = int.Parse(adj.Element("Station2").Value),
                Distance = double.Parse(adj.Element("Distance").Value),
                Time = new TimeSpan(int.Parse(adj.Element("Time").Element("Hour").Value), int.Parse(adj.Element("Time").Element("Min").Value), int.Parse(adj.Element("Time").Element("Sec").Value))
            };
        }
    }
}
