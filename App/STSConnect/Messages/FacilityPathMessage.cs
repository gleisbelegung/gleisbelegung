using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "wege")]
    public class FacilityPathMessage : IIncomingMessage, IOutgoingMessage
    {
        [XmlElement(ElementName = "shape")]
        public List<Shape> Shapes { get; set; }

        [XmlElement(ElementName = "connector")]
        public List<Connection> Connections { get; set; }


        [XmlRoot(ElementName = "shape")]
        public class Shape
        {

            [XmlAttribute(AttributeName = "type")]
            public int Type { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "enr")]
            public int Enr { get; set; }
        }

        [XmlRoot(ElementName = "connector")]
        public class Connection
        {

            [XmlAttribute(AttributeName = "enr1")]
            public int Enr1 { get; set; }

            [XmlAttribute(AttributeName = "enr2")]
            public int Enr2 { get; set; }

            [XmlAttribute(AttributeName = "name1")]
            public string Name1 { get; set; }

            [XmlAttribute(AttributeName = "name2")]
            public string Name2 { get; set; }
        }
    }
}