using System;
using Microsoft.Extensions.DependencyInjection;
using OpenBankAPI.Application.Abstraction.Token;
using OpenBankAPI.Infrastructure.Services.Token;

namespace OpenBankAPI.Infrastructure
{
	public static  class ServiceRegistration
	{

        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            //serviceCollection.AddScoped<IMailService, MailService>(); 
            //serviceCollection.AddScoped<IApplicationService, ApplicationService>();
        }


    }
}

