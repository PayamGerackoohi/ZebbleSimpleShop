using Domain.Api.Interfaces;
using Domain.Database;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Fake
{
    class ShopApi : IShopApi
    {
        private const int MaxQueryResults = 30;

        public Task<Category> GetCategory(int id) => ShopDatabase.Instance.CategoryDao.ReadAll().Fetch(c => c.Id == id);

        public Task<List<Product>> SimilarProducts(Product product) => product.Categories.SelectMany(c => GetProductsByCategorySync(c.Id)).AsListResult();

        private static List<Product> GetPersistedProducts() => ShopDatabase.Instance.ProductDao.ReadAll();

        public Task<Product> GetProduct(int id) => GetPersistedProducts().Fetch(p => p.Id == id);

        public Task<List<Product>> GetProduct(string keyword) => GetPersistedProducts().Query(p => p.Name.Remove(" ").Contains(keyword, false) || p.Categories.Any(c => c.Name.Remove(" ").Contains(keyword, false)));

        public Task<List<Product>> GetProducts() => GetPersistedProducts().AsResult();

        public Task<List<Product>> GetProductsByCategory(int id) => GetProductsByCategorySync(id).AsResult();

        private List<Product> GetProductsByCategorySync(int id) => GetPersistedProducts().Where(p => p.Categories.Any(c => c.Id == id)).ToList();

        public Task<List<Category>> GetCategories() => ShopDatabase.Instance.CategoryDao.ReadAll().AsResult();

        public Task<List<Category>> GetFlatCategories() => ShopDatabase.Instance.CategoryDao.ReadAllRaw().AsResult();

        public Task<List<Product>> PopularProducts(int max = MaxQueryResults) => GetPersistedProducts()
            .OrderByDescending(p => p.Sells)
            .ThenByDescending(p => p.Rating)
            .ThenByDescending(p => p.Views)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> MostVisitedProducts(int max = MaxQueryResults) => GetPersistedProducts()
            .OrderByDescending(p => p.Views)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> OfferProducts(int max = MaxQueryResults) => GetPersistedProducts()
            .OrderByDescending(p => p.Views)
            .ThenBy(p => p.Sells)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> NewProducts(int max = MaxQueryResults) => GetPersistedProducts()
            .OrderByDescending(p => p.OnSale)
            .Take(max)
            .AsListResult();

        public async Task<User> GetUser() => ShopDatabase.Instance.UserDao.Read();

        public async Task Save(User user) => ShopDatabase.Instance.UserDao.Save(user);

        public async Task RemoveFavorite(int productId)
        {
            var user = await GetUser();
            user.Favorites.RemoveWhere(f => f.Id == productId);
            await Save(user);
        }

        public async Task AddFavorite(int productId)
        {
            var user = await GetUser();
            if (!user.Favorites.Any(f => f.Id == productId))
                user.Favorites.Add(GetPersistedProducts().Find(p => p.Id == productId));
            await Save(user);
        }

        public async Task SaveCart(Order cart) => ShopDatabase.Instance.OrderDao.Save(cart);

        public Task<Order> GetCart() => ShopDatabase.Instance.OrderDao.GetCart().AsResult();

        public static Order GenCart() => GenOrder(isCart: true);

        public static User GenUser() => new User()
        {
            FirstName = "Payam",
            LastName = "Gerackoohi",
            Address = new Address
            {
                Country = "Iran",
                State = "Gilan",
                City = "Rasht",
                StreetAddress = "Goslar, Navid St., Farid Alley, Vahid Bldg., 1. floor",
                ZipCode = "123456"
            },
            Email = "payam.gerackoohi@geeks.ltd.uk",
            PhoneNumber = "+989225834537",
            Gender = Gender.Male,
            BirthDate = new DateTime(1991, 10, 14),
            Favorites = GetPersistedProducts().Take(3).ToList(),
            Orders = GenOrderList(3),
            Credential = new Credential
            {
                Username = "PayamGr",
                Password = "123321",
            },
        }.Also(user => ShopDatabase.Instance.UserDao.Save(user));

        public static List<Product> GenProducts() => ShopDatabase.Instance.CategoryDao.GetSubCategories()
            .Let(subCategories => Enumerable.Range(1, subCategories.Count).Select(n => new Product
            {
                Name = $"Product {n}",
                Categories = Enumerable.Range(0, 1 + (n % subCategories.Count)).Select(m => subCategories[m]).ToList(),
                Description = $"Description {n} ".Repeat(20),
                ShortCription = $"ShortCription {n} ".Repeat(3),
                Price = 1234.5678M * (1 + n % 7),
                //public byte[] Image { get; set; }
                //public byte[] ThumbnailImage { get; set; }
                Rating = (decimal)(1 + 4 * new Random().NextDouble()),
                Votes = 1234 * (1 + n % 6),
                Views = 100 * (n % 6),
                Sells = 10 * (n % 8),
                OnSale = DateTime.Now.AddDays(-(n % 4)),
            }.Also(p => ShopDatabase.Instance.ProductDao.Save(p))))
            .ToList();

        private static List<Order> GenOrderList(int count, int min = 1, int max = 4) => Enumerable.Range(1, count).Select(n => GenOrder(n, min: min, max: max)).ToList();

        private static Order GenOrder(int n = 1, int min = 1, int max = 4, bool isCart = false) => new Order()
        {
            IsCart = isCart,
            Time = DateTime.Now.AddDays(-n),
            Status = new OrderStatus { Status = Status.Delivered, Description = $"The product is delivered at { DateTime.Now.AddDays(2 - n)}." },
            OrderItems = Enumerable.Range(1, new Random().Next(min, max)).Select(
               m => new OrderItem
               {
                   Product = GetPersistedProducts()[m],
                   Count = new Random().Next(1, m)
               }.Also(oi => ShopDatabase.Instance.OrderItemDao.Save(oi))
            ).ToList(),
        }.Also(o => ShopDatabase.Instance.OrderDao.Save(o));

        public static List<Category> GenCategoryList(int seed, string head = "") => seed < 0 ? default :
            Enumerable.Range(1, seed).Select(
                n => new Category
                {
                    Name = $"Category {head}{n}",
                    SubCategoryList = GenCategoryList(new Random().Next(seed - 3, seed), $"{head}{n}."),
                    IsRoot = head == ""
                }.Also(cat => ShopDatabase.Instance.CategoryDao.Save(cat))
            ).ToList();

        public async Task AddToCart(Product product) => ShopDatabase.Instance.OrderDao.AddToCart(product);

        public async Task RemoveFromCart(OrderItem order) => ShopDatabase.Instance.OrderDao.RemoveFromCart(order);
    }

    static class ApiExt
    {
        public static Task<List<T>> Query<T>(this List<T> table, Func<T, bool> criteria = null)
           => table.Where(v => criteria == null || criteria(v)).AsListResult();

        public static Task<T> Fetch<T>(this List<T> table, Func<T, bool> criteria = null)
            => table.FirstOrDefault(v => criteria == null || criteria(v)).AsResult();

        public static Task<T> AsResult<T>(this T self) => Task.FromResult(self);

        public static Task<List<T>> AsListResult<T>(this IEnumerable<T> self) => self.ToList().AsResult();
    }
}
