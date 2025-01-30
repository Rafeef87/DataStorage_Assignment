using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _ProductRepository = productRepository;
    //CREATE
    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        var enttiy = await _ProductRepository.GetAsync(x => x.ProductName ==  form.ProductName);
        enttiy ??= await _ProductRepository.CreateAsync(ProductFactory.Create(form));

        return ProductFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var entties = await _ProductRepository.GetAllAsync();
        var products = entties.Select(ProductFactory.Create);
        return products ?? [];
    }
    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {

        var enttiy = await _ProductRepository.GetAsync(expression);
        var product= ProductFactory.Create(enttiy);
        return product ?? null!;
    }
    //UPDATE
    public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
    {
        var updateEntity = ProductFactory.Create(form);
        var entity = await _ProductRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var product = ProductFactory.Create(entity);
        return product ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteProductAsync(int id)
    {
       var result = await _ProductRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
