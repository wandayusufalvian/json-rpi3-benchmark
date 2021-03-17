using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using timeseries.Models;
using timeseries.RavenDB;

namespace timeseries.Services
{
    class ServicesRavenDB
    {
        public static void WriteData(List<int> period, int sensorQuantity)
        {   //save data to RavenDB

            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {   //make sure DocumentStoreHolderRaven point to correct database.
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s ++)
                            {
                                var sensordata = new SensorData3();
                                var dt= new DateTime(2021, 1, d + 1, h, m, s);
                                sensordata.datetime = dt;
                                for (int sen = 0; sen < sensorQuantity; sen++)
                                {
                                    Random random = new Random();
                                    double newRandom = random.NextDouble();
                                    sensordata.sensorData = new List<double>();
                                    sensordata.sensorData.Add(newRandom);
                                }
                                sensordata.ModFive= (new TimeSpan(dt.Ticks - (new DateTime(2021, 1, 1, 0, 0, 0)).Ticks)).TotalSeconds % 5;
                                sensordata.ModTen= (new TimeSpan(dt.Ticks - (new DateTime(2021, 1, 1, 0, 0, 0)).Ticks)).TotalSeconds % 10;
                                session.Store(sensordata);
                            }
                        }
                    }
                }

                session.SaveChanges();
            }

        }

        public static List<SensorData3> RetrieveAllDocs()
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                /* To get data sensor: 
                 * returned list (l) => l.sensorData (list object) => use index to get the value(e.g. : l.sensorData[0])
                 */
                return session
                        .Query<SensorData3>()
                        .Select(x => x)
                        .ToList();
            }
        }

        public static List<SensorData> RetrieveDataIntervalTime(DateTime start, DateTime end)
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                return session
                        .Query<SensorData>()
                        .Where(x => x.datetime >= start)
                        .Where(x => x.datetime <= end)
                        .Select(x => x)
                        .ToList();

            }
        }

        public static List<SensorData3> RetrieveDataModFive()
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                return session
                        .Query<SensorData3>()
                        .Where(x=>x.ModFive==0)
                        .Select(x => x)
                        .ToList();
            }
        }

        public static List<SensorData3> RetrieveDataModTen()
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                return session
                        .Query<SensorData3>()
                        .Where(x => x.ModTen == 0)
                        .Select(x => x)
                        .ToList();
            }
        }
    }
}
