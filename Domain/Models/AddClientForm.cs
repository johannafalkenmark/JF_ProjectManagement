

using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class AddClientForm
{
    [DataType(DataType.Text)]
    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required")]

    public string ClientName { get; set; } = null!;


    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^@]+@[^@]+\.[^@]+[^@]+@[^@]+\.[^@]+$", ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "Billing Address", Prompt = "Enter billing address")]
    public string? BillingAddress { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Billing Reference", Prompt = "Enter billing reference")]
    public string? BillingReference { get; set; }
}
