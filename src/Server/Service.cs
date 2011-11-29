using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace MSBus.Server
{
  public class Service : NancyModule
  {
    public Service()
    {
      Get["/"] = _ => { return "<html><head><title>Welcome to MSBus!</title></head><body>Welcome to MSBus, the lightweight message queueing and transport system.</body></html>"; };
      Get["/version"] = _ => { return "MSBus v0.0"; };
    }
  }
}
