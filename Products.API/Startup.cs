using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.API.Interfaces;
using Products.API.Options;
using Products.API.Repositories;
using Products.API.Services;
using Serilog;

namespace Products.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiOptions>(Configuration.GetSection(ApiOptions.SectionName));
            services.Configure<AzureQueueOptions>(Configuration.GetSection(AzureQueueOptions.SectionName));
            services.Configure<AzureStorageOptions>(Configuration.GetSection(AzureStorageOptions.SectionName));
            services.AddDbContext<ProductContext>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IAzureStorageService, AzureStorageService>();
            services.AddTransient<IAzureServiceBusService, AzureServiceBusService>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V4");
                c.RoutePrefix = string.Empty;
            });

            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}