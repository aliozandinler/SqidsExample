using System.Text.Json;
using System.Text.Json.Serialization;
using Sqids;

namespace WebApi.Json;

public class SqidsJsonConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                string? stringValue = reader.GetString();
                IReadOnlyList<int> sqid = GetSqids(options).Decode(stringValue);

                if (sqid.Count == 0)
                    throw new JsonException("Invalid sqid.");

                return (T)(object)sqid[0];

            case JsonTokenType.Number:
                return (T)(object)reader.GetInt32();

            default:
                throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value != null)
            writer.WriteStringValue(GetSqids(options).Encode((int)(object)value));
    }

    private SqidsEncoder<int> GetSqids(JsonSerializerOptions options)
    {
        var serviceProvider = options.Converters.OfType<IServiceProvider>().FirstOrDefault() ?? throw new InvalidOperationException("No service provider found in JSON converters");
        return serviceProvider.GetRequiredService<SqidsEncoder<int>>();
    }
}