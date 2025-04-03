using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity, MemberAddress>(context), IUserAddressRepository
{
    private readonly DataContext _context = context;

}