using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProductService
{
    Task<bool> CreateProductAsync(ProductRegistrationForm form);
    Task<IEnumerable<Product?>> GetAllProductsAsync();
    Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<bool> UpdateProductAsync(ProductUpdateForm form);
    Task<bool> DeleteProductAsync(int id);
}