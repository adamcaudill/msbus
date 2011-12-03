using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSBus.Server
{
  internal static class DataStore
  {
    //todo: update this to use RavenDb as a backing store, instead of just using local fields
    static DataStore()
    {
      Boxes = new Dictionary<string, Box>();
    }

    public static Dictionary<string, Box> Boxes { get; private set; }
  }
}
