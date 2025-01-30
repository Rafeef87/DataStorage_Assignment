using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    //CREATE
    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var enttiy = await _projectRepository.GetAsync(x => x.ProjectName == form.ProjectName);
        enttiy ??= await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        return ProjectFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entties = await _projectRepository.GetAllAsync();
        var projects = entties.Select(ProjectFactory.Create);
        return projects ?? [];
    }
    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {

        var enttiy = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(enttiy);
        return project ?? null!;
    }
    //UPDATE
    public async Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var updateEntity = ProjectFactory.Create(form);
        var entity = await _projectRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}