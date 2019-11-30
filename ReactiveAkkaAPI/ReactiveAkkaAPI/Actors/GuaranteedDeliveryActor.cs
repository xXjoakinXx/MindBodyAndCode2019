using Akka.Actor;
using ReactiveAkkaAPI.Messages;
using System;

namespace ReactiveAkkaAPI.Actors
{
    public class GuaranteedDeliveryActor : ReceiveActor
    {
        readonly ActorSelection _target; // La diferencia con IActorRef es que éste asegura que es el mismo actor
                                         // Con IActorRef en caso de fallo el Actor puede ser re-creado
        readonly object _message;
        readonly int _maxRetries;
        int _retryCount;
        readonly TimeSpan _messageTimeout;

        public GuaranteedDeliveryActor(ActorSelection actorSelection, object message, int maxRetries,
            TimeSpan messageTimeout)
        {
            _target = actorSelection;
            _message = message;
            _maxRetries = maxRetries;
            _messageTimeout = messageTimeout;

            Receive<ReceiveTimeout>(_ =>
            {
                // Circuit breaker implementation
                if (_retryCount >= _maxRetries)
                    throw new TimeoutException("Unable to deliver the message to the target in the specified " +
                        "number of retires");
                else
                {
                    _target.Tell(_message);
                    _retryCount++;
                }
            });

            Receive<AckMessage>(_ =>
            {
                SetReceiveTimeout(null); // Cancela la recepción de ReceiveTimeout message después de x tiempo
                Context.Stop(Self); // Matamos al actor puesto que el receptor del mensaje le ha contestado correctamente
                                    // y su función termina 
            });
        }

        protected override void PreStart()
        {
            SetReceiveTimeout(_messageTimeout); // Despues del tiempo indicado recivirá un ReceiveTimeoutMessage
            _target.Tell(_message);
        }


    }
}
