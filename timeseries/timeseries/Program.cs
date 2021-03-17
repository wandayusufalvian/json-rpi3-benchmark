using System;
using System.Collections.Generic;
using MyCouch;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Linq;
using timeseries.Services;

namespace timeseries
{
    class Program
    {
        static void Main()
        {
            //WRITE DATA==============================================
            //ServicesJson.JsonSerialization3(new List<int> { 0, 0, 0, 5 }, 5);
            //Services.ServicesRavenDB.WriteData(new List<int> { 0, 23, 59, 59 }, 5);
            ServicesCouchDB.WriteDataCouchDB(new List<int> { 0, 0, 0, 5 }, 5);

            //READ DATA===============================================
            //ServicesJson.JsonDeserialization();
            //ServicesRavenDB.re
            //Services.ServicesRavenDB.RetrieveAllDocsRavenDB();
            //var x=Services.ServicesRavenDB.RetrieveAllDocs();
            //foreach (var i in x)
            //{
            //    Console.WriteLine(i.datetime +","+ i.ModFive);
            //}

        }
    }
}
