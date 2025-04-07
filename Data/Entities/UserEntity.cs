using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? ImageUrl { get; set; }

    [ProtectedPersonalData]

    public string? FirstName { get; set; }

    [ProtectedPersonalData]

    public string? LastName { get; set; }

    [ProtectedPersonalData]

    public string? JobTitle { get; set; }

    public virtual UserAddressEntity? Address { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];


}


