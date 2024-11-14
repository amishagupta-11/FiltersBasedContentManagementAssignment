namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents the model used to assign a role to a user.
    /// </summary>
    public class RoleAssignmentModel
    {
        /// <summary>
        /// Gets or sets the username of the user to whom the role will be assigned.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the role to be assigned to the user.
        /// </summary>
        public string? Role { get; set; }
    }
}
