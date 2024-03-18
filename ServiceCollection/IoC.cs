using Domain.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollection
{
    public class IoC
    {
        public static void AddDependency(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
