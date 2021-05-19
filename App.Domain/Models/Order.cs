using Domain.Utils;
using Olive;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Models
{
    [Table("order")]
    public class Order
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [Column("order_item_list")]
        public List<OrderItem> OrderItems { get; set; } = new();

        [Column("total_price")]
        public decimal TotalPrice { get => OrderItems?.Sum(o => o.Price) ?? 0; }

        [Column("time")]
        public DateTime Time { get; set; } = DateTime.Now;

        [Column("is_cart")]
        public bool IsCart { get; set; }

        [ForeignKey(typeof(OrderStatus))]
        [Column("status_fk")]
        public int StatusId { get; set; }

        [OneToOne]
        [Column("status")]
        public OrderStatus Status { get; set; } = new();

        [ForeignKey(typeof(User))]
        [Column("user_fk")]
        public int UserId { get; set; }

        //[ManyToOne]
        //[Column("user")]
        //public User User { get; set; }

        #endregion

        #region Public Methods

        public string FormattedTime() => Time.FormattedDate();

        public string FormattedTotalPrice() => TotalPrice.CurrencyFormat();

        public override string ToString() => new string[] {
            $"Id: {Id}",
            $"TotalPrice: {TotalPrice}",
            $"OrderItems: [{OrderItems?.Select(oi=>oi.ToString()).ToString(",")}]",
            $"Time: {Time}",
            $"IsCart: {IsCart}",
            $"Status: {Status}",
        }.ToString("\n");

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

        #endregion
    }

    [Table("order_item")]
    public class OrderItem
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey(typeof(Product))]
        [Column("product_fk")]
        public int ProductId { get; set; }

        [OneToOne]
        [Column("product")]
        public Product Product { get; set; } = new();

        [Column("count")]
        public int Count { get; set; } = 1;

        [Column("price")]
        public decimal Price { get => Count * Product.Price; }

        [ForeignKey(typeof(Order))]
        [Column("order_fk")]
        public int OrderId { get; set; }

        //[ManyToOne]
        //[Column("order")]
        //public Order Order { get; set; }

        #endregion

        public string FormattedPrice() => Price.CurrencyFormat();

        public override string ToString() => new string[] {
            $"Id: {Id}",
            $"ProductId: {ProductId}",
            $"Product: {Product?.Name}",
            $"Count: {Count}",
            $"Price: {Price}",
        }.ToString("; ");
    }

    [Table("order_status")]
    public class OrderStatus
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("status")]
        public Status Status { get; set; } = Status.InCart;

        [Column("description")]
        public string Description { get; set; } = "";

        public override string ToString() => "[" + new string[] {
            $"Id: {Id}",
            $"Status: {Status}",
            $"Description: {Description}",
        }.ToString("; ") + "]";

        #endregion
    }

    public enum Status { InCart, InProcessQue, Affirmed, Preparation, Sent, Delivered, Rejected }
}
