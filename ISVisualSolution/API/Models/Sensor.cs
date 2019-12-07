using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public int Battery { get; set; }
        public long Timestamp { get; set; }

    }
}