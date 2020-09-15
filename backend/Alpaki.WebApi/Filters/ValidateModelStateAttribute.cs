using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alpaki.WebApi.Filters
{
    public class ValidateModelStateAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                var validationException = context.Exception as ValidationException;
                context.Result = new JsonResult(new { validationException.Errors, validationException.Message })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            base.OnException(context);
        }
    }
}
