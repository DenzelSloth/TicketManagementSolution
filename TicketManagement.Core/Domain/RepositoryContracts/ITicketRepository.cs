using Entities;

namespace RepositoryContracts
{
    /// <summary>
    /// ITicketRepository Interface
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// ITicketRepository AddTicket Method
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        Task<Ticket> AddTicket(Ticket ticket);

        /// <summary>
        /// ITicketRepository UpdateTicket Method
        /// </summary>
        /// <returns></returns>
        Task<List<Ticket>> GetAllTickets();

        /// <summary>
        /// ITicketRepository GetTicketByTicketNumber Method
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <returns></returns>
        Task<Ticket?> GetTicketByTicketNumber(int ticketNumber);
    }
}
