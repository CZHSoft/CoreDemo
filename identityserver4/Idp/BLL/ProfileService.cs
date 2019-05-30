using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idp.BLL
{
    public class ProfileService : IProfileService
    {
        //services
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var user = _userRepository.FindById(context.Subject.GetSubjectId());

                if (user != null)
                {
                    var claims = _userRepository.GetClaims(user);

                    List<Claim> c = new List<Claim>();

                    foreach (var data in claims)
                    {
                        c.Add(new Claim(data.Type, data.Value));
                    }

                    //set issued claims to return
                    context.IssuedClaims = c.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();

                    return Task.CompletedTask;
                }

            }
            catch (Exception ex)
            {
                //log your error
            }

            return Task.CompletedTask;
        }

        //check if user account is active.
        public Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var user = _userRepository.FindById(context.Subject.GetSubjectId());

                if (user != null)
                {
                    context.IsActive = user.IsActive;

                    return Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }

            return Task.CompletedTask;
        }
    }
}
