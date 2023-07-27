using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Resumen>().ToTable("Resumen");

            modelBuilder.Entity<Ticket>()
            .HasIndex(u => u.TicketNumber)
            .IsUnique();

            modelBuilder.Entity<Resumen>()
            .HasIndex(p => new { p.Id_Tienda, p.Id_Registradora })
            .IsUnique();
        }

        public async Task<int> SP_InsertTicket(Ticket ticket)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id_Tienda", ticket.Id_Tienda),
                new SqlParameter("@Id_Registradora", ticket.Id_Registradora),
                new SqlParameter("@FechaHora", ticket.FechaHora),
                new SqlParameter("@TicketNumber", ticket.TicketNumber),
                new SqlParameter("@Impuesto", ticket.Impuesto),
                new SqlParameter("@Total", ticket.Total),
            };
            return await Database.ExecuteSqlRawAsync("[dbo].[SP_Ticket_InsertTicket] @Id_Tienda, @Id_Registradora, @FechaHora, @TicketNumber, @Impuesto, @Total", parameters);
        }
    }
}
