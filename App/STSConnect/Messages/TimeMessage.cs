using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "simzeit")]
    public class TimeMessage : IIncomingMessage, IOutgoingMessage
    {

        [XmlAttribute(AttributeName = "sender")]
        public int Sender { get; set; }

        [XmlAttribute(AttributeName = "zeit")]
        public int Zeit { get; set; }
    }
}