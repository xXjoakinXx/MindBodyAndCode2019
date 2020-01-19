using Akka.Actor;
using ReactiveAkkaAPI.SignalRIntegration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.SignalRIntegration.Actors
{
    public class WebsocketActor : ReceiveActor
    {
        public WebsocketActor()
        {
            Receive<MessageReceivedMsg>(msg =>
            {
                //Funcionalidad específica de la aplicación al recivir mensajes del websocker (SignalR)

            });

            Receive<ClientConnectedMsg>(msg =>
            {
                // Funcionalidad específica para manejar la conexión de nuevos clientes
            });

            Receive<ClientDisconnectedMsg>(msg =>
            {
                // Funcionalidad específica para manejar la desconexión de los clientes
            });
        }
    }
}
