using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Gate;
using Gate.Kayak;
using Nancy.Hosting.Owin;

namespace MSBus.Server
{
  public class RestServer
  {
    public void Start()
    {
      var ep = new IPEndPoint(IPAddress.Any, 20589);
      KayakGate.Start(new SchedulerDelegate(), ep, _Configuration);
    }

    private static void _Configuration(IAppBuilder builder)
    {
      builder.RescheduleCallbacks().Run(Delegates.ToDelegate(new NancyOwinHost().ProcessRequest));
    }
  }
}
