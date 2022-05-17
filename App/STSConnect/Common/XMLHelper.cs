using System;
using System.CodeDom;
using System.IO;
using System.Xml;
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

        public static string Serialize(IOutgoingMessage outgoingMessage)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            var serializer = new XmlSerializer(outgoingMessage.GetType());

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, outgoingMessage, emptyNamespaces);
                return stream.ToString();
            }
        }
    }
}