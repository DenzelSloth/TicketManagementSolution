using CitiesManager.WebAPI.Controllers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Net.Sockets;

namespace TicketManagementSolution.WebAPI.Controllers.v1
{
    /// <summary>
    /// Ticket REST Controller
    /// </summary>\
    public class TicketController : CustomControllerBase
    {
        private readonly ITicketsService _ticketService;
        private readonly ITicketRepository _ticketsRepository;

        /// <summary>
        /// Ticket Controller Constructor DI
        /// </summary>
        /// <param name="ticketsRepository"></param>
        public TicketController(ITicketRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _ticketService = new TicketService(_ticketsRepository);
        }

        // GET: api/Ticket
        /// <summary>
        /// Get the list of all Tickets.
        /// </summary>
        /// <returns>list of all Tickets.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketResponse>>> GetTickets()
        {
            return await _ticketService.GetAllTickets();
        }

        // GET: api/Ticket/1
        /// <summary>
        /// Get the a Tickets item that matches the Ticket Number.
        /// </summary>
        /// <param name="ticketNumber">Ticket number to search.</param>
        /// <returns></returns>
        [HttpGet("{ticketNumber}")]
        public async Task<ActionResult<TicketResponse>> GetTicket(int? ticketNumber)
        {
            if (ticketNumber == null)
            {
                return Problem(detail: "Ticket Number is Empty", statusCode: 400, title: "Ticket Search By Ticket Number");
            }

            TicketResponse? ticketFiltered = await _ticketService.GetTicketByTicketNumber(ticketNumber);

            if (ticketFiltered == null)
            {
                return Problem(detail: "Ticket Number Not Found", statusCode: 404, title: "Ticket Search By Ticket Number");
            }
            return ticketFiltered;
        }

        // POST: api/Ticket
        /// <summary>
        /// Insets Ticket into the ticket list.
        /// </summary>
        /// <param name="ticket">Ticket to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TicketResponse>> PostTicket(
            [Bind(nameof(TicketAddRequest.Id_Tienda),
            nameof(TicketAddRequest.Id_Registradora),
            nameof(TicketAddRequest.FechaHora),
            nameof(TicketAddRequest.TicketNumber),
            nameof(TicketAddRequest.Impuesto),
            nameof(TicketAddRequest.Total))] TicketAddRequest ticket)
        {
            if (_ticketService == null)
            {
                return Problem("Error reaching Ticket Service.");
            }

            await _ticketService.AddTicket(ticket);
            TicketResponse? ticketCreated = await _ticketService.GetTicketByTicketNumber(ticket.TicketNumber);
            return CreatedAtAction(nameof(GetTicket), new { ticketNumber = ticket.TicketNumber }, ticketCreated);
        }
    }
}
