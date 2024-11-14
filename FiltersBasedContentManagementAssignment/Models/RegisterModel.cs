namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents the model used for user registration, containing the username and password.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the username chosen by the user for registration.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password chosen by the user for registration.
        /// </summary>
        public string? Password { get; set; }
    }
}

