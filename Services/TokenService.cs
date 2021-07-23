using BasicJwtService.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BasicJwtService.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TokenService'
    public class TokenService: ITokenService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TokenService'
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TokenService.TokenService(JwtSettings, IHttpContextAccessor)'
        public TokenService(JwtSettings jwtSettings, IHttpContextAccessor httpContextAccessor)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TokenService.TokenService(JwtSettings, IHttpContextAccessor)'
        {
           _jwtSettings = jwtSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// This method allows you to create a JWT token and adds the claims you provide.
        /// </summary>
        /// <param name="claims">The claims for the token's payload.</param>
        /// <returns>A string token.</returns>
        public string GenerateToken(List<Claim> claims)
        {
            var credentials = GetSigningCredentials();
            var tokenOptions = GenerateTokenOptions(credentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <summary>
        /// This method reads the token to get the claims.
        /// </summary>
        /// <param name="token">The JWT token to get the claims.</param>
        /// <returns>A list of token claims.</returns>
        public IEnumerable<Claim> GetTokenClaims(string token=null)
        {
            token = string.IsNullOrEmpty(token) ? GetToken() : token;
            if (string.IsNullOrEmpty(token))
                return null;
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return securityToken.Claims;
        }

        /// <summary>
        /// Reads the user identity of the HttpContext to get the claims.
        /// </summary>
        /// <returns>A list of user claims (null if token is expired).</returns>
        public IEnumerable<Claim> GetIdentityTokenClaims()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            return claims ;
        }

        /// <summary>
        /// Reads the user of the HttpContext to get the claims.
        /// </summary>
        /// <returns>A list of user claims (null if token is expired).</returns>
        public IEnumerable<Claim> GetUserTokenClaims()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            return claims;
        }

        /// <summary>
        /// Get a claim value from the list of claims by the claim name/type.
        /// </summary>
        /// <param name="claimName">The claim name/type in the token.</param>
        /// <param name="token">The token to retrieve the claim value.</param>
        /// <returns>The claim value.</returns>
        public string GetTokenClaimValue(string claimName, string token=null)
        {
            var claims = GetTokenClaims(token);
            var claim = claims?.FirstOrDefault(c => c.Type == claimName);
            return claim?.Value;
        }

        /// <summary>
        /// Allow to get the token from the request header (Authorization Header)
        /// </summary>
        /// <returns>The JWT token.</returns>
        public string GetToken()
        {
            var header = GetRequestHeader(HeaderNames.Authorization);
            var token = header.Replace("Bearer", "").Replace("bearer", "").Trim();
            return token;
        }

        private string GetRequestHeader(string headerName)
        {
            return _httpContextAccessor.HttpContext.Request.Headers[headerName].ToString();
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.Expires)),
                signingCredentials: credentials
            );
            return tokenOptions;
        }
    }
}
