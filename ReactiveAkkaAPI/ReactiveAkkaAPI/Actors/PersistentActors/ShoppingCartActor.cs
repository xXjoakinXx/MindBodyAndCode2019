using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.Actors.PersistentActors
{
    public class ShoppingCartActor : PersistentActor
    {
        private Dictionary<string, int> _items;

        private int _eventsProcessed = 0;

        public override string PersistenceId => "shoppingcart";

        protected override bool ReceiveCommand(object message)
        {
            if(message is AddItem)
            {
                var msg = (AddItem)message;
                var itemAddedEvent = new ItemAdded(msg.ItemId, msg.ItemCount);
                PersistAsync(itemAddedEvent, HandleEvent); // Cuidado si necesitas que la operación se complete con Async es un Fire-and-forget
                return true;
            }

            return false;
        }

        private void HandleEvent(object @event)
        {
            if(@event is ItemAdded)
            {
                var evt = (ItemAdded)@event;
                if (_items.ContainsKey(evt.ItemId))
                {
                    var currentCount = _items[evt.ItemId];
                    var newCount = currentCount + evt.ItemCount;
                    _items[evt.ItemId] = newCount;
                }
                else
                {
                    _items[evt.ItemId] = evt.ItemCount;
                }

                _eventsProcessed++;
            }

            if(_eventsProcessed > 10)
            {
                SaveSnapshot(_items); // Guardar snapshot cada 10 eventos recividos para así no saturar en los recovery
                _eventsProcessed = 0;
            }
        }

        protected override bool ReceiveRecover(object message)
        {
            if(message is SnapshotOffer) // Recuperación a partir del último Snapshot
            {
                var snapshot = (SnapshotOffer)message;
                _items = (Dictionary<string, int>)snapshot.Snapshot;
                return true;
            }

            if(message is ItemAdded)
            {
                HandleEvent(message); // Recover state from Journal individually
                return true;
            }

            return false;
        }
    }
}
