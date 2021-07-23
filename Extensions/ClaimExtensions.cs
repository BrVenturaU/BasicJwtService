using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BasicJwtService.Extensions
{
    /// <summary>
    /// Extension class for the token claims.
    /// </summary>
    public static class ClaimExtensions
    {
        /// <summary>
        /// Add default claims such as the JTI claim.
        /// </summary>
        /// <param name="claims">The actual list of claims.</param>
        /// <returns></returns>
        public static List<Claim> AddDefaultClaims(this List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return claims;
        }
    }
}
