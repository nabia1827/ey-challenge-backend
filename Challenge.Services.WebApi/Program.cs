using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Serilog;
using System.Text.Json.Serialization;

using Serilog.Events;
using Challenge.Services.WebApi.Modules.Feature;
using Challenge.Services.WebApi.Modules.HealthCheck;
using Challenge.Services.WebApi.Modules.Injection;
using Challenge.Services.WebApi.Modules.Logger;
using Challenge.Services.WebApi.Modules.Mapper;
using Challenge.Services.WebApi.Modules.Swagger;
using Challenge.Services.WebApi.Modules.Versioning;
using Challenge.Services.WebApi.Modules.Authentication;



var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration) // Cargar configuración desde appsettings.json
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddMapper();
builder.Services.AddFeature(builder.Configuration);
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddVersioning();
builder.Services.AddHealthCheck(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("policyApiEy",
                                                builder => builder.WithOrigins(
                                                "https://psei-sie-app-f0hmhdccbsgce7gu.brazilsouth-01.azurewebsites.net"
                                                ,"http://localhost:5173"

                                                )
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod()
                                                            .WithExposedHeaders("Content-Disposition")));

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
}).AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    options.PayloadSerializerOptions.Encoder = null;
    options.PayloadSerializerOptions.IncludeFields = false;
    options.PayloadSerializerOptions.IgnoreReadOnlyFields = false;
    options.PayloadSerializerOptions.IgnoreReadOnlyProperties = false;
    options.PayloadSerializerOptions.MaxDepth = 0;
    options.PayloadSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
    options.PayloadSerializerOptions.DictionaryKeyPolicy = null;
    options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    options.PayloadSerializerOptions.PropertyNameCaseInsensitive = false;
    options.PayloadSerializerOptions.DefaultBufferSize = 32_768;
    options.PayloadSerializerOptions.ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip;
    options.PayloadSerializerOptions.ReferenceHandler = null;
    options.PayloadSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
    options.PayloadSerializerOptions.WriteIndented = true;
    Console.WriteLine($"Number of default JSON converters: {options.PayloadSerializerOptions.Converters.Count}");
});

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APP");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("policyApiEy");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapHub<MainHub>("hubs/notifications");
});

app.Run();