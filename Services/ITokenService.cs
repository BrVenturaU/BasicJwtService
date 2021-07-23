using System.Collections.Generic;
using System.Security.Claims;

namespace BasicJwtService.Services
{
    /// <summary>
    /// This interface enable you to consume the TokenService injected in the IoC.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// This method allows you to create a JWT token and adds the claims you provide.
        /// </summary>
        /// <param name="claims">The claims for the token's payload.</param>
        /// <returns>A string token.</returns>
        string GenerateToken(List<Claim> claims);
        /// <summary>
        /// Allow to get the token from the request header (Authorization Header)
        /// </summary>
        /// <returns>The JWT token.</returns>
        string GetToken();
        /// <summary>
        /// This method reads the token to get the claims.
        /// </summary>
        /// <param name="token">The JWT token to get the claims.</param>
        /// <returns>A list of token claims.</returns>
        IEnumerable<Claim> GetTokenClaims(string token = null);
        /// <summary>
        /// Reads the user identity of the HttpContext to get the claims.
        /// </summary>
        /// <returns>A list of user claims (null if token is expired).</returns>
        IEnumerable<Claim> GetIdentityTokenClaims();
        /// <summary>
        /// Reads the user of the HttpContext to get the claims.
        /// </summary>
        /// <returns>A list of user claims (null if token is expired).</returns>
        IEnumerable<Claim> GetUserTokenClaims();
        /// <summary>
        /// Get a claim value from the list of claims by the claim name/type.
        /// </summary>
        /// <param name="claimName">The claim name/type in the token.</param>
        /// <param name="token">The token to retrieve the claim value.</param>
        /// <returns>The claim value.</returns>
        string GetTokenClaimValue(string claimName, string token = null);
    }
}
