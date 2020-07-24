using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alpaki.WebApi.Swagger
{
    [ExcludeFromCodeCoverage]
    public class AddAuthorizedHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "JWT token",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = new OpenApiString("Bearer ")
                }
            });
        }
    }
}
