using Microsoft.EntityFrameworkCore;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using NitroBoostAuthenticationService.Core;
using NitroBoostAuthenticationService.Data;
using NitroBoostAuthenticationService.Data.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;
using NitroBoostAuthenticationService.Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ISaltService, SaltService>();
builder.Services.AddScoped<ISaltRepository, SaltRepository>();
//builder.Services.AddScoped<IBaseSender, BaseSender>(options => new BaseSender(args[4]));

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = args[1];
    options.ClientId = args[2];
    options.Scope = "openid profile email";
    options.OpenIdConnectEvents = new OpenIdConnectEvents()
    {
        OnAccessDenied = async a =>
        {
            Logger.Log(
                $"ACCESS DENIED\r\nPath: {a.Request.Path}\r\nAuthorization: {a.Request.Headers.Authorization}",
                Severity.Warning);
            await Task.Yield();
        },
        OnAuthenticationFailed = async a =>
        {
            Logger.Log($"AUTH FAILED\r\nException: {a.Exception.Message}", Severity.Warning);
            await Task.Yield();
        },
        OnRemoteFailure = async a =>
        {
            Logger.Log($"REMOTE FAILURE\r\nException: {a.Failure.Message}", Severity.Critical);
            await Task.Yield();
        }
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (args.Length > 0)
{
    builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NitroBoostAuthenticationContext>(options =>
        options.UseNpgsql(args[0]));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();