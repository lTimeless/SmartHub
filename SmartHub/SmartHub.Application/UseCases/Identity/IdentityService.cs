using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity
{
    /// <summary>
    /// In here are functions that can be used in all Identity Services
    /// </summary>
    public class IdentityService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;

        public IdentityService(ITokenGenerator tokenGenerator, UserManager<User> userManager)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates an AuthResponse with the given params
        /// </summary>
        /// <param name="user">The currentUser </param>
        /// <param name="roles">The roles to the user</param>
        /// <returns></returns>
        internal AuthResponseDto CreateAuthResponse(User user, List<string> roles)
        {
            var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult() as List<Claim>;
            return new AuthResponseDto { Token = _tokenGenerator.CreateJwtToken(user, roles, claims ?? new List<Claim>()) };
        }
    }
}