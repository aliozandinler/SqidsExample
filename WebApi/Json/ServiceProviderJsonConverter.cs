using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Json;

public class ServiceProviderJsonConverter(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider) : JsonConverter<int>, IServiceProvider
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => throw new NotImplementedException();

    public object? GetService(Type serviceType) => (httpContextAccessor.HttpContext?.RequestServices ?? serviceProvider).GetService(serviceType);
}