using System.Xml.Serialization;

namespace Gleisbelegung.App.STSConnect.Messages
{
    [XmlRoot(ElementName = "status")]
    public class StatusMessage : IIncomingMessage, IOutgoingMessage
    {
        [XmlAttribute(AttributeName = "code")]
        public int Code { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
