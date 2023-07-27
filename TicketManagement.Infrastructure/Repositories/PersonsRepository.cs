using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;

namespace Repositories
{
    public class TicketsRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;

        public TicketsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            await _db.SP_InsertTicket(ticket);
            return ticket;
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _db.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetTicketByTicketNumber(int ticketNumber)
        {
            return await _db.Tickets.FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
        }
    }
}
