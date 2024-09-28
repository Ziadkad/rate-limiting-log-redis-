using System.Threading.RateLimiting;
using CI_CD_Pipelines.Entities;
using CI_CD_Pipelines.Services;
using CI_CD_Pipelines.Services.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(rateLimiteroptions =>
    {
        rateLimiteroptions.AddFixedWindowLimiter("fixed", options =>
        {
            options.PermitLimit = 1;
            options.Window = TimeSpan.FromSeconds(5);
            options.QueueLimit = 1;
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        });
        rateLimiteroptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    }
);

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
builder.Services.AddHttpClient();


builder.Services.AddCors(options =>
{
    options.AddPolicy("ZiadAngular",
        corsBuilder => corsBuilder.WithOrigins(builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ZiadAngular");

app.UseRateLimiter();
    
app.UseAuthorization();

app.MapControllers();

app.Run();