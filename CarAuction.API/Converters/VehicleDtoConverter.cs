using CarAuction.Application.Dtos;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CarAuction.API.Converters
{
  internal class VehicleDtoConverter : JsonConverter<VehicleDto>
  {
    public override VehicleDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, VehicleDto value, JsonSerializerOptions options)
      => JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
  }
}
