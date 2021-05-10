using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Category
    {
        private static int GlobalId = 0;
        public Guid Id { get; private set; } = (++GlobalId).ToGuid();
        public string Name { get; set; } = "";
        public List<Category> Categories { get; set; } = new();

        public override bool Equals(object obj) => obj != null && obj is Category o && o.Id == Id;

        public override string ToString() => $"{Name} <{Id}>";

        public override int GetHashCode()
        {
            int hashCode = -156754813;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Category>>.Default.GetHashCode(Categories);
            return hashCode;
        }
    }
}
