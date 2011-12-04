using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Gate;
using Gate.Kayak;
using Kayak;
using Nancy.Hosting.Owin;

namespace MSBus.Server
{
  public class ServerManager
  {
    private readonly Dictionary<string, object> _context = new Dictionary<string, object>();
    
    public void Start()
    {
      RavenDbManager.Start();
      var ep = new IPEndPoint(IPAddress.Any, 20589);
      KayakGate.Start(new SchedulerDelegate(), ep, AppBuilder.BuildConfiguration(_Configuration), _context);
    }

    public void Stop()
    {
      IScheduler sched = null;

      if (_context.ContainsKey("kayak.Scheduler"))
        sched = _context["kayak.Scheduler"] as IScheduler;

      if (sched != null)
        sched.Stop();

      RavenDbManager.Stop();
    }

    private static void _Configuration(IAppBuilder builder)
    {
      builder.RescheduleCallbacks().Run(Delegates.ToDelegate(new NancyOwinHost().ProcessRequest));
    }
  }
}
