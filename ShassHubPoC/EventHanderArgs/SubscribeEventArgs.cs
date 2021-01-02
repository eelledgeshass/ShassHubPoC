using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShassHubPoC.EventHanderArgs
{
    public class SubscribeEventArgs
    {
        public string ClientID { get; set; }
        public string Topic { get; set; }
    }
}
