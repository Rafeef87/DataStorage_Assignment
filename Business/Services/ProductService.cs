using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    //CREATE
    public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
    {
        if (await _productRepository.AlreadyExistsAsync(p => p.ProductName == form.ProductName))
            return false;
        await _productRepository.BeginTransactionAsync();
        try
        {
            await _productRepository.AddAsync(new ProductEntity { ProductName = form.ProductName });
            await _productRepository.SaveAsync();
            await _productRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //READ
    public async Task<IEnumerable<Product?>> GetAllProductsAsync()
    {
        var entties = await _productRepository.GetAllAsync();
        var products = entties.Select(ProductFactory.Create);
        return products;
    }
    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {

        var enttiy = await _productRepository.GetAsync(expression);
        var product= ProductFactory.Create(enttiy!);
        return product ?? null!;
    }
    //UPDATE
    public async Task<bool> UpdateProductAsync(ProductUpdateForm form)
    {
        if (await _productRepository.AlreadyExistsAsync(p => p.ProductName == form.ProductName))
            return false;
        await _productRepository.BeginTransactionAsync();
        try
        {
            _productRepository.Update(new ProductEntity { ProductName = form.ProductName });
            await _productRepository.SaveAsync();
            await _productRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //DELETE
    public async Task<bool> DeleteProductAsync(int id)
    {
        var entity = await _productRepository.GetAsync(p => p.Id == id);
        if (entity == null)
            return false;
        await _productRepository.BeginTransactionAsync();
        try
        {
            _productRepository.Remove(entity);
            await _productRepository.SaveAsync();
            await _productRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
            return false;
        }
    }
    public async Task<bool> ProductExsistsAsync(string productName)
    {
        var result = await _productRepository.AlreadyExistsAsync(x => x.ProductName == productName);
        return result;
    }
}
