using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Identity
{
    /// <summary>
    /// In here are functions that can be used in all Identity Services
    /// </summary>
    public class IdentityService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public IdentityService(ITokenGenerator tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Signs in the user with the given input data.
        /// </summary>
        /// <param name="userInput">The login input data.</param>
        /// <param name="foundUser">The found user.</param>
        /// <returns>Return true if the input data belongs to the foundUser.</returns>
        public async Task<bool> LoginAsync(LoginInput userInput, User foundUser)
        {
	        var result = await _signInManager.CheckPasswordSignInAsync(foundUser, userInput.Password, false);
	        return result.Succeeded;
        }

        /// <summary>
        /// Handles the registration process
        /// </summary>
        /// <param name="userInput">The input the user does</param>
        /// <param name="user">The new created user</param>
        /// <returns>A bool which indicates if the function was successful or not</returns>
        public async Task<bool> RegisterAsync(RegistrationInput userInput, User user)
        {
	        var created = await _userRepository.CreateUser(user, userInput.Password, userInput.Role);
	        if (created)
	        {
		        return true;
	        }
	        throw new SmartHubException("Problem Registering new User.");
        }

        /// <summary>
        /// Creates an AuthResponse with the given params
        /// </summary>
        /// <param name="user">The currentUser </param>
        /// <param name="roles">The roles to the user</param>
        /// <returns></returns>
        internal IdentityPayload CreateAuthResponse(User user, List<string> roles)
        {
            var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult() as List<Claim>;
            return new IdentityPayload(user, _tokenGenerator.CreateJwtToken(user, roles, claims ?? new List<Claim>()));
        }
    }
}