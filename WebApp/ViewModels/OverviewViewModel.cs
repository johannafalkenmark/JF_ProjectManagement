using Domain.Models;

namespace WebApp.ViewModels;


//Exempel overview
public class OverviewViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];
    public AddClientForm AddClientForm { get; set; } = new AddClientForm();

    public IEnumerable<Member> Members { get; set; } = [];
    public AddMemberForm AddMemberForm { get; set; } = new AddMemberForm();

    public IEnumerable<Project> Projects { get; set; } = [];
    public AddProjectForm AddProjectForm { get; set; } = new AddProjectForm();

    public IEnumerable<Status> Status { get; set; } = [];


}