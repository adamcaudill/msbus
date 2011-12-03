using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSBus.Server
{
  internal static class Extensions
  {
    public static Uri ToUri (this Nancy.Url url)
    {
      //build what should be the valid string
      string maybeUrl;

      maybeUrl = string.Format("{0}://{1}", url.Scheme, url.HostName);

      if (!string.IsNullOrEmpty(url.BasePath))
        maybeUrl += "/" + url.BasePath;

      maybeUrl += url.Path;

      if (!string.IsNullOrEmpty(url.Query))
        maybeUrl += "?" + url.Query;

      if (!string.IsNullOrEmpty(url.Fragment))
        maybeUrl += "#" + url.Fragment;

      Uri uri;
      Uri.TryCreate(maybeUrl, UriKind.Absolute, out uri);

      return uri;
    }
  }
}
