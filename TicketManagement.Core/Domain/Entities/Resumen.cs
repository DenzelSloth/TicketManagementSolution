using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Domain model for Resumen
    /// </summary>
    [Keyless]
    public class Resumen
    {
        [Required]
        [StringLength(10)]
        public string? Id_Tienda { get; set; }

        [Required]
        [StringLength(10)]
        public string? Id_Registradora { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Tickets{ get; set; }
    }
}
