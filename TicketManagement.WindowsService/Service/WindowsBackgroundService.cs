using Microsoft.Extensions.Options;
using TicketManagement.WindowsService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketManagement.WindowsService.Service;

public sealed class WindowsBackgroundService : BackgroundService
{
    private readonly ImportTicketService _importTicketService;
    private readonly ILogger<WindowsBackgroundService> _logger;
    private readonly IOptions<TicketImportConfiguration> _options;

    public WindowsBackgroundService(ImportTicketService importTicketService, ILogger<WindowsBackgroundService> logger, IOptions<TicketImportConfiguration> options)
    {
        _importTicketService = importTicketService;
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        if (_options == null)
        {
            _logger.LogError("Error reading configuration files.");
            Environment.Exit(1);
        }

        if (_options.Value.PendingPath == null)
        {
            _logger.LogError("Error reading PendingPath configuration.");
            Environment.Exit(1);
        }

        if (_options.Value.ProcessedPath == null)
        {
            _logger.LogError("Error reading ProcessedPath configuration.");
            Environment.Exit(1);
        }

        try
        {
            _importTicketService.PrepareDirectory(_options.Value.InsertTicketEndPoint!, _options.Value.PendingPath!, _options.Value.ProcessedPath!);
            string directoryPath = _options.Value.PendingPath!;
            _logger.LogInformation("Import Directory: {importPath}", _options.Value.PendingPath!);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Started Import Ticket Service.");

                if (!Directory.Exists(directoryPath))
                {
                    throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
                }

                foreach (string filePath in Directory.EnumerateFiles(directoryPath))
                {
                    if (File.Exists(filePath))
                    {
                        if (await _importTicketService.ReadTicketFct(filePath))
                        {
                            _logger.LogInformation("Successfully imported file: {filePath}", filePath);
                        }
                        else
                        {
                            _logger.LogError("Error importing file: {filePath}", filePath);
                        }
                    }
                }
                _logger.LogInformation("Completed Import Ticket Service.");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        catch (TaskCanceledException)
        {
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);
            Environment.Exit(1);
        }
    }
}