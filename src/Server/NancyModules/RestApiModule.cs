using System;
using System.Linq;
using MSBus.Server.Responses;
using Nancy;
using Nancy.ModelBinding;

namespace MSBus.Server.NancyModules
{
  public class RestApiModule : NancyModule
  {    
    public RestApiModule() : base("/api1")
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
                                          var msg = this.Bind<Message>();
                                          return _CreateMessage(x["box"], x["message"], msg);
                                        };
      
      //delete a message
      Delete["/boxes/{box}/{message}"] = x => { return _DeleteMessage(x["box"], x["message"]); };

      //get message details
      Get["/boxes/{box}/{message}"] = x => { return _GetMessage(x["box"], x["message"]); };
    }

    private Response _GetVersion()
    {
      var res = new {Version = "0.0"};
      return new SimplifiedJsonResponse(res);
    }

    private Response _GetBoxList()
    {
      var urlBase = Request.Url.ToUri() + "/";
      var boxes = DataStore.Boxes.Keys.ToDictionary(x => x, x => urlBase + x);
      return new SimplifiedJsonResponse(boxes);
    }

    private Response _CreateBox(string boxName)
    {
      //todo: validate the box name to make sure it's valid
      if (DataStore.Boxes.ContainsKey(boxName))
        return new ActionFailedResponse("Box already exists", HttpStatusCode.Conflict);

      DataStore.Boxes.Add(boxName, new Box());
      return new BoxCreatedResponse(Request.Url.ToUri().ToString());
    }

    private Response _DeleteBox(string boxName)
    {
      //todo: find out why Nancy sends it's own body instead of ours
      if (!DataStore.Boxes.ContainsKey(boxName))
        return new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);

      DataStore.Boxes.Remove(boxName);
      return new SimplifiedJsonResponse(new {Result = "Box Deleted"});
    }

    private Response _CreateMessage(string boxName, string id, Message msg)
    {
      if (!DataStore.Boxes.ContainsKey(boxName))
        return new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);

      var box = DataStore.Boxes[boxName];
      if (box.Messages.ContainsKey(id))
        return new ActionFailedResponse("Message ID Already Exists", HttpStatusCode.Conflict);

      box.Messages.Add(id, msg);
      return new SimplifiedJsonResponse(new { Result = "Message Added" });
    }

    private Response _DeleteMessage(string boxName, string id)
    {
      if (!DataStore.Boxes.ContainsKey(boxName))
        return new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);

      var box = DataStore.Boxes[boxName];
      if (!box.Messages.ContainsKey(id))
        return new ActionFailedResponse("Message doesn't exist", HttpStatusCode.NotFound);

      box.Messages.Remove(id);
      return new SimplifiedJsonResponse(new { Result = "Message Deleted" });
    }

    private Response _GetMessage(string boxName, string id)
    {
      if (!DataStore.Boxes.ContainsKey(boxName))
        return new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);

      var box = DataStore.Boxes[boxName];
      if (!box.Messages.ContainsKey(id))
        return new ActionFailedResponse("Message doesn't exist", HttpStatusCode.NotFound);

      return new SimplifiedJsonResponse(new { box.Messages[id].Body });
    }
  }
}
