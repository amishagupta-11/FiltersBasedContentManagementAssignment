using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace FiltersBasedContentManagementAssignment.Filters
{
    /// <summary>
    /// A filter that logs actions of type POST and DELETE.
    /// It writes the action's name to the debug output when the action is executed.
    /// </summary>
    public class LoggingFilter : IActionFilter
    {
        /// <summary>
        /// Called before an action is executed. This method is empty in this filter.
        /// </summary>
        /// <param name="context">The context for the action being executed.</param>
        public void OnActionExecuting(ActionExecutingContext context) { }

        /// <summary>
        /// Called after an action is executed. Logs the action name for POST and DELETE requests.
        /// </summary>
        /// <param name="context">The context for the executed action.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Post || context.HttpContext.Request.Method == HttpMethods.Delete)
            {
                Debug.WriteLine("Action logged: " + context.ActionDescriptor.DisplayName);
            }
        }
    }
}
