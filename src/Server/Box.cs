using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSBus.Server
{
  internal class Box
  {
    public Box()
    {
      Messages = new Dictionary<string, Message>();
    }

    public Dictionary<string, Message> Messages { get; private set; }
  }
}
