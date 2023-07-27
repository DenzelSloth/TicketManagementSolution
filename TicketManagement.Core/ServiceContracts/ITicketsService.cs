using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Perosn entity
    /// </summary>
    public interface ITicketsService
    {

        /// <summary>
        /// Adds ticket object to the list of tickets.
        /// </summary>
        /// <param name="ticket">Ticket object to add. </param>
        /// <returns>Returns Ticket after adding it.</returns>
        Task<TicketResponse> AddTicket(TicketAddRequest? ticketAddRequest);


        /// <summary>
        /// Gets all tickets.
        /// </summary>
        /// <returns>Returns a list of TicketResponse</returns>
        Task<List<TicketResponse>> GetAllTickets();

        /// <summary>
        /// Returns a ticket by ticket number.
        /// </summary>
        /// <param name="ticketNumber">Ticket number to search</param>
        /// <returns>Matching ticket</returns>
        Task<TicketResponse?> GetTicketByTicketNumber(int? ticketNumber);

    }
}
