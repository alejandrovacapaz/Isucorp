using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IsucorpTest.Core
{
    public static class Serialize
    {       
        public static T DeserializeObject <T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }                        
        }

        public static string SerializeObject <T>(this T toSerialize) 
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }      
    }
}
