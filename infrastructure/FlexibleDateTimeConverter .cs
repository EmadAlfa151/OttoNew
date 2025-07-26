using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OttoNew.infrastructure
{
	public class FlexibleDateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var str = reader.GetString();

			// Try parsing with various formats
			if (DateTimeOffset.TryParseExact(str,
				new[] { "yyyy-MM-dd'T'HH:mm:ss.fffzzz", "yyyy-MM-dd'T'HH:mm:ss.fff+0000" },
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var dto))
			{
				return dto.UtcDateTime;
			}

			throw new JsonException($"Unable to parse DateTime: {str}");
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
			writer.WriteStringValue(value.ToString("O")); // ISO 8601
	}

}
