using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using todolist_api.Data;
using todolist_api.Interfaces;
using todolist_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var key = Environment.GetEnvironmentVariable("APPSETTINGS__KEY");
if (string.IsNullOrEmpty(key))
    throw new InvalidOperationException("Brak APPSETTINGS__KEY w zmiennych środowiskowych");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:5005")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)),
            ValidateIssuerSigningKey = true
        };

        // options.Events = new JwtBearerEvents
        // {
        //     OnChallenge = context =>
        //     {
        //         context.HandleResponse();
        //         context.Response.StatusCode = 404;
        //         return Task.CompletedTask;
        //     },
        //     OnForbidden = context =>
        //     {
        //         context.Response.StatusCode = 404;
        //         return Task.CompletedTask;
        //     }
        // };
    });

var TdlDbConnectionString = Environment.GetEnvironmentVariable("TODOLIST_DB_CONNECTION");
if (string.IsNullOrEmpty(TdlDbConnectionString))
    throw new InvalidOperationException("Brak TODOLIST_DB_CONNECTION w zmiennych środowiskowych");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(TdlDbConnectionString)
);


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITodoitemsService, TodoitemsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowVueApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

