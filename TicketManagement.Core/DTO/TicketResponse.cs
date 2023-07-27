using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that returns a Ticke
    /// </summary>
    public class TicketResponse
    {
        public string? Id_Tienda { get; set; }
        public string? Id_Registradora { get; set; }
        public DateTime? FechaHora { get; set; }
        public int? TicketNumber { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaHora_Creacion { get; set; }

        /// <summary>
        /// Compares TicketResponse object data with another object data.
        /// </summary>
        /// <param name="obj">TicketResponse object to compare.</param>
        /// <returns>Returns true if all fields match.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TicketResponse ticket = (TicketResponse)obj;

            return Id_Tienda == ticket.Id_Tienda &&
                Id_Registradora == ticket.Id_Registradora &&
                FechaHora == ticket.FechaHora &&
                TicketNumber == ticket.TicketNumber &&
                Impuesto == ticket.Impuesto &&
                Total == ticket.Total &&
                FechaHora_Creacion == ticket.FechaHora_Creacion;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

public static class TicketResponseExtensions
{
    /// <summary>
    /// Extension method that converts a Ticket object to a TicketResponse object.
    /// </summary>
    /// <param name="ticket">Ticke object to parse</param>
    /// <returns>Parsed object</returns>
    public static TicketResponse ToTicketResponse(this Ticket ticket)
    {
        return new TicketResponse()
        {
            Id_Tienda = ticket.Id_Tienda,
            Id_Registradora = ticket.Id_Registradora,
            FechaHora = ticket.FechaHora,
            TicketNumber = ticket.TicketNumber,
            Impuesto = ticket.Impuesto,
            Total = ticket.Total,
            FechaHora_Creacion = ticket.FechaHora_Creacion
        };
    }
}
