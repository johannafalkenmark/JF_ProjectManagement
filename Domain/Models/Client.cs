

namespace Domain.Models;

public class Client
{
    public string Id { get; set; } = null!;
    public string ClientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? BillingReference { get; set; }

    public string? BillingAddress { get; set; }
}
