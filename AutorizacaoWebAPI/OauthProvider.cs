using AutorizacaoWebAPI.Models;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace AutorizacaoWebAPI
{
    public class OauthProvider : OAuthAuthorizationServerProvider
    {

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                string username = context.UserName;
                string password = context.Password;

                Usuario user = new Usuario().Get(username, password);

                if (user != null)
                {
                    List<Claim> claims = new List<Claim> {
                        new Claim(ClaimTypes.Name,user.Nome),
                        new Claim("UserID", user.ID.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    };
                    ClaimsIdentity OAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(OAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));

                }
                else
                {
                    context.SetError("erro", "erro");

                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

    }
}