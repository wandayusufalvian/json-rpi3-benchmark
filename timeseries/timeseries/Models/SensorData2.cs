using System;
using System.Collections.Generic;
using System.Text;

namespace timeseries.Models
{
    public class SensorData2
    {
        public int id { get; set; }
        public DateTime datetime { get; set; }
        public List<double> sensorData { get; set; }

        public SensorData2()
        {
            
        }
    }
}
