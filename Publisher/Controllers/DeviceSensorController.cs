using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Publisher.Models;

namespace Publisher.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceSensorController :ControllerBase
    {
        [HttpPost("Post")]
        public void Post([FromBody] DeviceSensor sensor)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "deviceStatusSampleQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = String.Format("Device Name : {0} , ModelNo : {1} , DeviceStatus :{2} , MotorTemperature :{3}, BlackCartridgePercentage :{4},CyanCartridgePercentage :{5}, MagentaCartridgePercentage :{6}, YellowCartridgePercentage :{7}", sensor.DeviceName, sensor.ModelNo, sensor.DeviceStatus, sensor.MotorTemperature, sensor.BlackCartridgePercentage, sensor.CyanCartridgePercentage, sensor.MagentaCartridgePercentage, sensor.YellowCartridgePercentage);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "deviceStatusSampleQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
