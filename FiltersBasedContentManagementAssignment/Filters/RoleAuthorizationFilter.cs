using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FiltersBasedContentManagementAssignment.Filters
{
    /// <summary>
    /// A custom authorization filter that checks if the user has the required role.
    /// If the user does not have the required role, they are forbidden access.
    /// </summary>
    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string RequiredRole;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleAuthorizationFilter"/> with the specified required role.
        /// </summary>
        /// <param name="requiredRole">The role required to access the resource.</param>
        public RoleAuthorizationFilter(string requiredRole)
        {
            RequiredRole = requiredRole;
        }

        /// <summary>
        /// Executes the authorization check. If the user does not have the required role, they are forbidden access.
        /// </summary>
        /// <param name="context">The context for the authorization filter.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Debugging: Check the roles available for the current user
            var roles = user.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();
            System.Diagnostics.Debug.WriteLine("User roles: " + string.Join(", ", roles));

            if (!user.Identity.IsAuthenticated || !user.IsInRole(RequiredRole))
            {
                context.Result = new ForbidResult(); // Deny access if role is not matched
            }
        }
    }
}
