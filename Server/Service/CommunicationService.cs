using Newtonsoft.Json;
using Server.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Service
{
    public class CommunicationService
    {
        public void StartListener()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine("Started listening requests at: {0}:{1}", ep.Address, ep.Port);

            while (true)
            {
                const int bytesize = 1024 * 1024;
                byte[] buffer = new byte[bytesize];

                var sender = listener.AcceptTcpClient();
                sender.GetStream().Read(buffer, 0, bytesize);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string messageString = Encoding.GetEncoding(1252).GetString(buffer);
                Message message = JsonConvert.DeserializeObject<Message>(messageString);

                if (message.Action == Models.Action.Generate)
                {
                    DiscountGenerationService dgs = new DiscountGenerationService();
                    dgs.GenerateCodes(2000);
                }
                else if (message.Action == Models.Action.Use)
                {
                    DiscountSearchService dss = new DiscountSearchService();
                    Code updatedCode = dss.FindCodeInFile(message.Text);
                    dss.MarkUsedCode(updatedCode);
                }
            }
        }
    }
}
