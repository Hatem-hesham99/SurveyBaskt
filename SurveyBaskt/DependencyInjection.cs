using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBaskt.Authantication;
using SurveyBaskt.persistence;
using System.Diagnostics;
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
                options.UseSqlServer(connectionstring)
                .LogTo(massage=> Debug.WriteLine(massage) , LogLevel.Warning) 
                );

            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<ApplicatonDbContext>();

            // add mapster 
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(config));


            // add fluent validation
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //add Authantication
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                op.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "SurveyBaskt App create by hatem",
                    ValidateAudience = true,
                    ValidAudience = "SurveyBaskt Users",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("J3rpXIEJ7PNNHSSlOsFRLf1PTnAC1DhW")),
                    ValidateLifetime = true,
                };
            });

            // add poll service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPollService, PollService>();
            services.AddSingleton<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
