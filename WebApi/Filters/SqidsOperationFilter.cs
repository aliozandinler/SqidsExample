using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Mvc;

namespace WebApi.Filters;

public class SqidsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var parameter in operation.Parameters)
        {
            var parameterDescription = context.ApiDescription.ParameterDescriptions
                .FirstOrDefault(x => x.ModelMetadata.BinderType == typeof(SqidsModelBinder) && x.Name == parameter.Name);

            if (parameterDescription != null)
            {
                parameter.Schema.Format = string.Empty;
                parameter.Schema.Type = "string";
            }
        }
    }
}