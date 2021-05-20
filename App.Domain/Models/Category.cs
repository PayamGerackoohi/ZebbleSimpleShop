using Domain.Database;
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
    [Table("category")]
    public class Category
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("is_root")]
        public bool IsRoot { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [Ignore]
        public List<Category> SubCategoryList { get; set; } = new();

        [ManyToMany(typeof(CategoryProduct))]
        [Column("product_list")]
        public List<Product> ProductList { get; set; }

        #endregion

        #region Public Methods

        public void PrepareDBWrite()
        {
            SubCategoryList?.Do(c =>
            {
                c.ParentId = Id;
                ShopDatabase.Instance.CategoryDao.SaveRaw(c);
            });
        }

        public void PrepareDBRead() => SubCategoryList = ShopDatabase.Instance.CategoryDao.ReadChildrenOf(Id);

        public override string ToString() => $"{Name} <{Id}>";

        public string PrintHierarchy(string indent = "") =>
            $"{(indent.None() ? "" : (indent + " "))}{this} -> {ParentId}\n" +
            SubCategoryList?.Select(c => c.PrintHierarchy(indent + "*")).ToString("");

        public override bool Equals(object obj) => obj != null && obj is Category o && o.Id == Id;

        public override int GetHashCode()
        {
            int hashCode = -156754813;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Category>>.Default.GetHashCode(SubCategoryList);
            return hashCode;
        }

        #endregion
    }

    [Table("category_product")]
    public class CategoryProduct
    {
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }
    }
}
