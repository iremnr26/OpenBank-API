using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OpenBankAPI.Persistence.Contexts;
using OpenBankAPI.Persistence.Repositories;
using OpenBankAPI.Application.Repositories;
using Domain.Entities.Identity;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Persistence.Services;
using OpenBankAPI.Application.Abstraction.Services;

namespace OpenBankAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySqlConnection");

            services.AddDbContext<OpenBankAPIDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                )
            );

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            // ✅ Generic Repository'leri Tanımlayalım
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OpenBankAPIDbContext>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));


            //scoped yaptık çünkü kullandıktan sonra dispose ediyor 
            services.AddScoped<IAccountReadRepository, AccountReadRepository>(); 
            services.AddScoped<IAccountWriteRepository, AccountWriteRepository>();
                    
            services.AddScoped<IBankReadRepository,BankReadRepository>(); 
            services.AddScoped<IBankWriteRepository, BankWriteRepository>();
                        
            services.AddScoped<IUserReadRepository,UserReadRepository>();
            services.AddScoped<IUserWriteRespository,UserWriteRepository>();
                         
            services.AddScoped<ITransactionReadRepository,TransactionReadRepository>();
            services.AddScoped<ITransactionWriteRepository ,TransactionWriteRepository>();
         
        }
    }
}
