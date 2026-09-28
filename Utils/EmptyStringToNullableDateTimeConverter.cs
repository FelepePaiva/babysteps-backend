using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BabySteps.API.Utils
{
    public class EmptyStringToNullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            
            // Se o JSON da Blip vier como "" ou "null", convertemos para o null nativo do C#
            if (string.IsNullOrWhiteSpace(value) || value.Trim().ToLower() == "null")
            {
                return null;
            }

            // Se vier uma data preenchida, faz o parse normal
            return DateTime.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("o"));
            else
                writer.WriteNullValue();
        }
    }
}