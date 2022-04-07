using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class DeviceStatus
    {
        public string DeviceName { get; set; }
        public string ModelNo { get; set; }
        public string Status { get; set; }
        public double Temperature { get; set; }
        public double TonerStatus { get; set; }
        public double InkStatus { get; set; }
    }
}
