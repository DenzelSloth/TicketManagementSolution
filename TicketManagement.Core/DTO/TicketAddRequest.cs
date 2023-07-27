using Entities;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a ticket
    /// </summary>
    public class TicketAddRequest
    {
        [Required(ErrorMessage = "Id_Tienda can't be null.")]
        public string? Id_Tienda { get; set; }

        [Required(ErrorMessage = "Id_Registradora can't be null.")]
        public string? Id_Registradora { get; set; }

        [Required(ErrorMessage = "FechaHora can't be null.")]
        public DateTime? FechaHora { get; set; }

        [Required(ErrorMessage = "TicketNumber can't be null.")]
        public int? TicketNumber { get; set; }

        [Required(ErrorMessage = "Impuesto can't be null.")]
        public decimal? Impuesto { get; set; }

        [Required(ErrorMessage = "Total can't be null.")]
        public decimal? Total { get; set; }

        public Ticket ToTicket()
        {
            return new Ticket()
            {
                Id_Tienda = Id_Tienda,
                Id_Registradora = Id_Registradora,
                FechaHora = FechaHora,
                TicketNumber = TicketNumber,
                Impuesto = Impuesto,
                Total = Total
            };
        }

        public static bool ParseToTicketResponse(string[] ticketFct, out TicketAddRequest? ticketAddRequest)
        {
            ticketAddRequest = null;
            TicketAddRequest ticketResponse = new TicketAddRequest();
            ticketResponse.Id_Tienda = ticketFct[0].Substring(3);
            ticketResponse.Id_Registradora = ticketFct[1];

            if (DateTime.TryParseExact(ticketFct[2] + ticketFct[3], "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDateTime))
                ticketResponse.FechaHora = parsedDateTime;
            else
                return false;

            if (int.TryParse(ticketFct[4], out int ticketNumber))
                ticketResponse.TicketNumber = ticketNumber;
            else
                return false;

            if (decimal.TryParse(ticketFct[5].Trim(), out decimal impuesto))
                ticketResponse.Impuesto = impuesto;
            else
                return false;

            if (decimal.TryParse(ticketFct[6].Trim(), out decimal total))
                ticketResponse.Total = total;
            else
                return false;

            ticketAddRequest = ticketResponse;
            return true;
        }

    }
}
