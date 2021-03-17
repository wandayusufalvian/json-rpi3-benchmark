using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace timeseries.RavenDB
{
    public class DataByModFive : AbstractIndexCreationTask<SensorData>
    {
        public DataByModFive()
        {

            //TimeSpan elapsedSpan = new TimeSpan(sensor.datetime.Ticks - start.Ticks);
            this.Map = sensors => from sensor in sensors
                                  select new { ModFiveIndex = (new TimeSpan(sensor.datetime.Ticks - (new DateTime(2021, 1, 1, 0, 0, 0)).Ticks)).TotalSeconds % 5 };
        }
    }
}
