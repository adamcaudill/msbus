using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Nancy.Owin;
using Microsoft.Owin.Hosting;

namespace MSBus.Server
{
  public class ServerManager
  {
    private IDisposable _server;
    
    public void Start()
    {
      RavenDbManager.Start();

      var url = "http://+:20589";
      _server = WebApp.Start<Startup>(url);
    }

    public void Stop()
    {
      _server.Dispose();

      RavenDbManager.Stop();
    }
  }
}
