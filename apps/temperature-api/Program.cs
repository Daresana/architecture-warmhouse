using TemperatureApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TemperatureService>();
var app = builder.Build();

if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
    app.Urls.Add("http://0.0.0.0:8081");

app.MapGet("/temperature", (string? location, string? sensorId, TemperatureService service) =>
    service.GetTemperature(location, sensorId));

// Монолит запрашивает показания датчика по его ID.
app.MapGet("/temperature/{sensorId}", (string sensorId, TemperatureService service) =>
    service.GetTemperature(null, sensorId));

app.Run();
