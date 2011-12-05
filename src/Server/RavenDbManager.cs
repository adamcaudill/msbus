using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Raven.Client;
using Raven.Client.Embedded;

namespace MSBus.Server
{
  internal static class RavenDbManager
  {
    private static EmbeddableDocumentStore _documentStore;
    private static string _dataPath;

    static RavenDbManager()
    {
      _dataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
    }

    public static void Start()
    {
      _documentStore = new EmbeddableDocumentStore { DataDirectory = _dataPath, UseEmbeddedHttpServer = true };
      _documentStore.Initialize();
    }

    public static void Stop()
    {
      _documentStore.Dispose();
    }

    public static IDocumentSession OpenSession()
    {
      return _documentStore.OpenSession();
    }
  }
}
