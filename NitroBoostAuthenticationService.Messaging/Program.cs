// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using NitroBoostAuthenticationService.Data;
using NitroBoostAuthenticationService.Messaging;
using NitroBoostMessagingClient;

var connectionString = args[0] ?? "Host=localhost;User=Postgres;Password=admin;";
var host = args[1] ?? "localhost";
var queue = args[2] ?? "default";

var helper = new TokenHelper(args[3], args[4]);
var context = new NitroBoostAuthenticationContext(
        new DbContextOptionsBuilder<NitroBoostAuthenticationContext>().UseNpgsql(connectionString).Options);
var processor = new MessageProcessor(context, helper);
var consumer = new BaseReceiver(processor, host, queue);

consumer.StartReceiving();
Console.ReadLine();
consumer.StopReceiving();
