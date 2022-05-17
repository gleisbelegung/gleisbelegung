using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "register")]
    public class RegisterMessage : IOutgoingMessage
    {

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "autor")]
        public string Autor { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [XmlAttribute(AttributeName = "protokoll")]
        public int Protokoll { get; set; }

        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
    }
}