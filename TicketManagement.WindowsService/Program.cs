using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using TicketManagement.WindowsService;
using Microsoft.Extensions.Configuration;
using TicketManagement.WindowsService.Service;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "GA Tech Ticket Import Service";
});

LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

IConfiguration configuration = builder.Configuration;
builder.Services.Configure<TicketImportConfiguration>(configuration.GetSection(nameof(TicketImportConfiguration)));

builder.Services.AddSingleton<ImportTicketService>();
builder.Services.AddHostedService<WindowsBackgroundService>();

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));

IHost host = builder.Build();
host.Run();