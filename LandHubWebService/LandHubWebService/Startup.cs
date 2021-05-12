using Command;

using CommandHandler;

using FluentValidation;

using LandHubWebService.Helpers;
using LandHubWebService.Pipeline;
using LandHubWebService.Validations;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Services.IManagers;
using Services.Managers;
using Services.Repository;

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
            var x = Configuration.GetSection("Mongosettings");
            services.Configure<Mongosettings>(Configuration.GetSection("Mongosettings"));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
            services.AddMediatR(typeof(CreateUserCommand).Assembly, typeof(CreateUserCommandHandler).Assembly);

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient(typeof(IBaseRepository<>), (typeof(BaseRepository<>)));
            services.AddTransient<IMongoLandHubDBContext, MongoLandHubDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Land Hub API");
                c.RoutePrefix = string.Empty;
            });

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
