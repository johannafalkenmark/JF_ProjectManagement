using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;


namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService= statusService;



    public async Task<ProjectResult> CreateProjectAsync(AddProjectForm form)
    {
        if (form == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are filled." };
        
        var projectEntity = form.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync(1);
        var status = statusResult.Result;
        projectEntity.StatusId = status!.Id;
        projectEntity.Created = DateTime.Now;

        var result = await _projectRepository.CreateAsync(projectEntity);
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };



    }


    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync
            ( //lägger in filtrering, sortering här:
            orderByDescending: true,
            sortBy: s => s.Created,
            where: null,
            include => include.User,
            include => include.Status,
            include => include.Client
            );
        return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 201, Result = response.Result };
        //Hade kunnat endast skicka tillbaka mapto här.
    }


    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            ( 
            where: x => x.Id == id,
            include => include.User,
            include => include.Status,
            include => include.Client
            );
        return response.Succeeded
            ? new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"project '{id}' was not found" };

    }


    public async Task<ProjectResult> UpdateProjectAsync(EditProjectForm form)
    {
        if (form == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are filled." };
        var projectEntity = form.MapTo<ProjectEntity>();
        var result = await _projectRepository.UpdateAsync(projectEntity);
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 200 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
}



