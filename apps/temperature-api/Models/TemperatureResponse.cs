using System.Text.Json.Serialization;

namespace TemperatureApi.Models
{
    public class TemperatureResponse
    {
        public double Value { get; set; }
        public string Unit { get; set; } = "°C";
        public DateTime Timestamp { get; set; }
        public string Location { get; set; } = "";
        public string Status { get; set; } = "active";

        [JsonPropertyName("sensor_id")]
        public string SensorId { get; set; } = "";

        [JsonPropertyName("sensor_type")]
        public string SensorType { get; set; } = "temperature";

        public string Description { get; set; } = "Temperature sensor";
    }
}
