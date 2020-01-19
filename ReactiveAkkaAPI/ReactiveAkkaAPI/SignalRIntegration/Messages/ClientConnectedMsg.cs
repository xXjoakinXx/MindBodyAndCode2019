using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.SignalRIntegration.Messages
{
    public class ClientConnectedMsg
    {
        private readonly string _connectionId;

        public string ConnectionId => _connectionId;

        public ClientConnectedMsg(string connectionId)
        {
            _connectionId = connectionId;
        }
    }
}
