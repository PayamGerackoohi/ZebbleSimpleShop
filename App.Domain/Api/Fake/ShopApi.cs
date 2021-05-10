using Domain.Api.Interfaces;
using Domain.Models;
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
        private static readonly List<Category> SubCategories = new();
        private static readonly List<Category> Categories = GenCategoryList(6);
        private static readonly List<Product> Products = GenProducts();
        private static User User = GenUser();
        private static Order Cart = GenOrder();

        public Task<Category> GetCategory(Guid id) => Categories.Fetch(c => c.Id == id);

        public Task<List<Product>> SimilarProducts(Product product) => product.Categories.SelectMany(c => GetProductsByCategorySync(c.Id)).AsListResult();

        public Task<Product> GetProduct(Guid id) => Products.Fetch(p => p.Id == id);

        public Task<List<Product>> GetProduct(string keyword) => Products.Query(p => p.Name.Remove(" ").Contains(keyword, false) || p.Categories.Any(c => c.Name.Remove(" ").Contains(keyword, false)));

        public Task<List<Product>> GetProducts() => Products.AsResult();

        public Task<List<Product>> GetProductsByCategory(Guid id) => GetProductsByCategorySync(id).AsResult();

        private List<Product> GetProductsByCategorySync(Guid id) => Products.Where(p => p.Categories.Any(c => c.Id == id)).ToList();

        public Task<List<Category>> GetCategories() => Categories.AsResult();

        public Task<List<Product>> PopularProducts(int max = MaxQueryResults) => Products
            .OrderByDescending(p => p.Sells)
            .ThenByDescending(p => p.Rating)
            .ThenByDescending(p => p.Views)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> MostVisitedProducts(int max = MaxQueryResults) => Products
            .OrderByDescending(p => p.Views)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> OfferProducts(int max = MaxQueryResults) => Products
            .OrderByDescending(p => p.Views)
            .ThenBy(p => p.Sells)
            .Take(max)
            .AsListResult();

        public Task<List<Product>> NewProducts(int max = MaxQueryResults) => Products
            .OrderByDescending(p => p.OnSale)
            .Take(max)
            .AsListResult();

        public Task<User> GetUser() => User.Copy().AsResult();

        public Task SaveUser(User user)
        {
            User = user;
            return Task.CompletedTask;
        }

        public Task RemoveFavorite(Guid productId)
        {
            User.Favorites.RemoveWhere(f => f.Id == productId);
            return Task.CompletedTask;
        }

        public Task AddFavorite(Guid productId)
        {
            if (!User.Favorites.Any(f => f.Id == productId))
                User.Favorites.Add(Products.Find(p => p.Id == productId));
            return Task.CompletedTask;
        }

        public Task<Order> GetCart() => Cart.AsResult();

        private static User GenUser() => new()
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
            Orders = GenOrderList(3),
            Favorites = Products.Take(3).ToList(),
            Credential = new Credential
            {
                Username = "PayamGr",
                Password = "123321",
                StayLoggedIn = true,
            },
        };

        private static List<Product> GenProducts() => Enumerable.Range(1, SubCategories.Count).Select(n => new Product
        {
            Name = $"Product {n}",
            Categories = Enumerable.Range(0, 1 + (n % SubCategories.Count)).Select(m => SubCategories[m]).ToList(),
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
        }).ToList();

        private static List<Order> GenOrderList(int count, int min = 1, int max = 4) => Enumerable.Range(1, count).Select(n => GenOrder(n, min: min, max: max)).ToList();

        private static Order GenOrder(int n = 1, int min = 1, int max = 4) => new()
        {
            Time = DateTime.Now.AddDays(-n),
            Status = new OrderStatus { Status = Status.Delivered, Description = $"The product is delivered at { DateTime.Now.AddDays(2 - n)}." },
            OrderItems = Enumerable.Range(1, new Random().Next(min, max)).Select(
               m => new OrderItem
               {
                   Product = Products[m],
                   Count = new Random().Next(1, m)
               }).ToList(),
        };

        private static List<Category> GenCategoryList(int seed, string head = "") => seed < 0 ? default :
            Enumerable.Range(1, seed).Select(
                n => new Category
                {
                    Name = $"Category {head}{n}",
                    Categories = GenCategoryList(new Random().Next(seed - 3, seed), $"{head}{n}."),
                }.Set(c => { if (c.Categories.None()) SubCategories.Add(c); })
            ).ToList();
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
