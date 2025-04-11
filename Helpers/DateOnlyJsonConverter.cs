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
                
                // Try default DateTime parsing for string
                if (!string.IsNullOrEmpty(dateString) && 
                    DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                
                // If we can't parse it, return default
                return default;
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                // Handle numeric timestamps if needed
                long ticks = reader.GetInt64();
                return new DateTime(ticks);
            }
            
            // For any other token type, just return default
            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Write only the date part in MM/dd/yyyy format
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
} 