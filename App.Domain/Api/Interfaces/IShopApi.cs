using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api.Interfaces
{
    interface IShopApi
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(Guid id);
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetProductsByCategory(Guid id);
        Task<List<Product>> GetProduct(string keyword);
        Task<List<Product>> SimilarProducts(Product product);
        Task<List<Product>> PopularProducts(int max = 30);
        Task<List<Product>> MostVisitedProducts(int max = 30);
        Task<List<Product>> OfferProducts(int max = 30);
        Task<List<Product>> NewProducts(int max = 30);
        Task<User> GetUser();
        Task SaveUser(User user);
        Task AddFavorite(Guid productId);
        Task RemoveFavorite(Guid productId);
        Task<Order> GetCart();
    }
}
