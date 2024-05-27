using Microsoft.EntityFrameworkCore;
using NitroBoostAuthenticationService.Core;
using NitroBoostAuthenticationService.Data;
using NitroBoostAuthenticationService.Data.Repositories;
using NitroBoostAuthenticationService.Shared.Configurations;
using NitroBoostAuthenticationService.Shared.Interfaces.Messaging;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;
using NitroBoostAuthenticationService.Web.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IBaseSender, BaseSender>(options => new BaseSender(args[4]));
builder.Services.AddSingleton<IBaseReceiver, BaseReceiver>(options =>
{
    BaseReceiver receiver = new BaseReceiver(args[4]);
    receiver.DataReceived += (sender, eventArgs) =>
    {
        new MessageHandler().ProcessMessage(eventArgs.Body.ToArray());
        receiver.AcknowledgeMessage(eventArgs);
    };
    return new BaseReceiver(args[4]);
});
builder.Services.AddSingleton<KeySigningConfiguration>(new KeySigningConfiguration()
{
    Key = args[1],
    Issuer = args[2],
    Audience = args[3]
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