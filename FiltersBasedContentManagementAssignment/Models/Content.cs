using System;
using System.ComponentModel.DataAnnotations;

namespace FiltersBasedContentManagementAssignment.Models
{
    /// <summary>
    /// Represents the content entity with properties such as Title, Description, Category, and CreatedAt.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Gets or sets the unique identifier for the content.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the content. This field is required and has a maximum length of 100 characters.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the content.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the category of the content. This field is required and has a maximum length of 50 characters.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the content. Defaults to the current UTC date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
