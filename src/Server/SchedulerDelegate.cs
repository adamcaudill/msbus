using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kayak;

namespace MSBus.Server
{
  internal class SchedulerDelegate : ISchedulerDelegate
  {
    public void OnException(IScheduler scheduler, Exception e)
    {
      //TODO: Something
    }

    public void OnStop(IScheduler scheduler)
    {
      //TODO: Something
    }
  }
}
