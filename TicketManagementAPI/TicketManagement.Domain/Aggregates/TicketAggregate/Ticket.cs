using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagement.Domain.SeedWork;

namespace TicketManagement.Domain.Aggregates.TicketAggregate
{
    public class Ticket : Entity<Guid>, IAggregateRoot
    {
        public int TicketNumber { get; private set; }

        public double CalculatedTotalPrice => GlobalDiscount > 0 && GlobalDiscount < 1 ? TotalPrice * GlobalDiscount : TotalPrice;
        public double GlobalDiscount { get; private set; }
        public double TotalPrice => Products.Sum(p => p.FinalPrice);
        public double CashReceived { get; private set; }
        public double Change { get; private set; }

        public TicketState Status { get; private set; }

        public ICollection<Product> Products { get; private set; }

        internal Ticket()
        {
            Id = Guid.NewGuid();
            Status = TicketState.PENDING;
            Products = new List<Product>();
        }

        public void ApplyDiscountInAllProducts(double discount)
        {

            if (Status != TicketState.BUILDING)
                throw new TicketException("Ticket must be in BUILDING state to admint discounts");

            if (discount <= 0)
                throw new TicketException("A discount of less than zero cannot be applied"); 

            if (discount >= 1)
                throw new TicketException("The discount must be a factor between 0 and 1"); 

            foreach (var product in Products)
            {
                product.ApplyDiscount(discount);
            }
        }

        public void ApplyDiscount(double discount)
        {
            if (Status != TicketState.BUILDING)
                throw new TicketException("Ticket must be in BUILDING state to admint discounts");

            if (discount <= 0)
                throw new TicketException("A discount of less than zero cannot be applied");

            if (discount >= 1)
                throw new TicketException("The discount must be a factor between 0 and 1");

            GlobalDiscount = discount;
        }

        public void PayTicket(double cash)
        {
            if (Status != TicketState.PENDING)
                throw new TicketException("Ticket must be in PENDING state to admin paiments");

            if (cash >= CalculatedTotalPrice)
            {
                CashReceived += cash;
                if (CashReceived >= CalculatedTotalPrice)
                {
                    Change = CashReceived - CalculatedTotalPrice;
                    Status = TicketState.PAID;
                }
            }
        }

        public void AddProduct(string name, double price, double discount)
        {
            if (Status != TicketState.BUILDING)
                throw new TicketException("Ticket must be in BUILDING state to add new products");

            var product = new Product(name, price);

            Products.Add(product);
        }

        public void RemoveProduct(Guid productId)
        {
            if (Status != TicketState.BUILDING)
                throw new TicketException("Ticket must be in BUILDING state to remove products");

            var product = Products.FirstOrDefault(p => p.Id == productId);
            Products.Remove(product);
        }
    }
}
