using Microsoft.EntityFrameworkCore;
using SDronacharyaFitnessZone.Infrastructure.DBContext;

namespace SDronacharyaFitnessZone.UserInterface.StartUpExtensions
{
    public static class ConfigureServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SDronacharyaDBConnectionString"));
            });
        }
    }
}
