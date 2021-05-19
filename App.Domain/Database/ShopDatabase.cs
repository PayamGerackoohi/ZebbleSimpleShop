using Domain.Api.Fake;
using Domain.Database.Dao.Base;
using Domain.Database.Dao.Impl;
using Domain.Models;
using Domain.Utils;
using Olive;
using SQLite;
using System;

namespace Domain.Database
{
    public class ShopDatabase : SQLiteConnection
    {
        #region Properties

        public static ShopDatabase Instance { get; private set; }
        public IAddressDao AddressDao { get; private set; }
        public ICategoryDao CategoryDao { get; private set; }
        public ICredentialDao CredentialDao { get; private set; }
        public IOrderDao OrderDao { get; private set; }
        public IOrderStatusDao OrderStatusDao { get; private set; }
        public IOrderItemDao OrderItemDao { get; private set; }
        public IProductDao ProductDao { get; private set; }
        public IUserDao UserDao { get; private set; }

        #endregion

        #region Public Methods

        public static void Setup()
        {
            if (Instance != null)
                return;

            var db = new ShopDatabase();

            db.CreateTable<Address>();
            db.AddressDao = new AddressDaoImpl(db);

            db.CreateTable<Category>();
            db.CreateTable<CategoryProduct>();
            //db.CreateTable<CategoryCategory>();
            db.CategoryDao = new CategoryDaoImpl(db);

            db.CreateTable<Credential>();
            db.CredentialDao = new CredentialDaoImpl(db);

            db.CreateTable<Order>();
            db.OrderDao = new OrderDaoImpl(db);

            db.CreateTable<OrderStatus>();
            db.OrderStatusDao = new OrderStatusDaoImpl(db);

            db.CreateTable<OrderItem>();
            db.OrderItemDao = new OrderItemDaoImpl(db);

            db.CreateTable<Product>();
            db.ProductDao = new ProductDaoImpl(db);

            db.CreateTable<User>();
            db.UserDao = new UserDaoImpl(db);

            Instance = db;

            Init();
        }

        public ShopDatabase() : base(Constants.DatabasePath, Constants.Flags) { }

        #endregion

        #region Private Methods

        private static void Init()
        {
            SetFakeCategories();
            SetFakeProducts();
            SetFakeUser();
            SetFakeCart();
        }

        private static void SetFakeCategories()
        {
            if (Instance.CategoryDao.ReadAll().None())
                ShopApi.GenCategoryList(4);
        }

        private static void SetFakeProducts()
        {
            if (Instance.ProductDao.ReadAll().None())
                ShopApi.GenProducts();
        }

        private static void SetFakeCart()
        {
            if (Instance.OrderDao.GetCart() == null)
                ShopApi.GenCart();
        }

        private static void SetFakeUser()
        {
            if (Instance.UserDao.Read() == null)
                ShopApi.GenUser();
        }

        #endregion
    }
}
