using Nancy;

namespace MSBus.Server.NancyModules
{
  public class RootModule : NancyModule
  {
    public RootModule()
    {
      Get["/"] = _ => { return "<html><head><title>Welcome to MSBus!</title></head><body>Welcome to MSBus, the lightweight message queueing and transport system.</body></html>"; };
      Get["/version"] = _ => { return "MSBus v0.0"; };
    }
  }
}
