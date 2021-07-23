namespace BasicJwtService.Settings
{
    /// <summary>
    /// The token configuration properties obtained from the project configurations (appsettings.json).
    /// </summary>
    public class JwtSettings
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateIssuer'
        public bool ValidateIssuer { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateIssuer'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateAudience'
        public bool ValidateAudience { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateAudience'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateLifetime'
        public bool ValidateLifetime { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateLifetime'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateIssuerSigningKey'
        public bool ValidateIssuerSigningKey { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.ValidateIssuerSigningKey'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Issuer'
        public string Issuer { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Issuer'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Audience'
        public string Audience { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Audience'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Expires'
        public int Expires { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Expires'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Secret'
        public string Secret { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'JwtSettings.Secret'
    }
}
