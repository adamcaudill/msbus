using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Responses;

namespace MSBus.Server.Responses
{
  internal class SimplifiedJsonResponse : JsonResponse
  {
    public SimplifiedJsonResponse(object model) : base(model, new JsonNetSerializer())
    {
      
    }
  }
}
