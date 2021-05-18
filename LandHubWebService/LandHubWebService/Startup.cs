using Command;

using CommandHandler;

using Domains.DBModels;

using FluentValidation;

using LandHubWebService.Helpers;
using LandHubWebService.Pipeline;
using LandHubWebService.Validations;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MongoDbGenericRepository;

using PropertyHatchCoreService.IManagers;
using PropertyHatchCoreService.Managers;

using Services.IManagers;
using Services.IServices;
using Services.Managers;
using Services.Repository;
using Services.Services;

using System.Text;

namespace LandHubWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PropertyHatch.Service", Version = "v1" });
            });

            services.Configure<Mongosettings>(Configuration.GetSection("Mongosettings"));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
            services.AddMediatR(typeof(CreateUserCommand).Assembly, typeof(CreateUserCommandHandler).Assembly);

            services.AddTransient<IBaseUserManager, BaseUserManager>();
            services.AddTransient(typeof(IBaseRepository<>), (typeof(BaseRepository<>)));
            services.AddTransient<IMongoLandHubDBContext, MongoLandHubDBContext>();
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<IOrganizationManager, OrganizationManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<ITokenService, TokenService>();

            var mongoDbContext = new MongoDbContext(Configuration.GetSection("Mongosettings:Connection").Value, Configuration.GetSection("Mongosettings:DatabaseName").Value);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddMongoDbStores<IMongoDbContext>(mongoDbContext)
              .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };

            });
            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PropertyHatch.Service v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
