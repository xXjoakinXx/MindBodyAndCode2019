using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.Messages
{
    public class AuthenticatedMessageEnvelope
    {
        private readonly object _payload;
        private readonly IActorRef _targetActor;
        private readonly string _authenticationToken;

        public object Payload => _payload; 
        public IActorRef TargetActor => _targetActor;
        public string AuthenticationToken => _authenticationToken;

        public AuthenticatedMessageEnvelope(object payload, IActorRef targetActor, string authenticationToken)
        {
            _payload = payload;
            _targetActor = targetActor;
            _authenticationToken = authenticationToken;
        }


    }
}
