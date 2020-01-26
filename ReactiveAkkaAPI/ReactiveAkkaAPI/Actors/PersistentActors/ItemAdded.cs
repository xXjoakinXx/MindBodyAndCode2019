using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveAkkaAPI.Actors.PersistentActors
{
    public class ItemAdded
    {
        public ItemAdded(string itemId, int itemCount)
        {
            ItemId = itemId;
            ItemCount = itemCount;
        }

        public string ItemId { get; }
        public int ItemCount { get; }
    }
}
