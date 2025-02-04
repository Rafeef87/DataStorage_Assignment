using System.Diagnostics;
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
        try
        {
            var enttiy = await _projectRepository.CreateAsync(ProjectFactory.Create(form));

            return ProjectFactory.Create(enttiy);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
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
        try {
            var updateEntity = ProjectFactory.Create(form);

            // Update the project in the database by submitting the correct entity
            var entity = await _projectRepository.UpdateAsync(x => x.Id == form.Id, updateEntity);
            // Create a Project object (for use in the application) from the updated ProjectEntity
            var project = ProjectFactory.Create(entity);
            return project ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
    //DELETE
    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}