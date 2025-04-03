using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.Design.Serialization;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]

public class AdminController(IMemberService memberService, IProjectService projectService, IClientService clientService, IStatusService statusService) : Controller
{

    private readonly IMemberService _memberService = memberService;

    private readonly IProjectService _projectService = projectService;

    private readonly IClientService _clientService = clientService;

    private readonly IStatusService _statusService = statusService;



    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> Overview()
    {
        var viewmodel = new OverviewViewModel()
        {
            Clients = await _clientService.GetClientsAsync(),
            Projects = await _projectService.GetProjectsAsync(),
            Members = await _memberService.GetMembersAsync()
        };
        return View(viewmodel);
    }

    public async Task<IActionResult> Projects()
    {

        var viewmodel = new ProjectsViewModel()
        {
            Projects =
            await _projectService.GetProjectsAsync(),
            Members = await _memberService.GetMembersAsync(),
            Clients = await _clientService.GetClientsAsync(),


        };
        return View(viewmodel);
    }


    //Behöver jag en egen ADDprojectviewmodel?
    [HttpPost]
    public async Task<IActionResult> AddProject(ProjectsViewModel model)
    {
        var addProjectForm = model.MapTo<AddProjectForm>();
        var result = await _projectService.CreateProjectAsync(addProjectForm);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditProject()
    {
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> DeleteProject(string id)
    {
        return View();

    }

    public async Task<IActionResult> Members()
    {
        var viewModel = new MembersViewModel()
        {
            Members = await _memberService.GetMembersAsync()
        };


        return View(viewModel);
    }

    public async Task<IActionResult> Clients()
    {
        var viewmodel = new ClientsViewModel()
        {
            Clients = await _clientService.GetClientsAsync()
        };
        return View(viewmodel);
    }


}
