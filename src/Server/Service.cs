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
      Get["/"] = _ => { return "Hello World"; };
    }
  }
}
