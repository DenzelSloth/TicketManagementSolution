using Entities;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using ServiceContracts.DTO;
using Services.Helpers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text;

namespace TicketManagement.WindowsService.Service;

public sealed class ImportTicketService
{
    private string? InsertEndPoint { get; set; }
    private string? PendingPath { get; set; }
    private string? ProcessedPath { get; set; }

    public void PrepareDirectory(string insertEndPoint, string pendingPath, string processedPath)
    {
        try
        {
            this.InsertEndPoint = insertEndPoint;
            this.PendingPath = pendingPath;
            this.ProcessedPath = Path.Combine(processedPath, "Log");
            Directory.CreateDirectory(this.PendingPath);
            Directory.CreateDirectory(this.ProcessedPath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating directory: {ex.Message}");
        }

    }

    public async Task<bool> ReadTicketFct(string filePath)
    {
        try
        {
            string? ticketFct = File.ReadLines(filePath).FirstOrDefault();
            if (ticketFct == null)
            {
                ErrorImport(filePath);
                return false;
            }
            string[] ticketFctSplitted = ticketFct.Split('|');
            if (ModelValidationHelper.ValidateTicketFromStringArray(ticketFctSplitted))
            {
                if (TicketAddRequest.ParseToTicketResponse(ticketFctSplitted, out TicketAddRequest? ticketResponse))
                {
                    if (ticketResponse != null)
                    {
                        if (await SendInsertTicketRequest(ticketResponse!))
                        {
                            SuccessImport(filePath);
                            return true;
                        };
                    }
                }
            }
            ErrorImport(filePath);
            return false;
        }
        catch(Exception)
        {
            ErrorImport(filePath);
            return false;
        }      
    }

    private async Task<bool> SendInsertTicketRequest(TicketAddRequest ticket)
    {
        try
        {
            using HttpClient httpClient = new();
            string jsonData = JsonSerializer.Serialize(ticket);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(this.InsertEndPoint, content);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void SuccessImport(string filePath)
    {
        string destinationFilePath = Path.Combine(ProcessedPath!, Path.GetFileName(filePath));
        try
        {
            if (File.Exists(filePath))
            {
                File.Move(filePath, destinationFilePath);
            }
        }
        catch (IOException ex)
        {
            throw ex; 
        }
    }

    private void ErrorImport(string filePath)
    {
        string destinationFilePath = Path.Combine(ProcessedPath!, Path.GetFileName(filePath) + "_error" );
        try
        {
            if (File.Exists(filePath))
            {
                File.Move(filePath, destinationFilePath);
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }
}