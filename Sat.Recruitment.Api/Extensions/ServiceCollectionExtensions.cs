using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Abstractions;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IModelValidation<User>, UserValidationService>();
            services.AddTransient<IUserFactory, UserFactory>();
            services.AddTransient<IDataStore<User>, FileDataStore>();
        }

    }
}
