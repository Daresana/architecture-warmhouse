using TemperatureApi.Models;

namespace TemperatureApi.Services
{
    public class TemperatureService
    {
        private readonly Dictionary<string, double> _lastValues = new();
        private readonly object _sync = new();

        public TemperatureResponse GetTemperature(string? location, string? sensorId)
        {
            if (string.IsNullOrEmpty(location))
            {
                location = sensorId switch
                {
                    "1" => "Living Room",
                    "2" => "Bedroom",
                    "3" => "Kitchen",
                    _ => "Unknown"
                };
            }

            if (string.IsNullOrEmpty(sensorId))
            {
                sensorId = location switch
                {
                    "Living Room" => "1",
                    "Bedroom" => "2",
                    "Kitchen" => "3",
                    _ => "0"
                };
            }

            double value;
            lock (_sync)
            {
                // После округления новое показание может совпасть с предыдущим.
                do
                {
                    value = Math.Round(15 + Random.Shared.NextDouble() * 15, 2);
                }
                while (_lastValues.TryGetValue(sensorId, out var previous) && value == previous);
                _lastValues[sensorId] = value;
            }

            return new TemperatureResponse
            {
                Value = value,
                Timestamp = DateTime.UtcNow,
                Location = location,
                SensorId = sensorId
            };
        }
    }
}
