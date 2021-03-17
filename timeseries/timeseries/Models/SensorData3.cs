using System;
using System.Collections.Generic;
using System.Text;

namespace timeseries.Models
{
    public class SensorData3
    {
        public double ModFive { get; set; }
        public double ModTen { get; set; }
        public DateTime datetime { get; set; }
        public List<double> sensorData { get; set; }

    }

       
}
