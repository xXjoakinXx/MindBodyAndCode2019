using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.SignalRIntegration.Messages
{
    public class SendMessageMsg
    {
        private readonly string _connectionId;
        private readonly string _data;

        public string ConnectionId => _connectionId;
        public string Data => _data;

        public SendMessageMsg(string connectionId, string data)
        {
            _connectionId = connectionId;
            _data = data;
        }

        
    }
}
