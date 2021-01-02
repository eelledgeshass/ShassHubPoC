using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShassHubPoC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShassHubPoC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMqttClientService _mqttClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MqttClientServiceProvider mqttProvider)
        {
            _logger = logger;

            _mqttClient = mqttProvider.MqttClientService;
            _mqttClient.PublishedTopic += _mqttClient_PublishedTopic;
            _mqttClient.SubscribededTopic += _mqttClient_SubscribededTopic;
        }

        private void _mqttClient_SubscribededTopic(object sender, EventHanderArgs.SubscribeEventArgs e)
        {
            Console.WriteLine($"{e.ClientID} Subscribed to Topic {e.Topic}");
        }

        private void _mqttClient_PublishedTopic(object sender, EventHanderArgs.PublishEventArgs e)
        {
            Console.WriteLine($"Topic Published: {e.Topic} with Payload {e.Payload}");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("Subscribe")]
        public ActionResult SubscribeTopic(string topic)
        {
            _mqttClient.Subscribe(topic);

            return Ok();
        }

        [HttpPut]
        [Route("Publish")]
        public ActionResult PublichTopic(string topic, string payload)
        {
            _mqttClient.Publish(topic, payload);

            return Ok();
        }
    }
}
