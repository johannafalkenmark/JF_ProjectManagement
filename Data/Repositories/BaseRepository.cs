﻿using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, TModel> (DataContext context) : IBaseRepository<TEntity, TModel> where TEntity : class
{
    protected readonly DataContext _context = context;

 
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

  

    //CREATE
    public virtual async Task<RepositoryResult<bool>> CreateAsync(TEntity entity)
    {
        if (entity == null)

            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can not be null."};
        try
        {


            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201};

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} :: {ex.Message}");

            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };

        }
    }






    //READ
    public virtual async Task<RepositoryResult<IEnumerable<TModel>>> GetAllAsync (        bool orderByDescending = false,Expression<Func<TEntity, object>>? sortBy = null,        Expression<Func<TEntity, bool>>? where = null,  params Expression<Func<TEntity, object>>[] includes)
    {

        IQueryable<TEntity> query = _dbSet; 

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.ToListAsync();
        var result = entities.Select(entity => entity.MapTo<TModel>());
            return new RepositoryResult<IEnumerable<TModel>> { Succeeded = true, StatusCode = 200, Result = result };
        
    }


    public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {

        IQueryable<TEntity> query = _dbSet;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.Select(selector).ToListAsync();


        var result = entities.Select(entity => entity!.MapTo<TSelect>());
        return new RepositoryResult<IEnumerable<TSelect>> { Succeeded = true, StatusCode = 200, Result = result };
    }



    public virtual async Task<RepositoryResult<TModel>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {


        IQueryable<TEntity> query = _dbSet;

    

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);


        var entity = await query.FirstOrDefaultAsync(where);

        if (entity == null)
            return new RepositoryResult<TModel> { Succeeded = false, StatusCode = 404, Error = "Entity not found" };


        var result = entity.MapTo<TModel>();

        return new RepositoryResult<TModel> { Succeeded = true, StatusCode = 200, Result = result };


    }


    public virtual async Task<RepositoryResult<bool>> AlreadyExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _dbSet.AnyAsync(findBy);
        return !exists
        ? new RepositoryResult<bool> { Succeeded = false, StatusCode = 404, Error = "Entity not found" }
            : new RepositoryResult<bool> { Succeeded = true, StatusCode = 200};
    }

    //UPDATE
    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {

        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can not be null." };

        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Error updating {nameof(TEntity)} :: {ex.Message}");

                return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
            
        }
    }


    //DELETE
    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {

        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can not be null." };

        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} :: {ex.Message}");
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }


    }

}
