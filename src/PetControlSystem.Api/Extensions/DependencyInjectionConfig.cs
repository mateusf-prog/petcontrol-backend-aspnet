using PetControlSystem.Data.Context;
using PetControlSystem.Data.Repository;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Security;
using PetControlSystem.Domain.Services;

namespace PetControlSystem.Api.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // data
            services.AddScoped<MyDbContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPetSupportRepository, PetSupportRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();

            // domain
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPetSupportService, PetSupportService>();
            services.AddScoped<INotificator, Notificator>();

            return services;
        }
    }
}
