using RoomHub.Api.Features;
using RoomHub.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("RoomHub") ?? "Data Source=roomhub.db");
builder.Services.AddFeatures();

var app = builder.Build();

app.Services.EnsureDatabaseCreated();

app.MapGet("/", () => "RoomHub API is running.");
app.MapFeatureEndpoints();

app.Run();
