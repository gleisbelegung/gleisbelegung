using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "zugfahrplan")]
    public class TrainScheduleMessage : IIncomingMessage, IOutgoingMessage
    {
        [XmlElement(ElementName = "gleis")]
        public List<Gleis> Gleise { get; set; }

        [XmlAttribute(AttributeName = "zid")]
        public int Zid { get; set; }

        [XmlRoot(ElementName = "gleis")]
        public class Gleis
        {
            [XmlAttribute(AttributeName = "plan")]
            public string Plan { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "an")]
            public string An { get; set; }

            [XmlAttribute(AttributeName = "ab")]
            public string Ab { get; set; }

            [XmlAttribute(AttributeName = "flags")]
            public string Flags { get; set; }
        }
    }
}