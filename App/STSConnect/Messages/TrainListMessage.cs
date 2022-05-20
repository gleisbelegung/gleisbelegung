using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "zugliste")]
    public class TrainListMessage : IIncomingMessage, IOutgoingMessage
    {
        [XmlElement(ElementName = "zug")]
        public List<Train> Trains { get; set; }

        [XmlRoot(ElementName = "zug")]
        public class Train
        {
            [XmlAttribute(AttributeName = "zid")]
            public int Zid { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

    }
}