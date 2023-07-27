using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.WindowsService
{
    public class TicketImportConfiguration
    {
        public string? PendingPath { get; set; }
        public string? ProcessedPath { get; set; }
        public string? InsertTicketEndPoint { get; set; }
    }
}

