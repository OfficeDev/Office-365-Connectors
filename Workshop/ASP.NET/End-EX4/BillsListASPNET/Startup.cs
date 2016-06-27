using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Configuration;

[assembly: OwinStartup(typeof(BillsListASPNET.Startup))]

namespace BillsListASPNET
{
    public class Startup
    {
        public static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        public static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        public static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = String.Format(CultureInfo.InvariantCulture, aadInstance, "common", "/v2.0"),
                    RedirectUri = redirectUri,
                    Scope = "openid email profile",
                    ResponseType = "id_token",
                    PostLogoutRedirectUri = redirectUri,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = (context) =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Error?message=" + context.Exception.Message);
                            return Task.FromResult(0);
                        }
                    }
                });
        }
    }
}
