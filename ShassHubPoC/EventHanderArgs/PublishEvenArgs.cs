using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShassHubPoC.EventHanderArgs
{
    public class PublishEventArgs : EventArgs
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
    }
}
