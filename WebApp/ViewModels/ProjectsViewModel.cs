using Domain.Models;

namespace WebApp.ViewModels;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];

    public IEnumerable<Client> Clients { get; set; } = [];
    public IEnumerable<Member> Members { get; set; } = [];
    public IEnumerable<Status> Status { get; set; } = [];

    public AddProjectForm AddProjectForm { get; set; } = new AddProjectForm();
    public AddClientForm AddClientForm { get; set; } = new AddClientForm();
    public AddMemberForm AddMemberForm { get; set; } = new AddMemberForm();
}
