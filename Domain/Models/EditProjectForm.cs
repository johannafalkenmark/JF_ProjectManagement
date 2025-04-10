using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class EditProjectForm
{
    public string Id { get; set; } = null!;

    [DataType(DataType.Upload)]
    [Display(Name = "Image", Prompt = "Select Image")]
    public IFormFile? ImageUrl { get; set; }


    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; } = null!;


    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }


    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }



    [DataType(DataType.Currency)]
    [Display(Name = "Budget", Prompt = "0")]
    public decimal? Budget { get; set; }






    public virtual Client? Client { get; set; }

    public string ClientId { get; set; } = null!;


    public IEnumerable<Client> Clients { get; set; } = [];


    public virtual Member? Member { get; set; }

    public string MemberId { get; set; } = null!;

    public IEnumerable<Member> Members { get; set; } = [];


    public virtual Status? Status { get; set; }
    public IEnumerable<Status> Statuses { get; set; } = [];

    public int StatusId { get; set; }



}
