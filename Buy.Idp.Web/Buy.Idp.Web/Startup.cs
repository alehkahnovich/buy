using Buy.Idp.Business;
using Buy.Idp.Business.Stores;
using Buy.Idp.Business.Validators;
using Buy.Idp.Infrastructure.Dependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.Idp.Web
{
    internal sealed class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            var builder = services.AddIdentityServer(options => {
                options.UserInteraction.LoginUrl = "/account/login";
                options.Authentication.CheckSessionCookieName = "buy.idp.session";
            })
            .AddResourceStore<ResourceStore>()
            .AddClientStore<ClientStore>()
            .AddProfileService<ProfileService>()
            .AddPersistedGrantStore<PersistedGrantStore>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "buy.idp";
                    options.DefaultChallengeScheme = "buy.idp";
                })
                .AddCookie("buy.idp");

            services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    options.Authority = "http://localhost:5200";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "identity-resource";
                });


            services.BootDependencies(new Bootstrap());

            //todo:
            builder.AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
