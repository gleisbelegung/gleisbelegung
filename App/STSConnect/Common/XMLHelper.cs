using System;
using System.CodeDom;
using System.IO;
using System.Xml.Serialization;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.Common
{
    public static class XMLHelper
    {
        public static IIncomingMessage Deserialize(Type type, string message)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (StringReader reader = new StringReader(message))
            {
                return (IIncomingMessage)serializer.Deserialize(reader);
            }
        }

        public static string SerializeAsXml(IOutgoingMessage outgoingMessage)
        {
            XmlSerializer serializer = new XmlSerializer(outgoingMessage.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, outgoingMessage);
                return writer.ToString();
            }
        }
    }
}