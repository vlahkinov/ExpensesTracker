using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpenseTracker
{
    public class DateOnlyJsonConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "MM/dd/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? dateString = reader.GetString();
                if (!string.IsNullOrEmpty(dateString) && 
                    DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
            }
            
            // Fallback to default DateTime parsing
            return JsonSerializer.Deserialize<DateTime>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write only the date part in MM/dd/yyyy format
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
} 