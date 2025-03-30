using EventManagementAPI.Data;
using EventManagementAPI.Repositories;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using EventManagementAPI.Services.EventManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona controllers
builder.Services.AddControllers();

// Configurando Rate Limiting
//builder.Services.AddRateLimiter(options =>
//{
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//        RateLimitPartition.GetFixedWindowLimiter(
//            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//            factory: partition => new FixedWindowRateLimiterOptions
//            {
//                AutoReplenishment = true,
//                PermitLimit = 10,
//                QueueLimit = 0,
//                Window = TimeSpan.FromMinutes(1)
//            }));

//    options.AddFixedWindowLimiter("fixed", opt =>
//    {
//        opt.PermitLimit = 4;
//        opt.Window = TimeSpan.FromSeconds(12);
//        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        opt.QueueLimit = 2;
//    });

//    options.OnRejected = async (context, cancellationToken) =>
//    {
//        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
//        context.HttpContext.Response.Headers["Retry-After"] = "60";

//        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", cancellationToken);
//    };
//});

// Configuração de Versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Configuração de injeção de dependências
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IEventService, EventService>();

// Configuração de URLs minúsculas
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"Event Management API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.OperationFilter<RemoveVersionParameter>(); // Ajuste para remover o conflito de versões
    options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
});

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configuração de Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
//app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();
app.Run();