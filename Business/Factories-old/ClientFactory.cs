//using Data.Entities;
//using Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Business.Factories;

//public class ClientFactory
//{
//    public static ClientEntity? Create(AddClientForm form) => form == null ? null : new ClientEntity
//    {
//        ClientName = form.ClientName,
//        Email = form.Email,
//        BillingReference = form.BillingReference,
//        BillingAddress = form.BillingAddress
//    };

//    public static Client Create(ClientEntity entity)
//    {
//        if (entity == null)
//            return null;

//        var client = new Client
//        {

//            ClientName = entity.ClientName,
//            Email = entity.Email,
//            BillingReference = entity.BillingReference,
//            BillingAddress = entity.BillingAddress
//        };
//        return client;

//    }
//}
