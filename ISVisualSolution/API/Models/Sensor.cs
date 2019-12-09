    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Sensor
    {
       

        public Dictionary<string, string> SensorType { get; set; }
        public int Id { get; set; }
        public int Battery { get; set; }
        public long Timestamp { get; set; }

        public static implicit operator List<object>(Sensor v)
        {
            throw new NotImplementedException();
        }
    }
}