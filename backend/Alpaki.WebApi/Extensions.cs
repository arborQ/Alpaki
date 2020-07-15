using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpaki.WebApi
{
    public static class Extensions
    {
        public static ValidationProblemDetails ToValidationProblemDetails(this ValidationException exception)
        {
            var errors = exception.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage)
                        .ToArray()
                );
            return new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest
            };
        }
    }
}