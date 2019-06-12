using IdentityServer4.Models;
using IdentityServer4.Validation;
using Idp.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idp.BLL
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = _userRepository.Find(context.UserName, context.Password);

                if (user != null)
                {
                    var claims = _userRepository.GetClaims(user);

                    List<Claim> c = new List<Claim>();

                    foreach (var data in claims)
                    {
                        c.Add(new Claim(data.Type, data.Value));
                    }

                    //set the result
                    context.Result = new GrantValidationResult(
                        subject: user.UserId.ToString(),
                        authenticationMethod: "custom",
                        claims: c);

                    return Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }

            return Task.CompletedTask;
        }
    }
}
