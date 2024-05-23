using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using VATWebAPI.Models;

namespace VATWebAPI.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private AuthHelper _authHelper = new AuthHelper();


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {

                IFormCollection parameters = await context.Request.ReadFormAsync();

                if (parameters == null)
                {
                    context.SetError("invalid_grant", "Invalid request");
                    return;
                }


                string userName = parameters.Get("UserName");
                string password = parameters.Get("Password");
                string bin = parameters.Get("BIN");

                

                APIUtilities validator = new APIUtilities();
                (var isValid, var errorMessage) = validator.Validate(userName, password, bin);

                if (!isValid)
                {
                    context.SetError("invalid_grant", errorMessage);
                    return;
                }
                LogInModel user = new LogInModel();
                user.UserName = userName;
                user.Password = password;
                user.BIN = bin;
                _authHelper.Authenticate(user);
                string DbName = validator.GetDbName(user);


                ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);


                identity.AddClaim(new Claim(ClaimTypes.Name, "API"));
                identity.AddClaim(new Claim(ClaimTypes.Country, "BD"));
                identity.AddClaim(new Claim("FullName", "API"));
                identity.AddClaim(new Claim("UserName", user.UserName));
                identity.AddClaim(new Claim("BIN", user.BIN));
                identity.AddClaim(new Claim("Db", DbName));

                context.Validated(identity);
                return;


                //context.SetError("invalid_grant", "Access Denied");

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                context.SetError("invalid_grant", e.Message);
            }
        }
    }
}