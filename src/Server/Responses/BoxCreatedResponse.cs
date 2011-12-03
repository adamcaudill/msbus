using Nancy;

namespace MSBus.Server.Responses
{
  internal class BoxCreatedResponse : SimplifiedJsonResponse
  {
    public BoxCreatedResponse(string path) : base(new {Box = path})
    {
      StatusCode = HttpStatusCode.Created;
    }
  }
}
