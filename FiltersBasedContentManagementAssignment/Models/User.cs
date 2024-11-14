using System.ComponentModel.DataAnnotations;

namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents a user in the content management system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user. This is a required field with a maximum length of 50 characters.
        /// </summary>
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the user. This is a required field.
        /// </summary>
        [Required]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user. The role has a maximum length of 20 characters.
        /// </summary>
        [StringLength(20)]
        public string? Role { get; set; }
    }
}
