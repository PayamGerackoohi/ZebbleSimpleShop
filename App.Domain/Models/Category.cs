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
    //[Table("id_holder")]
    //public class IntHolder
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int Key { get; set; }

    //    [Column("data")]
    //    public int data { get; set; }

    //    public static implicit operator int(IntHolder intHolder) => intHolder.data;
    //}

    [Table("category")]
    public class Category
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        // just supports 255 categories. Zebble with 100 of them would gladly fill memory on PC! So, seems enough :)
        //public byte Id { get; set; }
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("is_root")]
        public bool IsRoot { get; set; }

        //[OneToMany]
        //[Ignore]
        //public List<IntHolder> SubCategoryIdList { get; set; }
        //[Column("sub_category_id_list")]
        //public byte[] SubCategoryIdList { get; set; }

        //[ManyToMany(typeof(CategoryCategory))] // just provides self connection of a sub-category with itself!
        [Column("parent_id")]
        public int ParentId { get; set; }

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
            //SubCategoryIdList = SubCategoryList?.Select(c => c.Id).ToArray();
            SubCategoryList?.Do(c =>
            {
                c.ParentId = Id;
                ShopDatabase.Instance.CategoryDao.SaveRaw(c);
            });
        }

        public void PrepareDBRead()
        {
            //SubCategoryList = SubCategoryIdList?.Select(id => ShopDatabase.Instance.CategoryDao.ReadRaw(id).Set(c => c.PrepareDBRead()))?.ToList();
            SubCategoryList = ShopDatabase.Instance.CategoryDao.ReadChildrenOf(Id);
        }

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

    //[Table("category_category")]
    //public class CategoryCategory
    //{
    //    [ForeignKey(typeof(Category))]
    //    public int ParentId { get; set; }

    //    [ForeignKey(typeof(Category))]
    //    public int ChildId { get; set; }
    //}
}
