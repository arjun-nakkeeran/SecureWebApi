using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class MediatRServiceRegistration
    {
        public static void AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(MediatRServiceRegistration).Assembly));
        }
    }
}
