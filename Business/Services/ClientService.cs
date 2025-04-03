using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }

    ////CREATE
    //public async Task<bool> CreateClientAsync(AddClientForm form)

    //{
    //    try
    //    {

    //        var clientEntity = ClientFactory.Create(form);
    //        if (clientEntity == null)
    //            return false;

    //        bool result = await _clientRepository.CreateAsync(clientEntity);
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //        return false;
    //    }
    //}

    //public async Task<IEnumerable<Client>> GetClientsAsync()
    //{


    //    var entities = await _clientRepository.GetAllAsync();

    //    var clients = entities.Select(ClientFactory.Create);

    //    return clients;
    //}

    ////Fortsätt CRUD


}
