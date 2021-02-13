using System;
using System.Security.Cryptography;

namespace SmartHub.Application.Common.Helpers
{
    public static class TokenUtils
    {
        private static string _staticToken= "";

        /// <summary>
        /// Generates a random token with a default length of 32chars.
        /// </summary>
        /// <param name="size"></param>
        /// <returns>The generated token.</returns>
        public static string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);
            _staticToken = token;
            return token;
        }

        /// <summary>
        /// Validates the given string Token and if it is not valid it will generate a new one and save it
        /// </summary>
        /// <param name="keyFromjson">The given token to validate</param>
        /// <returns>A new generated token</returns>
        public static string ValidateAndGenerateToken(string keyFromjson)
        {
            if (string.IsNullOrEmpty(keyFromjson) || keyFromjson.Contains("<"))
            {
                if (string.IsNullOrEmpty(_staticToken))
                {
                    _staticToken = GenerateToken();
                    return _staticToken;
                }
                return _staticToken;
            }

            _staticToken = keyFromjson;
            return _staticToken;
        }
    }
}