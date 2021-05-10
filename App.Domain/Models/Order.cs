using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Models
{
    class Order
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public List<OrderItem> OrderItems { get; set; } = new();
        public decimal TotalPrice { get => OrderItems.Sum(o => o.Price); }
        public DateTime Time { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = new();

        public string FormattedTime() => Time.FormattedDate();

        public string FormattedTotalPrice() => TotalPrice.CurrencyFormat();

        public override bool Equals(object obj) => obj != null && obj is Order o && o.Id == Id;

        public override int GetHashCode()
        {
            int hashCode = 1475629190;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderItem>>.Default.GetHashCode(OrderItems);
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<OrderStatus>.Default.GetHashCode(Status);
            return hashCode;
        }
    }

    class OrderItem
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public Product Product { get; set; } = new();
        public int Count { get; set; } = 1;
        public decimal Price { get => Count * Product.Price; }

        public string FormattedPrice() => Price.CurrencyFormat();
    }

    class OrderStatus
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public Status Status { get; set; } = Status.InCart;
        public string Description { get; set; } = "";
    }

    public enum Status { InCart, InProcessQue, Affirmed, Preparation, Sent, Delivered, Rejected }
}
