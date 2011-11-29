using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nancy;
using Newtonsoft.Json;

namespace MSBus.Server
{
  internal class JsonNetSerializer : ISerializer
  {
    //based on code from https://github.com/NancyFx/Nancy.Serialization.ServiceStack
    
    public bool CanSerialize(string contentType)
    {
      return _IsJsonType(contentType);
    }

    public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
    {
      var ser = JsonSerializer.Create(new JsonSerializerSettings());
      using (var writer = new JsonTextWriter(new StreamWriter(outputStream)))
      {
        ser.Serialize(writer, model);
        writer.Flush();
      }
    }

    private static bool _IsJsonType(string contentType)
    {
      if (string.IsNullOrEmpty(contentType))
      {
        return false;
      }

      var contentMimeType = contentType.Split(';')[0];

      return contentMimeType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase) ||
             contentMimeType.Equals("text/json", StringComparison.InvariantCultureIgnoreCase) ||
            (contentMimeType.StartsWith("application/vnd", StringComparison.InvariantCultureIgnoreCase) &&
             contentMimeType.EndsWith("+json", StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
