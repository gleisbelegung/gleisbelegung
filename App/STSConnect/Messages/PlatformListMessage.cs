using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "bahnsteigliste")]
    public class PlatformListMessage : IOutgoingMessage, IIncomingMessage
    {
        [XmlElement(ElementName = "bahnsteig")]
        public List<Bahnsteig> Bahnsteige { get; set; }

        [XmlRoot(ElementName = "n")]
        public class N
        {

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "bahnsteig")]
        public class Bahnsteig
        {

            [XmlElement(ElementName = "n")]
            public List<N> N { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "haltepunkt")]
            public bool Haltepunkt { get; set; }
        }
    }
}