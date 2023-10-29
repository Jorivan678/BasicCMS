using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using webapi.Core.DTOs;

namespace webapi.Infrastructure.Filters
{
    public class InputValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate _next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(GetModelStateErrors(context.ModelState));
                return;
            }

            await _next();
        }

        /// <summary>
        /// Method used to return custom responses based on the
        /// <see cref="ModelStateDictionary"/>. This should be used with the HTTP status code
        /// <see cref="StatusCodes.Status400BadRequest"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An enumeration of <see cref="ResponseErrorObject"/> to display validation errors.</returns>
        private static ResponseErrorObject GetModelStateErrors(ModelStateDictionary model)
        {
            var keys = model.Where(x => x.Value!.Errors.Any()).Select(x => x.Key);
            var messages = model.Where(x => x.Value!.Errors.Any()).Select(x => x.Value!.Errors.Select(e => e.ErrorMessage));
            return new ResponseErrorObject("Validation", "Bad Request", StatusCodes.Status400BadRequest, keys, messages);
        }
    }
}
