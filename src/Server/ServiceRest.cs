using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Responses;

namespace MSBus.Server
{
  public class ServiceRest : NancyModule
  {
    public ServiceRest() : base("/api1")
    {
      Get["/version"] = _ => { return _GetVersion(); };
    }

    private Response _GetVersion()
    {
      var res = new {Version = "0.0"};
      return new JsonResponse(res, new DefaultJsonSerializer());
    }
  }
}
