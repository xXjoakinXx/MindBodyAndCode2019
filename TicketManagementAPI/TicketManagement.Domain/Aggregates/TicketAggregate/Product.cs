using System;
using TicketManagement.Domain.SeedWork;

namespace TicketManagement.Domain.Aggregates.TicketAggregate
{
    public class Product : Entity<Guid>
    {
        internal Product(string name, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            SetPrice(price);
        }

        public string Name { get; private set; }

        public double Price { get; private set; }
        public double Discount { get; private set; }
        public double FinalPrice => Discount > 0 && Discount < 1 ? Discount * Price : Price;


        public void SetPrice(double price)
        {
            if (price <= 0) throw new TicketException("I can't create a product with a price less than zero");

            Price = price;
        }
            
        public void ApplyDiscount(double discount)
        {
            Discount = discount;
        }

    }
}
