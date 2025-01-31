﻿using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

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

    public static Project Create(ProjectEntity entity) => new()
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
}