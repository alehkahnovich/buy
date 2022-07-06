using Buy.Infrastructure.Library.Dependencies;
using Buy.Infrastructure.Web.Extensions;
using Buy.Upload.Web.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Buy.Upload.Web
{
    public class Startup {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options => options
                .AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithExposedHeaders(Headers.Artifact)
                    .AllowAnyMethod()
                    .Build()));

            services
                .AddExceptionHandling()
                .BootDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseExceptionHandling();
            app.UseAuthentication();
            app.UseCors();
            app.UseMvc();
        }
    }
}
