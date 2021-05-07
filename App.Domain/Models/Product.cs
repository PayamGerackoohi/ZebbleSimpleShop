using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    class Product
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public string Name { get; set; } = "";
        public List<Category> Categories { get; set; }
        public string Description { get; set; }
        public string ShortCription { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public byte[] ThumbnailImage { get; set; }
        public decimal Rating { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int Sells { get; set; }
        public DateTime OnSale { get; set; }

        public string FormattedPrice() => Price.CurrencyFormat();

        public string RatingText() => string.Format("{0:0.#} / 5 of {1:#,##0} Votes", Rating, Votes);
        //public string RatingText() => $"{Rate}/5 {Votes} Votes";

        public override bool Equals(object obj) => obj != null && obj is Product o && o.Id == Id;

        public override string ToString() => $"{Name}: ({Categories.ToString(", ")})";

        public override int GetHashCode()
        {
            int hashCode = -1190871373;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Category>>.Default.GetHashCode(Categories);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShortCription);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(ThumbnailImage);
            hashCode = hashCode * -1521134295 + Rating.GetHashCode();
            hashCode = hashCode * -1521134295 + Views.GetHashCode();
            hashCode = hashCode * -1521134295 + Sells.GetHashCode();
            hashCode = hashCode * -1521134295 + OnSale.GetHashCode();
            return hashCode;
        }
    }
}
