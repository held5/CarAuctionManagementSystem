using CarAuction.Application.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarAuction.API.Converters
{
  internal class AddVehicleRequestDtoConverter : JsonConverter<AddVehicleRequestDto>
  {
    public override AddVehicleRequestDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      using JsonDocument document = JsonDocument.ParseValue(ref reader);

      JsonElement root = document.RootElement;

      if (!root.TryGetProperty("vehicleType", out JsonElement typeProperty))
      {
        throw new JsonException("Missing VehicleType property.");
      }

      var vehicleType = typeProperty.GetString()?.ToLower();

      if (string.IsNullOrEmpty(vehicleType))
      {
        throw new JsonException("VehicleType must not be null or empty.");
      }

      switch (vehicleType)
      {
        case "sedan":
          return JsonSerializer.Deserialize<AddSedanRequestDto>(root.GetRawText(), options);
        case "truck":
          return JsonSerializer.Deserialize<AddTruckRequestDto>(root.GetRawText(), options);
        case "suv":
          return JsonSerializer.Deserialize<AddSuvRequestDto>(root.GetRawText(), options);
        default:
          throw new JsonException($"Unknown vehicle type: {vehicleType}");
      }
    }

    public override void Write(Utf8JsonWriter writer, AddVehicleRequestDto value, JsonSerializerOptions options) => throw new NotImplementedException();
  }
}
