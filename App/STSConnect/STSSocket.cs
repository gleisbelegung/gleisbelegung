
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;
using Gleisbelegung.App.STSConnect.Common;
using System.Linq;
using PubSub;
using Gleisbelegung.App.STSConnect.MessageProcessors;
using Gleisbelegung.App.Events;

namespace Gleisbelegung.App.STSConnect
{
    public class STSSocket
    {
        private readonly int STS_PORT = 3691;
        private readonly Socket socket;
        private bool isQuitting = false;

        Dictionary<string, Type> incomingMessagesMapper = new Dictionary<string, Type> {
            { "status", typeof(StatusMessage) }
        };

        public STSSocket()
        {
            socket = StartClientAsync();
            DoWhatever();
        }

        private Socket StartClientAsync()
        {
            // Connect to a Remote server
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is  IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEndpoint = new IPEndPoint(ipAddress, STS_PORT);

            // Create a TCP/IP  socket.
            var socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(remoteEndpoint);
            socket.Blocking = false;

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
                if (socket.Available > 0)
                {
                    byte[] bytes = new byte[socket.Available];
                    int bytesRec = socket.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    XElement element = XElement.Parse(data);
                    ProcessMessage(element.Name.ToString(), data);
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

            var type = incomingMessagesMapper[elementName];
            var message = XMLHelper.Deserialize(type, data);

            switch (message)
            {
                case StatusMessage typedMessage:
                    Hub.Default.Publish(new IncomingMessageEvent<StatusMessage>(typedMessage));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void SendMessage(IOutgoingMessage outgoingMessage)
        {
            if (!socket.Connected)
            {
                throw new PluginException("Could not write message, because socket is not connected");
            }

            socket.Send(Encoding.ASCII.GetBytes(XMLHelper.SerializeAsXml(outgoingMessage)));
        }
    }
}
