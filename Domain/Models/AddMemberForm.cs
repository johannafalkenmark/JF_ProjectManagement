

using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class AddMemberForm
{

    [DataType(DataType.ImageUrl)]
    [Display(Name = "Image")]
    public string? ImageUrl { get; set; }


    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "First Name")]
    public string FirstName { get; set; } = null!;


    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;


    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [RegularExpression(@"^[^@]+@[^@]+\.[^@]+[^@]+@[^@]+\.[^@]+$", ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "Phone Number", Prompt = "Enter phone number")]
    public string? PhoneNumber { get; set; }


    [DataType(DataType.Text)]
    [Display(Name = "JobTitle", Prompt = "Enter job title")]
    public string? JobTitle { get; set; }


}
