

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class AddProjectForm
{


    [DataType(DataType.Upload)]
    [Display(Name = "Image", Prompt = "Select Image")]
    public IFormFile? ImageUrl { get; set; }


    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;


    //DESCRIPTION RICH TEXT
    [DataType(DataType.Text)]
    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; } = null!;


    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }


    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }



    [DataType(DataType.Currency)]
    [Display(Name = "Budget", Prompt = "0")]
    public decimal? Budget { get; set; }



    public string ClientId { get; set; } = null!;


    public string MemberId { get; set; } = null!;



    public int StatusId { get; set; }






    //[DataType(DataType.Text)]
    //[Display(Name = "Status", Prompt = "")]



    //Insert data här -members/users
    //[Display(Name = "Members", Prompt = "Find members...")]
    //[Required(ErrorMessage = "Required")]

    //Insert data här - clients
   

    //[Display(Name = "Client Name", Prompt = "Client Name")]
    //[Required(ErrorMessage = "Required")]
    //public string ClientName { get; set; } = null!;
}