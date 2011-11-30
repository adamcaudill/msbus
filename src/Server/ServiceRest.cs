using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace MSBus.Server
{
  public class ServiceRest : NancyModule
  {
    public ServiceRest() : base("/api1")
    {
      //version number request
      Get["/version"] = _ => { return _GetVersion(); };

      //list the current boxes
      Get["/boxes"] = _ => { return _GetBoxList(); };

      //create a new box
      Put["/boxes/{box}"] = x => { return _CreateBox(x["box"]); };

      //delete a box
      Delete["/boxes/{box}"] = x => { return _DeleteBox(x["box"]); };

      //create a new message
      Put["/boxes/{box}/{message}"] = x =>
                                       {
                                         var req = this.Bind<Message>();
                                         return _CreateMessage(x["box"], x["message"], req);
                                       };
      
      //delete a message
      Delete["/boxes/{box}/{message}"] = x => { return _DeleteMessage(x["box"], x["message"]); };

      //get message details
      Get["/boxes/{box}/{message}"] = x => { return _GetMessage(x["box"], x["message"]); };
    }

    private Response _GetVersion()
    {
      var res = new {Version = "0.0"};
      return new JsonResponse(res, new JsonNetSerializer());
    }

    private Response _GetBoxList()
    {
      throw new NotImplementedException();
    }

    private Response _CreateBox(string box)
    {
      throw new NotImplementedException();
    }

    private Response _DeleteBox(string box)
    {
      throw new NotImplementedException();
    }

    private Response _CreateMessage(string box, string id, Message req)
    {
      throw new NotImplementedException();
    }

    private Response _DeleteMessage(string box, string id)
    {
      throw new NotImplementedException();
    }

    private Response _GetMessage(string box, string id)
    {
      throw new NotImplementedException();
    }
  }
}
