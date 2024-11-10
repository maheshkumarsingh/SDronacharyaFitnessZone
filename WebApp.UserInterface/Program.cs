using WebApp.UserInterface.Extensions.StartUpExtensions;
using WebApp.UserInterface.Middleware;

namespace SDronacharyaFitnessZone.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureService(builder.Configuration);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDeveloperExceptionPage();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200",
                            "https://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
