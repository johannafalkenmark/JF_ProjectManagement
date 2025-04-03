using Domain.Models;

namespace WebApp.ViewModels;

public class ClientsViewModel
{
    public IEnumerable<Client> Clients { get; set; } = [];
    public AddClientForm AddClientForm { get; set; } = new AddClientForm();
}
