

namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string? ImageUrl { get; set; } 
    public string ProjectName { get; set; } = null!;

      public string? Description { get; set; } 

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }


    public decimal? Budget { get; set; }

    public virtual Status? Status { get; set; }

    //länkar denna till users/member:
    public virtual Member? Member { get; set; } = new Member();

    //länkar denna till client: OBS ev ej "new" på dessa?
    public virtual Client Client { get; set; } = new Client();
}
