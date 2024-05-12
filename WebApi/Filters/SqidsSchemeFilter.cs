using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Json;

namespace WebApi.Filters;

public class SqidsSchemeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        foreach (var property in context.Type.GetProperties())
        {
            var attribute = property.GetCustomAttributes(typeof(JsonConverterAttribute), false).FirstOrDefault() as JsonConverterAttribute;
            if (attribute?.ConverterType == typeof(SqidsJsonConverter<int>) || attribute?.ConverterType == typeof(SqidsJsonConverter<int?>))
            {
                var keyName = char.ToLower(property.Name[0]) + property.Name[1..];
                if (schema.Properties.TryGetValue(keyName, out OpenApiSchema? value))
                {
                    value.Format = string.Empty;
                    value.Type = "string";
                }
            }
        }
    }
}