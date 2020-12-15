using AudioBooks.Api.Repositories;
using AudioBooks.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using Microsoft.ApplicationInsights.Extensibility;
using IdentityServer4.AccessTokenValidation;
using AudioBooks.Api.Repositories.Contracts;

namespace AudioBooks.Api
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
            services.AddMemoryCache();//todo use redis cache in future

            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);/*use pascal case*/


            var idpUri = this.Configuration.GetValue<string>("Apis:IDP");//"https://localhost:55441/"
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)// Auth scheme is Bearer
                .AddIdentityServerAuthentication(options =>
                {  // base-address of your identityserver
                    options.Authority = "https://localhost:55441";
                    options.ApiName = "audiobooksapi";
                });


            services.AddDbContext<AudioBookContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("AudioBookContextConnection"))
            // .EnableSensitiveDataLogging()
            );

            // Inject Application Insights
            this.ConfigureApplicationInsights(services);

            //register services and repositories
            this.ConfigureServiceRepositories(services);

            // register AutoMapper-related services
            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        private void ConfigureApplicationInsights(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton(Configuration);
            services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>(t => new TelemetryInitializer(Configuration["ApplicationInsights:cloud_roleName"]));
        }

        private void ConfigureServiceRepositories(IServiceCollection services)
        {
            services.AddScoped<IAudioBookRepository, AudioBookRepository>();
            services.AddScoped<ILookupDataRepository, LookupDataRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audiobooks API V1");
                    // c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        // ensure generic 500 status code on fault.
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError; ;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
