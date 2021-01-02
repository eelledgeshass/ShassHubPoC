using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Receiving;
using ShassHubPoC.EventHanderArgs;
using System;
using System.Threading.Tasks;

namespace ShassHubPoC.Services
{
    public interface IMqttClientService : IHostedService,
                                          IMqttClientConnectedHandler,
                                          IMqttClientDisconnectedHandler,
                                          IMqttApplicationMessageReceivedHandler
    {
        event EventHandler<PublishEventArgs> PublishedTopic;
        event EventHandler<SubscribeEventArgs> SubscribededTopic;
        Task Publish(string topic, string payload);
        Task Subscribe(string tpoic);

    }
}
