using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using MemesAPI.Models;

namespace WebApplication2.auth
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        DBManager db = null;
        public MyAuthorizationServerProvider()
        {
            if (db == null)
            {
                db = new DBManager();
            }
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var userdata = db.LoginHelper(context.UserName, context.Password);
            if (userdata != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid grant", "Username or Password Incorrect.");
                return;
            }
        }
    }
}