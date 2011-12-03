using System;
using System.Collections.Generic;
using System.IO;
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

    public static byte[] ReadToEnd(this Stream stream)
    {
      const int BUFFER = 4096;

      var buffer = new byte[BUFFER];
      int read;
      var ret = new byte[0];

      //reset the stream incase other activity has moved things, but only if the stream supports seeking
      if (stream.CanSeek)
        stream.Position = 0;

      do
      {
        read = stream.Read(buffer, 0, buffer.Length);

        if (read > 0)
        {
          var pos = ret.Length;
          Array.Resize(ref ret, read + ret.Length);
          Array.Copy(buffer, 0, ret, pos, read);

          buffer = new byte[BUFFER];
        }
      } while (read > 0);

      return ret;
    }
  }
}
