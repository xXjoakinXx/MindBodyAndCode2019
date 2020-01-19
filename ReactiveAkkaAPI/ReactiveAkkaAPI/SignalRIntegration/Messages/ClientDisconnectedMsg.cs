using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.SignalRIntegration.Messages
{
    public class ClientDisconnectedMsg
    {
        private readonly string _connectionId;

        public string ConnectionId => _connectionId;

        public ClientDisconnectedMsg(string connectionId)
        {
            _connectionId = connectionId;
        }
    }
}
