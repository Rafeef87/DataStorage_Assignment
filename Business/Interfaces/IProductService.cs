﻿using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductRegistrationForm form);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<Product> UpdateProductAsync(ProductUpdateForm from);
    Task<bool> DeleteProductAsync(int id);
}