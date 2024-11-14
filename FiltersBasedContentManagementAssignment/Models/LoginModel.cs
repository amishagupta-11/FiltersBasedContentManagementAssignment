namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents the model used for user login, containing the username and password.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username of the user attempting to log in.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        public string? Password { get; set; }
    }
}
