using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersBasedContentManagementAssignment.Filters
{
    /// <summary>
    /// Custom exception handling filter to manage specific exceptions across the application.
    /// Catches exceptions like <see cref="KeyNotFoundException"/> and <see cref="ArgumentException"/> 
    /// and returns appropriate HTTP responses.
    /// </summary>
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        /// <summary>
        /// Invoked when an exception occurs during the execution of a controller action.
        /// Handles specific exceptions and returns custom responses, marking them as handled.
        /// </summary>
        /// <param name="context">The exception context containing details of the thrown exception.</param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is KeyNotFoundException)
            {
                // Returns a 404 Not Found response when the specified content is not found.
                context.Result = new NotFoundObjectResult("Content not found.");
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ArgumentException)
            {
                // Returns a 400 Bad Request response for invalid arguments or input data.
                context.Result = new BadRequestObjectResult("Invalid input.");
                context.ExceptionHandled = true;
            }
        }
    }
}
