using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    //public static ProjectUpdateForm Update() => new();
    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        ProjectName = form.ProjectName,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        StatusId = form.StatusId,
        UserId = form.UserId,
        ProductId = form.ProductId,
    };
    

    // This method creates a Project from a ProjectEntity
    public static Project Create(ProjectEntity entity) 
    {
         if (entity == null)
        throw new ArgumentNullException(nameof(entity), "ProjectEntity is null in ProjectFactory.Create");

    return new Project()
    {
        Id = entity.Id,
        ProjectName = entity.ProjectName,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        CustomerId = entity.CustomerId,
        StatusId = entity.StatusId,
        UserId = entity.UserId,
        ProductId = entity.ProductId,
    };
    }
    // This method creates a ProjectEntity from a ProjectUpdateForm
    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        ProjectName = form.ProjectName,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        StatusId = form.StatusId,
        UserId = form.UserId,
        ProductId = form.ProductId
    };

    // This method creates a ProjectUpdateForm from a Project
    public static ProjectUpdateForm Create(Project project) => new()
    {
        Id = project.Id,
        ProjectName = project.ProjectName,
        StartDate = project.StartDate,
        EndDate = project.EndDate,
        CustomerId = project.CustomerId,
        StatusId = project.StatusId,
        UserId = project.UserId,
        ProductId = project.ProductId
    };
    //This method return a detailed DTO
    public static ProjectDetailsDto Read(ProjectEntity entity)
    {
        return new ProjectDetailsDto
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerName = entity.Customer.CustomerName,
            StatusName = entity.Status.StatusName,
            UserName = entity.User.FirstName,
            ProductName = entity.Product.ProductName
        };
    }


}