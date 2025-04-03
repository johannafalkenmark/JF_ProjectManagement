

using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public virtual DbSet<UserAddressEntity> UserAddresses { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<StatusEntity> Status { get; set; }
    
}
