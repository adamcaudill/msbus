using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MSBus.Server.Responses;
using Nancy;
using Nancy.ModelBinding;

namespace MSBus.Server.NancyModules
{
  public class RestApiModule : NancyModule
  {
    private Stopwatch _stopwatch;
    
    public RestApiModule() : base("/api1")
    {
      //todo: debug code, get rid of this later
      Before.AddItemToStartOfPipeline(x =>
                                        {
                                          _stopwatch = new Stopwatch();
                                          _stopwatch.Start();
                                          return null;
                                        });
      
      //version number request
      Get["/version"] = _ => { return _GetVersion(); };

      //list the current boxes
      Get["/boxes"] = _ => { return _GetBoxList(); };

      //create a new box
      Put["/boxes/{box}"] = x => { return _CreateBox(x["box"]); };

      //delete a box
      Delete["/boxes/{box}"] = x => { return _DeleteBox(x["box"]); };

      //list messages in a box
      Get["/boxes/{box}"] = x => { return _GetMessagesInBox(x["box"]); };

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

      //todo: debug code, get rid of this later
      After.AddItemToEndOfPipeline(x =>
                                     {
                                       _stopwatch.Stop();
                                       Console.WriteLine("> ({0}ms) {1}: {2}", _stopwatch.ElapsedMilliseconds, Request.Method, Request.Url.Path);
                                     });
    }

    private Response _GetVersion()
    {
      var res = new {Version = "0.0"};
      return new SimplifiedJsonResponse(res);
    }

    private Response _GetBoxList()
    {
      Dictionary<string, string> ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var urlBase = Request.Url.ToUri() + "/";
        var data = db.Query<Box>().ToList();
        ret = data.ToDictionary(x => x.Name, x => urlBase + x.Name);
      }

      return new SimplifiedJsonResponse(ret);
    }

    private Response _CreateBox(string boxName)
    {
      //todo: validate the box name to make sure it's valid
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        if (db.Query<Box>().Where(x => x.Name == boxName).Any())
        {
          ret = new ActionFailedResponse("Box already exists", HttpStatusCode.Conflict);
        }
        else
        {
          var box = new Box(boxName);
          db.Store(box);
          db.SaveChanges();
          ret = new BoxCreatedResponse(Request.Url.ToUri().ToString());
        }
      }

      return ret;
    }

    private Response _DeleteBox(string boxName)
    {
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var box = db.Query<Box>().Where(x => x.Name == boxName).FirstOrDefault();

        if (box == null)
        {
          ret = new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);
        }
        else
        {
          db.Delete(box);
          db.SaveChanges();
          ret = new SimplifiedJsonResponse(new { Result = "Box Deleted" });
        }
      }

      return ret;
    }

    private Response _GetMessagesInBox(string boxName)
    {
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var box = db.Query<Box>().Where(x => x.Name == boxName).FirstOrDefault();

        if (box == null)
        {
          ret = new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);
        }
        else
        {
          var urlBase = Request.Url.ToUri() + "/";

          var messages = box.Messages.Keys.ToDictionary(x => x, x => urlBase + x);
          ret = new SimplifiedJsonResponse(messages);
        }
      }

      return ret;
    }

    private Response _CreateMessage(string boxName, string id, Message msg)
    {
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var box = db.Query<Box>().Where(x => x.Name == boxName).FirstOrDefault();
        
        if (box == null)
        {
          ret = new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);
        }
        else
        {
          if (box.Messages.ContainsKey(id))
          {
            ret = new ActionFailedResponse("Message ID Already Exists", HttpStatusCode.Conflict);
          }
          else
          {
            box.Messages.Add(id, msg);
            db.SaveChanges();
            ret = new SimplifiedJsonResponse(new {Result = "Message Added"});
          }
        }
      }

      return ret;
    }

    private Response _DeleteMessage(string boxName, string id)
    {
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var box = db.Query<Box>().Where(x => x.Name == boxName).FirstOrDefault();

        if (box == null)
        {
          ret = new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);
        }
        else
        {
          if (!box.Messages.ContainsKey(id))
          {
            ret = new ActionFailedResponse("Message doesn't exist", HttpStatusCode.NotFound);
          }
          else
          {
            box.Messages.Remove(id);
            db.SaveChanges();
            ret = new SimplifiedJsonResponse(new { Result = "Message Deleted" });
          }
        }
      }

      return ret;
    }

    private Response _GetMessage(string boxName, string id)
    {
      Response ret;

      using (var db = RavenDbManager.OpenSession())
      {
        var box = db.Query<Box>().Where(x => x.Name == boxName).FirstOrDefault();

        if (box == null)
        {
          ret = new ActionFailedResponse("Box doesn't exist", HttpStatusCode.NotFound);
        }
        else
        {
          if (!box.Messages.ContainsKey(id))
          {
            ret = new ActionFailedResponse("Message doesn't exist", HttpStatusCode.NotFound);
          }
          else
          {
            ret = new SimplifiedJsonResponse(new { box.Messages[id].Body });
          }
        }
      }

      return ret;
    }
  }
}
