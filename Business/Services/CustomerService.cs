using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == form.CustomerName))
            return false;
        await _customerRepository.BeginTransactionAsync();
        try
        {
            await _customerRepository.AddAsync(new CustomerEntity { CustomerName = form.CustomerName });
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //READ
    public async Task<IEnumerable<Customer?>> GetAllCustomersAsync()
    {
        var entties = await _customerRepository.GetAllAsync();
        var customers = entties.Select(CustomerFactory.Create);
        return customers;
    }
    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var enttiy = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(enttiy!);
    }
    public async Task<Customer?> GetCustomerAsync(string customerName)
    {
        var enttiy = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(enttiy!);
    }
    //UPDATE
    public async Task<bool> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == form.CustomerName))
            return false;
        await _customerRepository.BeginTransactionAsync();
        try
        {
            _customerRepository.Update(new CustomerEntity { CustomerName = form.CustomerName });
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //DELETE
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(x => x.Id == id);
        if(entity == null)
            return false;
        await _customerRepository.BeginTransactionAsync();
        try
        {
            _customerRepository.Remove(entity);
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }
    public async Task<bool> CustomerExsistsAsync(string customerName)
    {
            var result = await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == customerName);
            return result;
    }
}