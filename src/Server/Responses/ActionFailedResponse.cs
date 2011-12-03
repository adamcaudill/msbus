using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace MSBus.Server.Responses
{
  internal class ActionFailedResponse : SimplifiedJsonResponse
  {
    public ActionFailedResponse(string errorMessage, HttpStatusCode status) : base(new {Error = errorMessage})
    {
      StatusCode = status;
    }
  }
}
