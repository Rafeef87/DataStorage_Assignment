
using Data.Entities;
using Data.Interfaces;

namespace Shared.Services;
//Detta är genererat av Chat GPT 4o 
public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task AddProjectAsync(ProjectEntity project)
    {
        await _projectRepository.CreateAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectEntity project)
    {
        await _projectRepository.UpdateAsync(p => p.Id == project.Id, project);
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        await _projectRepository.DeleteAsync(p => p.Id == projectId);
    }
}
