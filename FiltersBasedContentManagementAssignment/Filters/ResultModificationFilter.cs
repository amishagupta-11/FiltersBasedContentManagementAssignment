using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersBasedContentManagementAssignment.Filters
{
    /// <summary>
    /// A filter that modifies the response before it is sent to the client.
    /// Adds a custom header "X-Content-Management" with the value "Filtered".
    /// </summary>
    public class ResultModificationFilter : IResultFilter
    {
        /// <summary>
        /// Called before the result is executed. Adds a custom header to the response.
        /// </summary>
        /// <param name="context">The context for the result being executed.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.TryAdd("X-Content-Management", "Filtered");
        }

        /// <summary>
        /// Called after the result is executed. This method is empty in this filter.
        /// </summary>
        /// <param name="context">The context for the executed result.</param>
        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}
