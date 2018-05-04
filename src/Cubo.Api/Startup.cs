using AutoMapper;
using Cubo.Api.Middleware;
using Cubo.Core.Mappers;
using Cubo.Core.Repositories;
using Cubo.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cubo.Api
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
            services.AddMvc();
            services.AddScoped<IBucketService, BucketService>();
            services.AddSingleton<IBucketRepository, InMemoryBucketRepository>();
            services.AddScoped<IItemService, ItemService>();
            services.AddSingleton(_ => AutoMapperConfig.GetMapper());
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseMiddleware<ErrorHandleMiddleware>();
        }
    }
}
