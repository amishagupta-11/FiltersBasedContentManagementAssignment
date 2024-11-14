namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents the settings for JWT authentication, including issuer, audience, and signing key.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the issuer of the JWT token (typically the server issuing the token).
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience for which the JWT token is intended (typically the recipient of the token).
        /// </summary>
        public string? Audience { get; set; }

        /// <summary>
        /// Gets or sets the key used to sign the JWT token, ensuring its authenticity.
        /// </summary>
        public string? Key { get; set; }
    }
}
