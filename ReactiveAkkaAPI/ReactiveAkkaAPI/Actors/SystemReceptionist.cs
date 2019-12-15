using Akka.Actor;
using ReactiveAkkaAPI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.Actors
{
    public class SystemReceptionist : ReceiveActor
    {
        public SystemReceptionist()
        {
            Receive<AuthenticatedMessageEnvelope>(env =>
            {
                if (IsAuthorised(env.AuthenticationToken)) // Se valida el token y si es valido...
                {
                    env.TargetActor.Forward(env.Payload); // Forward del payload del mensaje. Es decir se manda el mensaje en si 
                                                         // la diferencia con el Send es que el emisor es el originario. Para el receptor
                                                         // este actor SystemReceptionist es totalmente desconocido.
                }
            });
        }

        private bool IsAuthorised(string authToken)
        {
            // Code to authorizate token
            return true;
        }
    }
}
