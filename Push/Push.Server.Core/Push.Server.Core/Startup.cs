using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Push.Server.Core.Hubs;

namespace Push.Server.Core
{
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => options.AddPolicy("AddSignalRCore",
                builder => {
                    builder.AllowAnyMethod().AllowAnyHeader()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                }));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            

            app.UseRouting();
            app.UseCors("AddSignalRCore");
            app.UseEndpoints(endpoints => { endpoints.MapHub<PushupHub>("/push"); });
        }
    }
}
