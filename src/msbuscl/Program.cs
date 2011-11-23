using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSBus.Server;

namespace MSBus.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      var server = new RestServer();
      server.Start();
    }
  }
}
