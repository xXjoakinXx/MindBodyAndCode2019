using System;

namespace TicketManagement.Domain.Aggregates.TicketAggregate
{
    public class TicketException : Exception
    {
        public TicketException(string message) : base(message)
        {
        }
    }
}
