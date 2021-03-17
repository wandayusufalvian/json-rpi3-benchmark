using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using timeseries.RavenDB;

namespace timeseries
{
    public static class DocumentStoreHolderRaven
    {
        static string db1 = "timeseries"; // using model SensorData
        static string db2 = "timeseries2"; // using model SensorData3


        private static readonly Lazy<IDocumentStore> LazyStore =
        new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { "https://a.frmltrx.development.run/" },
                Database = db2,
                Certificate = new X509Certificate2("C:\\Users\\DELL\\Downloads\\admin.client.certificate.frmltrx.pfx")
            };
            //new DataByModFive().Execute(store);
            return store.Initialize();
        });

        public static IDocumentStore Store => LazyStore.Value;
    }
}
