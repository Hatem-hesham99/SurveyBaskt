using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBaskt.persistence;
using System.Reflection;

namespace SurveyBaskt
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services , IConfiguration configuration)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();


            // add db context
            var connectionstring = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicatonDbContext>(options =>
                options.UseSqlServer(connectionstring));

            // add mapster 
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(config));


            // add fluent validation
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // add poll service
            services.AddScoped<IPollService, PollService>();

            return services;
        }
    }
}
