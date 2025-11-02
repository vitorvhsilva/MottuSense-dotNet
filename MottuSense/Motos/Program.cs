using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Motos.Application.Interfaces;
using Motos.Application.Services;
using Motos.Domain.Interfaces;
using Motos.Infraestructure.Data.AppData;
using Motos.Infraestructure.Data.Repositories;
using Motos.Presentation.Mappers;
using Motos.Presentation.Security;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(x =>
{
    x.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

builder.Services.AddAutoMapper(typeof(ControllerMapper));

builder.Services.AddTransient<IMotoRepository, MotoRepository>();
builder.Services.AddTransient<IMotoService, MotoService>();

builder.Services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddTransient<ILocalizacaoService, LocalizacaoService>();

builder.Services.AddTransient<IEventoMotoRepository, EventoMotoRepository>();
builder.Services.AddTransient<IEventoMotoService, EventoMotoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "rateLimit", opt =>
    {
        opt.PermitLimit = 20;
        opt.Window = TimeSpan.FromSeconds(60);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); 
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; 
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new QueryStringApiVersionReader("api-version")
    );
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; 
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Motos API",
        Version = "v1",
        Description = "API de Gestão de Motos e Eventos da MottuSense"
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                $"Motos API {desc.GroupName.ToUpperInvariant()}");
        }
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.UseRateLimiter();
app.UseResponseCompression();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }