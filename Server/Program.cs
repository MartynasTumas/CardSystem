﻿using Server.Service;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            CommunicationService cs = new CommunicationService();
            cs.StartListener();
        }
    }
}
