using ShoppingApp.API.Models;

namespace ShoppingApp.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<int> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);
    }
}
