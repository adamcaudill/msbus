using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace MSBus.Server
{
  public class ServiceRest : NancyModule
  {
    public ServiceRest() : base("/api1")
    {
      
    }
  }
}
