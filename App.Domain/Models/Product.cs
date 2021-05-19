using Domain.Utils;
using Olive;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [Table("product")]
    public class Product
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [ManyToMany(typeof(CategoryProduct))]
        [Column("category_list")]
        public List<Category> Categories { get; set; } = new();

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("shortcription")]
        public string ShortCription { get; set; } = "";

        [Column("price")]
        public decimal Price { get; set; } = 0;

        [Column("image")]
        public byte[] Image { get; set; }

        [Column("thumbnail_image")]
        public byte[] ThumbnailImage { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; } = 0;

        [Column("votes")]
        public int Votes { get; set; } = 0;

        [Column("views")]
        public int Views { get; set; } = 0;

        [Column("sells")]
        public int Sells { get; set; } = 0;

        [Column("on_sale")]
        public DateTime OnSale { get; set; } = DateTime.Now;

        [ForeignKey(typeof(User))]
        [Column("user_fk")]
        public int UserId { get; set; }

        //[ManyToOne]
        //[Column("user")]
        //public User User { get; set; }

        #endregion

        #region Public Methods

        public string FormattedPrice() => Price.CurrencyFormat();

        public string RatingText() => string.Format("{0:0.#} / 5 of {1:#,##0} Votes", Rating, Votes);

        //public override bool Equals(object obj) => obj != null && obj is Product o && o.Id == Id;

        public override string ToString() => $"{Name}: ({Categories.ToString(", ")})";

        // todo: Equals-GetHashCode seems not working!
        //public override int GetHashCode() 
        //{
        //    int hashCode = -1190871373;
        //    hashCode = hashCode * -1521134295 + Id.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<List<Category>>.Default.GetHashCode(Categories);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShortCription);
        //    hashCode = hashCode * -1521134295 + Price.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(Image);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(ThumbnailImage);
        //    hashCode = hashCode * -1521134295 + Rating.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Views.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Sells.GetHashCode();
        //    hashCode = hashCode * -1521134295 + OnSale.GetHashCode();
        //    return hashCode;
        //}

        #endregion
    }
}
