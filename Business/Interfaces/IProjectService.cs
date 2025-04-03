using Business.Models;
using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectForm form);
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
}