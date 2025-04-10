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
using System.Web.Mvc;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }



    //Behöver ev denna för visa lista med clients i addproject
    public async Task<ClientResult> GetClientsSelectListAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        var clientList = result.Result!.Select(c => new Client
        {
            Id = c.Id,
            ClientName = c.ClientName,
            Email = c.Email,
            BillingReference = c.BillingReference,
            BillingAddress = c.BillingAddress
        }).ToList();

        return new ClientResult
        {
            Result = clientList,
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            Error = result.Error
        };
    }

}
