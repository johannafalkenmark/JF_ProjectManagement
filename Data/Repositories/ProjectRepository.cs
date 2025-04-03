
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    //public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    //{
    //    //För projekt vill jag kunna ta fram Client-lista.
    //    var entities = await _context.Projects
    //        .Include(x => x.Client)
    //        .ToListAsync();

    //    return entities!;
    //}


    //public override async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    //{
    //    if (expression == null)
    //        return null!;

    //    return await _context.Projects
    //        .Include(x => x.Client)
    //        .FirstOrDefaultAsync(expression) ?? null!;
    //}
}

