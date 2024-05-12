using System.Text.Json.Serialization;
using WebApi.Json;

namespace WebApi.Models;

public class UserModel
{
    [JsonConverter(typeof(SqidsJsonConverter<int>))]
    public int Id { get; set; }

    [JsonConverter(typeof(SqidsJsonConverter<int?>))]
    public int? NullableId { get; set; }

    public string Name { get; set; }
}