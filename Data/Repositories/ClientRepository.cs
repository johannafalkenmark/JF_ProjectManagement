using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
    //Instanserar Datacontext ifall vi vill göra modifieringar
    private readonly DataContext _context = context;

}
