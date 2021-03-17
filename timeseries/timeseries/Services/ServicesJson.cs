using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using timeseries.Models;

namespace timeseries.Services
{
    public class ServicesJson
    {
        public static void JsonSerialization(List<int> period, int sensorQuantity)
        {   //1 json file contain all data as list
            List<SensorData> SensorDatas = new List<SensorData>();

            for (int d = 0; d <= period[0]; d++)
            {
                for (int h = 0; h <= period[1]; h++)
                {
                    for (int m = 0; m <= period[2]; m++)
                    {
                        for (int s = 0; s <= period[3]; s++)
                        {
                            SensorData doc = new SensorData
                            {
                                datetime = new DateTime(2021, 1, d + 1, h, m, s)
                            };
                            for (int sen = 0; sen < sensorQuantity; sen++)
                            {
                                Random random = new Random();
                                double newRandom = random.NextDouble();
                                doc.sensorData.Add(newRandom);
                            }
                            SensorDatas.Add(doc);
                        }
                    }
                }
            }
            string jsonFile = JsonConvert.SerializeObject(SensorDatas.ToArray());
            System.IO.File.WriteAllText(@"Z:\ini.txt",jsonFile);
        }

        public static List<SensorData> JsonDeserialization()
        {
            string jsonFile = System.IO.File.ReadAllText(@"Z:\ts-api\Data\data-1.txt");
            List<SensorData> sensorDatas = JsonConvert.DeserializeObject<List<SensorData>>(jsonFile);
            Console.WriteLine($"File Quantity : {sensorDatas.Count}");
            return sensorDatas;
        }

        public static void JsonSerialization2(List<int> period, int sensorQuantity)
        {   // each data = 1 json file 
            int index = 0;
            for (int d = 0; d <= period[0]; d++)
            {
                for (int h = 0; h <= period[1]; h++)
                {
                    for (int m = 0; m <= period[2]; m++)
                    {
                        for (int s = 0; s <= period[3]; s++)
                        {   
                            SensorData doc = new SensorData
                            {
                                datetime = new DateTime(2021, 1, d + 1, h, m, s)
                            };
                            for (int sen = 0; sen < sensorQuantity; sen++)
                            {
                                Random random = new Random();
                                double newRandom = random.NextDouble();
                                doc.sensorData.Add(newRandom);
                            }
                            string jsonFile = JsonConvert.SerializeObject(doc);
                            System.IO.File.WriteAllText(@$"Z:\JsonFiles\{index}.txt", jsonFile);
                            index++;
                        }
                    }
                }
            }

            
        }

        public static List<SensorData> JsonDeserialization2()
        {
            List<SensorData> sensorDatas = new List<SensorData>();

            for(int index = 0; index < 86400; index++)
            {
                if (index % 10 == 0)
                {
                    string jsonFile = System.IO.File.ReadAllText(@$"Z:\JsonFiles\{index}.txt");
                    SensorData sensordata = JsonConvert.DeserializeObject<SensorData>(jsonFile);
                    sensorDatas.Add(sensordata);
                }
            }
            return sensorDatas;
        }

        public static void JsonSerialization3(List<int> period, int sensorQuantity)
        {
            //same with jsonSerialization1, but use different data model 
            List<SensorData2> SensorDatas = new List<SensorData2>();
            int iD = 0; 
            for (int d = 0; d <= period[0]; d++)
            {
                for (int h = 0; h <= period[1]; h++)
                {
                    for (int m = 0; m <= period[2]; m++)
                    {
                        for (int s = 0; s <= period[3]; s++)
                        {
                            SensorData2 doc = new SensorData2
                            {   //id=iD,
                                datetime = new DateTime(2021, 1, d + 1, h, m, s)
                            };
                            for (int sen = 0; sen < sensorQuantity; sen++)
                            {
                                Random random = new Random();
                                double newRandom = random.NextDouble();
                                doc.sensorData.Add(newRandom);
                            }
                            SensorDatas.Add(doc);
                            iD++;
                        }
                    }
                }
            }
            string jsonFile = JsonConvert.SerializeObject(SensorDatas.ToArray());
            System.IO.File.WriteAllText(@"Z:\ini-2.txt", jsonFile);
        }

        public static IEnumerable JsonDeserialization3()
        {

            //string jsonFile = System.IO.File.ReadAllText(@"Z:\ini.txt");
            StreamReader sr = new StreamReader(@"Z:\ini.txt");
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(sr.ReadToEnd());

            JArray o = JArray.Parse(strBlder.ToString());

            for (int i = 0; i < 1; i++)
            {
                JToken acme = o.SelectToken($"$.[{i}]['sensorData']");
                Console.WriteLine(acme);
            }
            return o; 
        }



    }
}
