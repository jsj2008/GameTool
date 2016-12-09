using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PublicUtilities
{
    public class XmlBeanTools
    {
        public static string GetBeanXml(object obj)
        {
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            MemoryStream writer = new MemoryStream();
            try
            {
                ser.Serialize(writer, obj);
                return Encoding.UTF8.GetString(writer.GetBuffer());
            }
            finally
            {
                writer.Close();
            }
        }

        private static string GetAbsPath(string xmlFile)
        {
            return string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, xmlFile);
        }

        public static bool SaveBeanToXmlFile(object obj, string xmlfile)
        {
            string xmlPath = GetAbsPath(xmlfile);
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(xmlPath);
            try
            {
                ser.Serialize(writer, obj);
                return true;
            }
            finally
            {
                writer.Close();
            }
        }

        private static bool IsFileExist(string xmlFile)
        {
            if (File.Exists(xmlFile))
            {
                return true;
            }

            return false;
        }

        public static object GetBeanFromXmlFile(Type objtype, string xmlfile)
        {
            string xmlPath = GetAbsPath(xmlfile);
            if (!IsFileExist(xmlPath)) return null;

            XmlSerializer ser = new XmlSerializer(objtype);
            TextReader reader = new StreamReader(xmlPath);
            try
            {
                return ser.Deserialize(reader);
            }
            catch
            {
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public static object GetBeanFromXmlText(Type objtype, string xmltext)
        {
            XmlSerializer ser = new XmlSerializer(objtype);
            StringReader reader = new StringReader(xmltext);
            try
            {
                return ser.Deserialize(reader);
            }
            catch
            {
                return null;
            }
            finally
            {
                reader.Close();
            }
        }
    }

    public class StreamTools
    {
        public static bool DumpStreamToFile(Stream stream, string filepath)
        {
            System.IO.FileStream writer = new System.IO.FileStream(filepath, System.IO.FileMode.Create);
            try
            {
                int buflen = 4096;
                byte[] buff = new byte[buflen];
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                int readed = buflen;
                while (readed > 0)
                {
                    readed = stream.Read(buff, 0, buflen);
                    writer.Write(buff, 0, readed);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                writer.Close();
            }
        }
    }
}