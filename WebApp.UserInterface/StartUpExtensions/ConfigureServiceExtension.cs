using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SDronacharyaFitnessZone.Core.Domain.RepositoryContracts;
using SDronacharyaFitnessZone.Core.ServiceContracts;
using SDronacharyaFitnessZone.Core.Services;
using SDronacharyaFitnessZone.Infrastructure.Repositories;
using System.Text;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Core.ServiceContracts;
using WebApp.Core.Services;
using WebApp.Infrastructure.DBContext;

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
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenKey = configuration["TokenKey"] ?? throw new Exception("TokenKey not found");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey     (Encoding.UTF8.GetBytes(tokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SDronacharyaDBConnectionString"));
            });
        }
    }
}
