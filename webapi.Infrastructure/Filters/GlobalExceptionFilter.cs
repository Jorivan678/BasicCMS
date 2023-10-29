using webapi.Core.Exceptions;
using webapi.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public virtual void OnException(ExceptionContext excontext)
        {
            var eType = excontext.Exception.GetType();
            if (eType == typeof(BusinessException))
            {
                HandleBusinessException(excontext);
            }
            else if (eType == typeof(NotFoundException))
            {
                HandleNotFoundException(excontext);
            }
            else if (eType == typeof(UnprocessableEntityException))
            {
                HandleUnprocessableEntityException(excontext);
            }
            else if (eType == typeof(ConflictException))
            {
                HandleConflictException(excontext);
            }
            else if (eType == typeof(ForbiddenException))
            {
                HandleForbiddenException(excontext);
            }
            else if (eType == typeof(UnauthorizedException))
            {
                HandleUnauthorizedException(excontext);
            }
            else
            {
                HandleGenericException(excontext);
            }
        }

        protected virtual void HandleBusinessException(ExceptionContext excontext)
        {
            var ex = (excontext.Exception as BusinessException)!;

            var validation = !ex.HasKeysAndValues
                ? new ResponseErrorObject("Validation", "Bad Request", StatusCodes.Status400BadRequest, new string[] { "Error" }, new IEnumerable<string>[] { new string[] { ex.Message } })
                : new ResponseErrorObject("Validation", "Bad Request", StatusCodes.Status400BadRequest, ex.Keys, ex.Values);

            excontext.Result = new BadRequestObjectResult(validation);
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleNotFoundException(ExceptionContext excontext)
        {
            var exception = (excontext.Exception as NotFoundException)!;
            var validation = new ResponseObject("Not Found", "Error", StatusCodes.Status404NotFound, exception.Message);

            excontext.Result = new NotFoundObjectResult(validation);
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleUnprocessableEntityException(ExceptionContext excontext)
        {
            var exception = (excontext.Exception as UnprocessableEntityException)!;
            var validation = new ResponseObject("Unprocessable Entity", "Error", StatusCodes.Status422UnprocessableEntity, exception.Message);

            excontext.Result = new UnprocessableEntityObjectResult(validation);
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleConflictException(ExceptionContext excontext)
        {
            var exception = (excontext.Exception as ConflictException)!;
            var validation = new ResponseObject("Conflict", "Error", StatusCodes.Status409Conflict, exception.Message);

            excontext.Result = new ConflictObjectResult(validation);
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleForbiddenException(ExceptionContext excontext)
        {
            var exception = (excontext.Exception as ForbiddenException)!;
            var validation = new ResponseObject("Forbidden", "Validation", StatusCodes.Status403Forbidden, exception.Message);

            excontext.Result = new ObjectResult(validation)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleUnauthorizedException(ExceptionContext excontext)
        {
            var exception = (excontext.Exception as UnauthorizedException)!;
            var validation = new ResponseObject("Unauthorized", "Validation", StatusCodes.Status401Unauthorized, exception.Message);

            excontext.Result = new UnauthorizedObjectResult(validation);
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            excontext.ExceptionHandled = true;
        }

        protected virtual void HandleGenericException(ExceptionContext excontext)
        {
            var exception = excontext.Exception;
            var response = new ResponseObject("Internal Server Error", "Error", StatusCodes.Status500InternalServerError, exception.Message);

            var objectResult = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
            excontext.Result = objectResult;
            excontext.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            excontext.ExceptionHandled = true;
        }
    }
}