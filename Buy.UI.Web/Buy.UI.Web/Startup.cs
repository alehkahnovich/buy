using Buy.UI.Web.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Buy.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var settings = Configuration.GetSection("OpenId").Get<OpenIdSettings>();

            services
                .AddAuthentication(options => {
                    options.DefaultScheme = "buy.root.idp";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("buy.root.idp")
                .AddOpenIdConnect("oidc", options => {
                    options.Authority = settings.Authority;
                    options.ClientId = settings.ClientId;
                    options.ClientSecret = settings.ClientSecret;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    foreach (var scope in settings.Scopes) options.Scope.Add(scope);

                    options.SignInScheme = "buy.root.idp";
                    options.ResponseType = "code id_token";
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.CallbackPath = new PathString("/home/signin-oidc");
                    options.SaveTokens = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=landing}/{action=index}/{id?}");
            });
        }
    }
}
