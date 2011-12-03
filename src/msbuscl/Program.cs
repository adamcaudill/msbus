using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MSBus.Server;

namespace MSBus.Console
{
  class Program
  {
    private static ServerManager _server;
    
    static void Main(string[] args)
    {
      _server = new ServerManager();
      _StartServer();
      System.Console.WriteLine("Press any key to stop server...");
      System.Console.ReadKey();
      System.Console.WriteLine("Stopping Server...");
      _server.Stop();
      System.Console.WriteLine("Server Stopped - Press any key to exit...");
      System.Console.ReadKey();
    }

    private static void _StartServer()
    {
      var th = new Thread(_server.Start) {Name = "_RestServer"};
      th.Start();
    }
  }
}
