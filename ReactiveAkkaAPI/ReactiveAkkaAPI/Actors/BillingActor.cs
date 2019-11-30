using Akka.Actor;
using ReactiveAkkaAPI.Messages;

namespace ReactiveAkkaAPI.Actors
{
    public class BillingActor : ReceiveActor
    {
        public BillingActor()
        {
            Receive<VocalGreeting>(_ =>
            {
                Sender.Tell(AckMessage.Instance); // Responde al enviante diciendo que ha recivido el mensaje correctamente
            });
        }
    }
}
