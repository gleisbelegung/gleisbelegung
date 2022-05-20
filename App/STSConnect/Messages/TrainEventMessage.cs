using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "ereignis")]
    public class TrainEventMessage : IIncomingMessage, IOutgoingMessage
    {

        [XmlAttribute(AttributeName = "zid")]
        public int Zid { get; set; }

        [XmlAttribute(AttributeName = "art")]
        public string Art { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "verspaetung")]
        public string Verspaetung { get; set; }

        [XmlAttribute(AttributeName = "gleis")]
        public string Gleis { get; set; }

        [XmlAttribute(AttributeName = "plangleis")]
        public string Plangleis { get; set; }

        [XmlAttribute(AttributeName = "von")]
        public string Von { get; set; }

        [XmlAttribute(AttributeName = "nach")]
        public string Nach { get; set; }

        [XmlAttribute(AttributeName = "sichtbar")]
        public bool Sichtbar { get; set; }

        [XmlAttribute(AttributeName = "amgleis")]
        public bool Amgleis { get; set; }
    }
}