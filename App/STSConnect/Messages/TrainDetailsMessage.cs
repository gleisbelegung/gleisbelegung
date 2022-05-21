using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "zugdetails")]
    public class TrainDetailsMessage : IIncomingMessage, IOutgoingMessage
    {
        [XmlAttribute(AttributeName = "zid")]
        public int Zid { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "verspaetung")]
        public int Verspaetung { get; set; }

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

        [XmlAttribute(AttributeName = "usertext")]
        public string Usertext { get; set; }

        [XmlAttribute(AttributeName = "usertextsender")]
        public string Usertextsender { get; set; }

        [XmlAttribute(AttributeName = "hinweistext")]
        public string Hinweistext { get; set; }
    }
}