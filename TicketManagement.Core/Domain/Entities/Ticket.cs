using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// Domain model for Ticket
    /// </summary>
    [Keyless]
    public class Ticket
    {
        [Required]
        [StringLength(10)]
        public string? Id_Tienda { get; set; }

        [Required]
        [StringLength(10)]
        public string? Id_Registradora { get; set; }

        [Required]
        public DateTime? FechaHora { get; set; }

        [Required]
        [Column("Ticket")]
        public int? TicketNumber { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal? Impuesto { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [Required]
        public DateTime? FechaHora_Creacion { get; set; }
    }
}
