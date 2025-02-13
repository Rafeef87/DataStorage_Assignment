using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<IEnumerable<Customer?>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<bool> UpdateCustomerAsync(CustomerUpdateForm from);
    Task<bool> DeleteCustomerAsync(int id);
}
