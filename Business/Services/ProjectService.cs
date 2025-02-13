using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ProjectService(ICustomerService  customerService, IProductService productService, IStatusTypeService statusTypeService, IUserService userService, IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerService _customerService = customerService;
    private readonly IProductService _productService = productService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    private readonly IUserService _userService = userService;

    //CREATE
    public async Task CreateProjectAsync(ProjectRegistrationForm form)
    {
        var customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);
        if (customer == null)
        {
            var result = await _customerService.CreateCustomerAsync(form.Customer);
            if (result)
                customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);
        }
        if (customer != null)
        { 
            await _projectRepository.BeginTransactionAsync();
            try
            {
                var projectEnttiy = ProjectFactory.Create(form);
                projectEnttiy!.CustomerId = customer.Id;

                await _projectRepository.AddAsync(projectEnttiy);
                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();
            }
            catch 
            {
                await _projectRepository.RollbackTransactionAsync();
            }
        }

        var product = await _productService.GetProductAsync(form.Product.ProductName);
        if (product == null)
        {
            var result = await _productService.CreateProductAsync(form.Product);
            if (result)
                product = await _productService.GetProductAsync(form.Product.ProductName);
        }
        if (product != null)
        {
            await _projectRepository.BeginTransactionAsync();
            try
            {
                var projectEnttiy = ProjectFactory.Create(form);
                projectEnttiy!.ProductId = product.Id;

                await _projectRepository.AddAsync(projectEnttiy);
                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();
            }
            catch
            {
                await _projectRepository.RollbackTransactionAsync();
            }
        }

        var status = await _statusTypeService.GetStatusTypeAsync(form.Status.StatusName);
        if (status == null)
        {
            var result = await _statusTypeService.CreateStatusTypeAsync(form.Status);
            if (result)
                status = await _statusTypeService.GetStatusTypeAsync(form.Status.StatusName);
        }
        if (status != null)
        {
            await _projectRepository.BeginTransactionAsync();
            try
            {
                var projectEnttiy = ProjectFactory.Create(form);
                projectEnttiy!.StatusId = status.Id;

                await _projectRepository.AddAsync(projectEnttiy);
                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();
            }
            catch
            {
                await _projectRepository.RollbackTransactionAsync();
            }
        }

        var user = await _userService.GetUserAsync(form.User.FirstName);
        if (user == null)
        {
            var result = await _userService.CreateUserAsync(form.User);
            if (result)
                user = await _userService.GetUserAsync(form.User.FirstName);
        }
        if (user != null)
        {
            await _projectRepository.BeginTransactionAsync();
            try
            {
                var projectEnttiy = ProjectFactory.Create(form);
                projectEnttiy!.UserId = user.Id;

                await _projectRepository.AddAsync(projectEnttiy);
                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();
            }
            catch
            {
                await _projectRepository.RollbackTransactionAsync();
            }
        }
    }
    //READ
    public async Task<IEnumerable<ProjectDetailsDto>> GetAllProjectsAsyncFK()
    {
        var entities = await _projectRepository
            .GetAllIncludingAsync(p => p.Customer, p => p.Status, p => p.User, p => p.Product);
        
        var projects = entities.Select(ProjectFactory.Read);
        return projects;
    }
    public async Task<IEnumerable<Project?>> GetAllProjectsAsync()
    {
        var entties = await _projectRepository.GetAllAsync();
        var projects = entties.Select(ProjectFactory.Create);
        return projects ?? [];
    }
    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {

        var enttiy = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(enttiy!);
        return project ?? null!;
    }
    //UPDATE
    public async Task UpdateProjectAsync(ProjectUpdateForm form)
    {
        await _projectRepository.BeginTransactionAsync();
        try
        {
            _projectRepository.Update(new ProjectEntity { ProjectName = form.ProjectName });
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
           
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
            
        }
    }
    //DELETE
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;
        await _projectRepository.BeginTransactionAsync();
        try
        {
            _projectRepository.Remove(entity);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }
}