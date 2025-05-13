using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Application.Repositories;

namespace OpenBankAPI.Application
{
	public static class ServiceRegistration
	{
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));
            collection.AddHttpClient();


        }
    }
}

