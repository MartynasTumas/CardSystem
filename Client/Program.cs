using Client.Service;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CommunicationService cs = new CommunicationService();
            cs.StartConnection();
        }
    }
}
