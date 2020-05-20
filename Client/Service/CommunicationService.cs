using Newtonsoft.Json;
using Server.Models;
using System;
using System.Net.Sockets;
using System.Text;

namespace Client.Service
{
    public class CommunicationService
    {
        public void StartConnection()
        {
            Message message = new Message();
            message.Text = "¥¤ÿ’"; //maybe needs to be changed to correct discount code (depends on what is generated in file)
            message.Action = Server.Models.Action.Use;// now message will will try to search for discount code
                                                      // you can set action it to Server.Models.Action.Generate to generate discount codes (then text can be empty)

            string jsonMessage = JsonConvert.SerializeObject(message);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] byteArray = Encoding.GetEncoding(1252).GetBytes(jsonMessage);

            SendMessage(byteArray);

            Console.Read();
        }

        private static byte[] SendMessage(byte[] messageBytes)
        {
            const int bytesize = 1024 * 1024;
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 1234);
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Connected to the server");

                messageBytes = new byte[bytesize];

                stream.Read(messageBytes, 0, messageBytes.Length);

                stream.Dispose();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes;
        }
    }
}
