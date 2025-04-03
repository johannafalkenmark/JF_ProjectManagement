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
        
        //omvandla till project entity:
        var projectEntity = form.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync(1);
        var status = statusResult.Result;
        projectEntity.StatusId = status!.Id;


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
        //Hade kunnat endast skicka tillbaka mapto här
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

}
//    public async Task<bool> CreateProjectAsync(AddProjectForm form)

//    {
//        try
//        {

//            var projectEntity = ProjectFactory.Create(form);
//            if (projectEntity == null)
//                return false;

//            bool result = await _projectRepository.CreateAsync(projectEntity);
//            return result;
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine(ex.Message);
//            return false;
//        }
//    }

//    public async Task<IEnumerable<Project>> GetProjectsAsync()
//    {


//        var entities = await _projectRepository.GetAllAsync();

//        var projects = entities.Select(ProjectFactory.Create);

//        return projects;
//    }

//    //Fortsätt CRUD


//
