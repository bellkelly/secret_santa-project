using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecretSanta.Application.Common.Exceptions;

namespace SecretSanta.WebAPI.Filters
{
    /// <summary>
    /// Custom handler to intercept exceptions and set appropriate result for API response.
    /// </summary>
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Custom exception handlers.
        /// </summary>
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);

                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);

                return;
            }

            HandleUnknownException(context);
        }

        /// <summary>
        /// Handle ValidationException and set result as BadRequestObjectResult.
        /// </summary>
        /// <param name="context">An <see cref="ExceptionContext" /></param>
        private static void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;
            var details = new ValidationProblemDetails(exception.Errors)
            {
                // 400 Bad Request
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle invalid ModelState and set result as BadRequestObjectResult.
        /// </summary>
        /// <param name="context">An <see cref="ExceptionContext" /></param>
        private static void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                // 400 Bad Request
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle UnknownException and set result as ObjectResult with 500 status code.
        /// </summary>
        /// <param name="context">An <see cref="ExceptionContext" /></param>
        private static void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occured while processing the request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handle NotFoundException and set result as NotFoundObjectResult.
        /// </summary>
        /// <param name="context">An <see cref="ExceptionContext" /></param>
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
