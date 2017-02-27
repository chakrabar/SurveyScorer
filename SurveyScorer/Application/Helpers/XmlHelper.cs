using System.IO;
using System.Xml.Serialization;

namespace SurveyScorer.Application.Helpers
{
    public class XmlHelper
    {
        public static T DeserializeData<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(data);
            T result = (T)serializer.Deserialize(reader);
            return result;
        }

        public static string SerializeData<T>(T data, bool isMinified = true)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, data);
            var xmlString = sw.ToString();
            return xmlString;
        }
    }
}