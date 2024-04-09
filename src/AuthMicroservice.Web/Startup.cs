using AuthMicroservice.Application.Configurations;
using AuthMicroservice.Infrastructure.DependencyInjection;

namespace AuthMicroservice.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(_configuration);

            services.AddJwtAuthentication();
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
