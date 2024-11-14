using FiltersBasedContentManagementAssignment.Data;
using FiltersBasedContentManagementAssignment.Filters;
using FiltersBasedContentManagementAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiltersBasedContentManagementAssignment.Controllers
{
    /// <summary>
    /// Controller for managing content within the application. 
    /// Provides endpoints for creating, retrieving, updating, and deleting content.
    /// Role-based authorization is applied to certain actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ExceptionHandlingFilter))]
    public class ContentController : ControllerBase
    {
        private readonly ContentManagementDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentController"/> class with the specified database context.
        /// </summary>
        /// <param name="context">Database context for accessing content data.</param>
        public ContentController(ContentManagementDbContext context)
        {
            DbContext = context;
        }

        /// <summary>
        /// Creates a new content entry.
        /// Requires "Admin" role for access.
        /// </summary>
        /// <param name="content">The content data to be created.</param>
        /// <returns>Returns a success message and the created content data.</returns>
        [HttpPost]
        [TypeFilter(typeof(RoleAuthorizationFilter), Arguments = ["Admin"])]
        public IActionResult CreateContent(Content content)
        {
            if (string.IsNullOrEmpty(content.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            if (string.IsNullOrEmpty(content.Category))
            {
                throw new ArgumentException("Category cannot be empty.");
            }
            content.CreatedAt = DateTime.UtcNow;
            DbContext.Contents.Add(content);
            DbContext.SaveChanges();
            return Ok(new { message = "Content created", content });
        }

        /// <summary>
        /// Retrieves a content entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the content to retrieve.</param>
        /// <returns>Returns the content data if found, otherwise a not found message.</returns>
        [HttpGet("GetContent/{id}")]
        [TypeFilter(typeof(ResultModificationFilter))]
        public IActionResult GetContentById(int id)
        {
            var content = DbContext.Contents.Find(id);
            return content == null ? throw new KeyNotFoundException() : (IActionResult)Ok(content);
        }

        /// <summary>
        /// Deletes a content entry by its ID.
        /// Requires "Admin" role for access.
        /// </summary>
        /// <param name="id">The ID of the content to delete.</param>
        /// <returns>Returns no content on success or a not found message if the content does not exist.</returns>
        [HttpDelete("DeleteId/{id}")]
        [TypeFilter(typeof(RoleAuthorizationFilter), Arguments = ["Admin"])]
        [ServiceFilter(typeof(LoggingFilter))]
        public IActionResult DeleteContent(int id)
        {
            var content = DbContext.Contents.Find(id)??throw new KeyNotFoundException();
            DbContext.Contents.Remove(content);
            DbContext.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Updates an existing content entry by its ID.
        /// Requires "Admin" role for access.
        /// </summary>
        /// <param name="id">The ID of the content to update.</param>
        /// <param name="updatedContent">The updated content data.</param>
        /// <returns>Returns a success message and the updated content data, or a not found message if the content does not exist.</returns>
        [HttpPost("EditContent/{id}")]
        [TypeFilter(typeof(RoleAuthorizationFilter), Arguments = ["Admin"])]
        public IActionResult UpdateContent(int id, [FromBody] Content updatedContent)
        {
            var content = DbContext.Contents.Find(id)??throw new KeyNotFoundException();

            if (string.IsNullOrEmpty(updatedContent.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }
            content.Title = updatedContent.Title;
            content.Description = updatedContent.Description;
            content.CreatedAt = DateTime.UtcNow;
            //DbContext.Contents.Update(content);
            DbContext.SaveChanges();
            return Ok(new { message = "Content updated", content });
        }
    }
}
