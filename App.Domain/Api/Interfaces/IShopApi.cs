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
        Task<List<Category>> GetFlatCategories();
        Task<Category> GetCategory(int id);
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProductsByCategory(int id);
        Task<List<Product>> GetProduct(string keyword);
        Task<List<Product>> SimilarProducts(Product product);
        Task<List<Product>> PopularProducts(int max = 30);
        Task<List<Product>> MostVisitedProducts(int max = 30);
        Task<List<Product>> OfferProducts(int max = 30);
        Task<List<Product>> NewProducts(int max = 30);
        Task<User> GetUser();
        Task Save(User user);
        Task AddFavorite(int productId);
        Task RemoveFavorite(int productId);
        Task SaveCart(Order cart);
        Task<Order> GetCart();
        Task AddToCart(Product product);
        Task RemoveFromCart(OrderItem order);
    }
}
