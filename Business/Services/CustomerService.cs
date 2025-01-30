using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    //CREATE
    public async Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var enttiy = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        enttiy ??= await _customerRepository.CreateAsync(CustomerFactory.Create(form));

        return CustomerFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entties = await _customerRepository.GetAllAsync();
        var customers = entties.Select(CustomerFactory.Create);
        return customers ?? [];
    }
    public async Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {

        var enttiy = await _customerRepository.GetAsync(expression);
        var customer = CustomerFactory.Create(enttiy);
        return customer ?? null!;
    }
    //UPDATE
    public async Task<Customer> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        var updateEntity = CustomerFactory.Create(form);
        var entity = await _customerRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var result = await _customerRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}