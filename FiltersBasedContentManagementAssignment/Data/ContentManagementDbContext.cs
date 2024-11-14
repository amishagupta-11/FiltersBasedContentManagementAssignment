using FiltersBasedContentManagementAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace FiltersBasedContentManagementAssignment.Data
{
    /// <summary>
    /// Database context for the content management system.
    /// Provides access to the Content and User entities in the database.
    /// </summary>
    public class ContentManagementDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for Content entities.
        /// Represents the collection of content items stored in the database.
        /// </summary>
        public DbSet<Content> Contents { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for User entities.
        /// Represents the collection of users stored in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagementDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public ContentManagementDbContext(DbContextOptions<ContentManagementDbContext> options) : base(options) { }
    }
}
