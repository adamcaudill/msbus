using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.IO;

namespace MSBus.Server
{
  internal class Message
  {
    public Message(RequestStream requestStream)
    {
      Body = Encoding.UTF8.GetString(requestStream.ReadToEnd());
    }
    
    public string Body { get; set; }
  }
}
