using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Repositories;
using ApiDotNet6.Application.Mappings;
using ApiDotNet6.Application.Services.Interfaces;
using ApiDotNet6.Application.Services;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Authorization;
using ApiDotNet6.Infra.Data.Authentication;

namespace ApiDotNet6.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql( configuration.GetConnectionString("DefaultConnection")));

            // Injetando os repositórios.
            services.AddScoped<IPersonRepository, PersonRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
