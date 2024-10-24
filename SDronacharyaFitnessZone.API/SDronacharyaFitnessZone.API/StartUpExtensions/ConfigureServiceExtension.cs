using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using SDronacharyaFitnessZone.Core.Services;
using SDronacharyaFitnessZone.Infrastructure.DBContext;
using SDronacharyaFitnessZone.Infrastructure.Repositories;

namespace SDronacharyaFitnessZone.UserInterface.StartUpExtensions
{
    public static class ConfigureServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SDronacharyaDBConnectionString"));
            });
        }
    }
}
