
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

namespace Gleisbelegung.App.STSConnect
{
    public class STSSocket : IEventListener<SendMessageEvent>
    {
        private readonly int STS_PORT = 3691;
        private readonly Socket socket;
        private bool isQuitting = false;

        private Dictionary<string, IIncomingMessageWrapper> incomingMessagesMapper;

        interface IIncomingMessageWrapper
        {
            void DeserializeAndPublishEvent(string xml);
        }

        class IncomingMessageWrapper<T> : IIncomingMessageWrapper where T : IIncomingMessage
        {
            public void DeserializeAndPublishEvent(string data)
            {
                var message = XMLHelper.Deserialize<T>(data);
                EventHub.Publish<IncomingMessageEvent<T>>(new IncomingMessageEvent<T>(message));
            }
        }

        public STSSocket()
        {
            incomingMessagesMapper = new Dictionary<string, IIncomingMessageWrapper>();

            this.RegisterSubscriptions();
            RegisterMessageMappingData();

            socket = StartClientAsync();
            DoWhatever();
        }

        private void RegisterMessageMappingData()
        {
            // load all incoming messages, get the name of the xml root attribute and register them
            var incomingMessageTypes = ReflectionHelper.FindTypesByInterface<IIncomingMessage>();
            var generic = typeof(IncomingMessageWrapper<>);
            foreach (var type in incomingMessageTypes)
            {
                var name = ReflectionHelper.GetXmlRootElementNameByType(type);
                var instance = ReflectionHelper.CreateInstanceOfGenericType<IIncomingMessageWrapper>(generic, new[] { type });

                incomingMessagesMapper.Add(name, instance);
            }
        }

        private Socket StartClientAsync()
        {
            EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.CONNECTING));

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEndpoint = new IPEndPoint(ipAddress, STS_PORT);

            var socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(remoteEndpoint);
            socket.Blocking = false;

            EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.CONNECTED));

            return socket;
        }

        private void DoWhatever()
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(1000);
                        GD.Print("After pause");
                        ReadMessage();
                    }
                });

            }
            catch (Exception e)
            {
                GD.Print(e.ToString());
            }
        }

        private void ReadMessage()
        {
            try
            {
                if (socket.Available == 0)
                    return;

                byte[] bytes = new byte[socket.Available];
                int bytesRec = socket.Receive(bytes);
                var data = Encoding.UTF8.GetString(bytes, 0, bytesRec);


                var perLine = data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var currentMessage = string.Empty;
                var count = 0;

                foreach (var line in perLine)
                {
                    var openingTags = Regex.Matches(line, "<(?!/)(.*)>").Count; // all opening tags
                    var closingTags = Regex.Matches(line, "</.*>").Count + Regex.Matches(line, "<.*/>").Count; // all closing tags and self closing tags
                    count += openingTags - closingTags;
                    currentMessage += line;

                    if (count == 0)
                    {
                        XElement element = XElement.Parse(currentMessage);
                        ProcessMessage(element.Name.ToString(), currentMessage);

                        currentMessage = string.Empty;
                    }
                }
            }
            catch (System.Exception e)
            {
                GD.Print(e.ToString());
            }
        }

        private void ProcessMessage(string elementName, string data)
        {
            if (!incomingMessagesMapper.ContainsKey(elementName))
            {
                throw new PluginException($"No message handler for {elementName}");
            }

            var wrapper = incomingMessagesMapper[elementName];
            wrapper.DeserializeAndPublishEvent(data);
        }

        public void SendMessage(IOutgoingMessage outgoingMessage)
        {
            if (!socket.Connected)
            {
                throw new PluginException("Could not write message, because socket is not connected");
            }

            socket.Send(Encoding.UTF8.GetBytes(XMLHelper.Serialize(outgoingMessage) + "\n"));
        }

        public void ProcessEvent(SendMessageEvent eventData)
        {
            SendMessage(eventData.Message);
        }
    }
}
