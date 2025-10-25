using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
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

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(x => {
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

builder.Services.AddRateLimiter(options => {
    options.AddFixedWindowLimiter(policyName: "rateLimit", opt => {
        opt.PermitLimit = 20;
        opt.Window = TimeSpan.FromSeconds(60);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


builder.Services.AddResponseCompression(options => {
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


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.ExampleFilters();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.UseRateLimiter();
app.UseResponseCompression();
app.MapControllers();

app.Run();
