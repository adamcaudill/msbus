using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSBus.Server
{
  internal class DataStore
  {
    //todo: update this to use RavenDb as a backing store, instead of just using local fields
    public DataStore()
    {
      Boxes = new Dictionary<string, Box>();
    }

    public Dictionary<string, Box> Boxes { get; private set; }
  }
}
