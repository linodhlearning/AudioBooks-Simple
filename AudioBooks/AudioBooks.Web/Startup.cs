using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using AudioBooks.Web.HttpHandlers;

namespace AudioBooks.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            //ASP.NET Core also does some magic mapping as a default.Some claims are removed, and some are added. If you want to take control of this, you can turn this off as follows:
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddHttpContextAccessor();
            services.AddTransient<BearerTokenHandler>();

            // create an HttpClient used for accessing the API
            services.AddHttpClient("AudioBooksAPIClient", client =>
            {
                var audioBookApiUri = _configuration.GetValue<string>("Apis:AudioBookApi");
                client.BaseAddress = new Uri(audioBookApiUri);/*https://localhost:44305*/
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

            var idpUri = _configuration.GetValue<string>("Apis:IDP");/*"https://localhost:55441/"*/

            // create an HttpClient used for accessing the API
            services.AddHttpClient("LinIDPClient", client =>
            {
                client.BaseAddress = new Uri(idpUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
             {
                 options.AccessDeniedPath = "/Authorization/AccessDenied";
             })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = idpUri;
                options.ClientId = "audiobookwebclient";
                options.ResponseType = "code"; //CODE FLOW
                options.UsePkce = true;//PROOF KEY OF CODE EXCHANGE
                                       //options.CallbackPath = new Microsoft.AspNetCore.Http.PathString("..");// customise redirect uri of signin-oidc
                                       //  options.Scope.Add("openid"); //default no need to explicitly define this
                                       //  options.Scope.Add("profile");//default no need to explicitly define this
                options.Scope.Add("email");
                options.Scope.Add("address");
                options.Scope.Add("roles");
                options.Scope.Add("operations");
                options.Scope.Add("audiobooksapi");

                /*Delete from the ID_TOken*/
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.DeleteClaim("s_hash");
                options.ClaimActions.DeleteClaim("auth_time");

                options.ClaimActions.MapUniqueJsonKey("role", "role");
                options.SaveTokens = true;
                options.ClientSecret = "test_secret";
                options.GetClaimsFromUserInfoEndpoint = true;//get call to user info end point using access token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();// has route data

            app.UseAuthentication();
            app.UseAuthorization();

            // mvc routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
