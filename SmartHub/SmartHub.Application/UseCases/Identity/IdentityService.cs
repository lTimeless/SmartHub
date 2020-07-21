using System.Collections.Generic;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Utils;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.Identity
{
    /// <summary>
    /// In here are functions that can be used in all Identity Services
    /// </summary>
    public class IdentityService
    {
        private readonly ITokenGenerator _tokenGenerator;

        public IdentityService(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Creates an AuthResponse with the given params
        /// </summary>
        /// <param name="user">The currentUser </param>
        /// <param name="roles">The roles to the user</param>
        /// <returns></returns>
        internal AuthResponseDto CreateAuthResponse(User user, List<string> roles)
        {
            return	new AuthResponseDto(
                _tokenGenerator.CreateJwtToken(user),
                user.UserName,
                roles,
                DateTimeUtils.LocalNow.PlusHours(JwtExpireTime.HoursToExpire.GetValue())
            );
        }
    }
}