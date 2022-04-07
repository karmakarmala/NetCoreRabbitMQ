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
    public class DeviceStatusController :ControllerBase
    {
        [HttpPost("Post")]
        public void Post([FromBody] DeviceStatus device)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "deviceStatus",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = String.Format("Device Name : {0} , ModelNo : {1} , Status :{2} , Temperature :{3}, Ink Status :{4},Toner Status :{5}", 
                                            device.DeviceName, 
                                            device.ModelNo, 
                                            device.Status, 
                                            device.Temperature, 
                                            device.InkStatus, 
                                            device.TonerStatus);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "deviceStatus",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
