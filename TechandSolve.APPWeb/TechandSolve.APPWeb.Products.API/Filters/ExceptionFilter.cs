using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechAndSolve.APPWeb.Products.API.Filters;

public class ExceptionFilter
    (ILogger<ExceptionFilter> logger): IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "An unhandled exception occurred");

        context.ExceptionHandled = true;

        if (context.Exception is FluentValidation.ValidationException validationException)
        {
            var errors = validationException
                .Errors
                .Select(e => new { e.PropertyName, e.ErrorMessage });

            context.Result = new BadRequestObjectResult(errors);

            return;
        }

        if (context.Exception is KeyNotFoundException keyNotFoundException)
        {
            context.Result = new NotFoundObjectResult(keyNotFoundException.Message);

            return;
        }

        context.Result = new ObjectResult("An unexpected error occurred. Please try again later.")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}