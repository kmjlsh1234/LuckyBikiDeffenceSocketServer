﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace LuckyBikiDeffenceSocketServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
        }
    }
}
