using Game.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Game.Util
{
    public static class XmlConvert
    {
        public static string Serialize(Level level)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Level));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, level);
                    xml = sww.ToString();
                }
            }

            return xml;
        }

        public static Object Deserialize(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                if (xmlReader != null)
                    xmlReader.Close();
                if (strReader != null)
                    strReader.Close();
            }

            return obj;
        }
    }
}
