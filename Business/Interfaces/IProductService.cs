using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProductService
{
    Task<bool> CreateProductAsync(ProductRegistrationForm form);
    Task<IEnumerable<Product?>> GetAllProductsAsync();
    Task<Product?> GetProductAsync(int id);
    Task<Product?> GetProductAsync(string productName);
    Task<bool> UpdateProductAsync(ProductUpdateForm form);
    Task<bool> DeleteProductAsync(int id);
}