using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "anlageninfo")]
    public class FacilityInfoMessage : IOutgoingMessage, IIncomingMessage
    {
        [XmlAttribute(AttributeName = "simbuild")]
        public int Simbuild { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "online")]
        public bool Online { get; set; }

        [XmlAttribute(AttributeName = "aid")]
        public int Aid { get; set; }
    }
}