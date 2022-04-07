using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class DeviceSensor
    {
        public string DeviceName { get; set; }
        public string ModelNo { get; set; }
        public string DeviceStatus { get; set; }
        public double MotorTemperature { get; set; }
        public double BlackCartridgePercentage { get; set; }
        public double CyanCartridgePercentage { get; set; }
        public double MagentaCartridgePercentage { get; set; }
        public double YellowCartridgePercentage { get; set; }
    }
}
