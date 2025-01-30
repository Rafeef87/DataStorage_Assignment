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
    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        var enttiy = await _productRepository.GetAsync(x => x.ProductName ==  form.ProductName);
        enttiy ??= await _productRepository.CreateAsync(ProductFactory.Create(form));

        return ProductFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var entties = await _productRepository.GetAllAsync();
        var products = entties.Select(ProductFactory.Create);
        return products ?? [];
    }
    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {

        var enttiy = await _productRepository.GetAsync(expression);
        var product= ProductFactory.Create(enttiy);
        return product ?? null!;
    }
    //UPDATE
    public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
    {
        var updateEntity = ProductFactory.Create(form);
        var entity = await _productRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var product = ProductFactory.Create(entity);
        return product ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteProductAsync(int id)
    {
       var result = await _productRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
