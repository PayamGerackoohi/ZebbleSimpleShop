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
        // just supports 255 categories. Zebble with 100 of them would gladly fill memory on PC! So, seems enough :)
        public byte Id { get; set; }
        //public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("is_root")]
        public bool IsRoot { get; set; }

        [Column("sub_category_id_list")]
        public byte[] SubCategoryIdList { get; set; }
        //public int[] SubCategoryIdList { get; set; }

        //[ManyToMany(typeof(CategoryCategory))] // just provides self connection of a sub-category with itself!
        [Ignore]
        public List<Category> SubCategoryList { get; set; } = new();

        //[ForeignKey(typeof(Product))]
        //[Column("product_fk")]
        //public int ProductId { get; set; }

        [ManyToMany(typeof(CategoryProduct))]
        [Column("product_list")]
        public List<Product> ProductList { get; set; }

        //[ForeignKey(typeof(Product))]
        //[Column("product_fk")]
        //public int ProductId { get; set; }

        //[ManyToOne]
        //[Column("product")]
        //public Product Product { get; set; }

        #endregion

        #region Public Methods

        public void PrepareDBWrite()
        {
            SubCategoryIdList = SubCategoryList?.Select(c => c.Id).ToArray();
        }

        public void PrepareDBRead()
        {
            SubCategoryList = SubCategoryIdList?.Select(id => ShopDatabase.Instance.CategoryDao.ReadRaw(id).Set(c => c.PrepareDBRead()))?.ToList();
        }

        public override bool Equals(object obj) => obj != null && obj is Category o && o.Id == Id;

        public override string ToString() => $"{Name} <{Id}>";

        //public string Log(string indent) => $"{indent} {Name}:\n{indent} {SubCategoryList?.Count()}";
        //public string Log(string indent = "") => $"{indent} {Name}:\n" + SubCategoryList?.Select(c => c.Log(indent + "*").ToString("\n"));
        public string Log(string indent = "") => $"{indent} {Name}\n" + SubCategoryList?.Select(c => c.Log(indent + "*")).ToString("\n");

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

    //[Table("category_category")]
    //public class CategoryCategory
    //{
    //    [ForeignKey(typeof(Category))]
    //    public int ParentId { get; set; }

    //    [ForeignKey(typeof(Category))]
    //    public int ChildId { get; set; }
    //}
}
