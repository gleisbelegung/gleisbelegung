
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

namespace Gleisbelegung.App.STSConnect
{
    public class STSSocket
    {
        public STSSocket()
        {
            StartClientAsync();
        }

        private void StartClientAsync()
        {
            byte[] bytes = new byte[1024];

            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is  IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 3691);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    GD.Print("Socket connected to ",
                        sender.RemoteEndPoint.ToString());

                    var task = Task.Run(async () =>  // <- marked async
                    {
                        while (true)
                        {
                            // DoWork();
                            await Task.Delay(10000); // <- await with cancellation
                            GD.Print("After pause");
                        }
                    });

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    GD.Print("Echoed test = ", data);

                    XmlSerializer serializer = new XmlSerializer(typeof(StatusMessage));
                    using (StringReader reader = new StringReader(data))
                    {
                        var test = (StatusMessage)serializer.Deserialize(reader);
                    }

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    GD.Print("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    GD.Print("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    GD.Print("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                GD.Print(e.ToString());
            }
        }
    }
}