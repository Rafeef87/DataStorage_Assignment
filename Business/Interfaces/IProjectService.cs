using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    Task CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<ProjectDetailsDto>> GetAllProjectsAsyncFK();
    Task<IEnumerable<Project?>> GetAllProjectsAsync();
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task UpdateProjectAsync(ProjectUpdateForm from);
    Task<bool> DeleteProjectAsync(int id);
}