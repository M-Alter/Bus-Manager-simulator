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
            catch (Exception)
            {

                throw;
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
    }
}
