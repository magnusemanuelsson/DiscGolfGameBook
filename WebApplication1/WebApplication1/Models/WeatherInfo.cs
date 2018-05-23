using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class WeatherInfo
    {
        public class Geometry
        {
            public string type { get; set; }
            public List<List<double>> coordinates { get; set; }
        }

        public class Parameter
        {
            public string name { get; set; }
            public string levelType { get; set; }
            public int level { get; set; }
            public string unit { get; set; }
            public List<double> values { get; set; }
        }

        public class TimeSery
        {
            public DateTime validTime { get; set; }
            public List<Parameter> parameters { get; set; }
        }

        public class RootObject
        {
            public DateTime approvedTime { get; set; }
            public DateTime referenceTime { get; set; }
            public Geometry geometry { get; set; }
            public List<TimeSery> timeSeries { get; set; }
        }
    }
}