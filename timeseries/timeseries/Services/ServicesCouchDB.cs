﻿using MyCouch;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using timeseries.Models;

namespace timeseries.Services
{
    class ServicesCouchDB
    {
        //couchDB
        public static HttpClient ConnectToCouchDB()
        {
            String usernamepass = "yusuf:yusuf";//"username:password"
            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(usernamepass);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
            }
        public static void WriteDataCouchDB(List<int> period, int sensorQuantity)
        {
            string ip = "http://yusuf:yusuf@10.100.3.54:5984/";
            string database1 = "test"; // interval 1 s
            string database2 = "test2"; // interval 5 s
            string database3 = "test3"; // interval 10 s
            string database4 = "test4"; // kolom id buat sendiri 
            //select database
            string database = database4;
            DateTime dt = new DateTime(2021,1,1,0,0,0);

            using (var db = new MyCouchStore(ip, database))
            {
                for (int i=0;i<86400;i++)
                {
                    SensorData3 doc = new SensorData3();
                    if (i % 5 == 0)
                    {
                        doc.ModFive = 0;
                    }
                    else
                    {
                        doc.ModFive = i;
                    }
                    
                    if (i % 10 == 0)
                    {
                        doc.ModTen = 0; 
                    }
                    else
                    {
                        doc.ModTen = i;
                    }
                    doc.datetime = dt.AddSeconds(i);
                    doc.sensorData = new List<double>();
                    for (int sen = 0; sen < sensorQuantity; sen++)
                    {
                        Random random = new Random();
                        double newRandom = random.NextDouble();
                        doc.sensorData.Add(newRandom);
                    }
                    var response = db.StoreAsync(doc);
                    Console.WriteLine(response.Result);
                }
            }
            //using (var db = new MyCouchStore(ip, database))
            //{
            //    for (int d = 0; d <= period[0]; d++)
            //    {
            //        for (int h = 0; h <= period[1]; h++)
            //        {
            //            for (int m = 0; m <= period[2]; m++)
            //            {
            //                for (int s = 0; s <= period[3]; s += interval)
            //                {
            //                    SensorData doc = new SensorData
            //                    {
            //                        datetime = new DateTime(2021, 1, d + 1, h, m, s)
            //                    };
            //                    for (int sen = 0; sen < sensorQuantity; sen++)
            //                    {
            //                        Random random = new Random();
            //                        double newRandom = random.NextDouble();
            //                        doc.sensorData.Add(newRandom);
            //                    }
            //                    var response = db.StoreAsync(doc);
            //                    Console.WriteLine(response.Result);
            //                }
            //            }
            //        }
            //    }
            //}

        }

        public static async Task QueryingCouchDB(HttpClient client, int dataQuantity)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            HttpResponseMessage result = await client.GetAsync($"http://10.100.3.54:5984/test/_design/12hours/_view/name-name?limit={dataQuantity}");

            String a = result.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(a);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

    }
}
