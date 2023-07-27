using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using RepositoryContracts;

namespace Services
{
    public class TicketService : ITicketsService
    {
        private readonly ITicketRepository _ticketsRepository;

        public TicketService(ITicketRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<TicketResponse> AddTicket(TicketAddRequest? ticketAddRequest)
        {
            //Check if ticketAddRequest is null
            if (ticketAddRequest == null)
            {
                throw new ArgumentNullException(nameof(ticketAddRequest));
            }

            //Check if TicketNumber is null
            if (ticketAddRequest.TicketNumber == null)
            {
                throw new ArgumentException("TicketNumber cannot be null", nameof(ticketAddRequest));
            }

            //Check if ticketAddRequest model is valid
            ModelValidationHelper.Validate(ticketAddRequest);

            //Check if TicketNumber is duplicated
            var tickets = await _ticketsRepository.GetAllTickets();
            if (tickets.Any(t => t.TicketNumber == ticketAddRequest.TicketNumber))
            {
                throw new ArgumentException("TicketNumber already exists", nameof(ticketAddRequest));
            }

            Ticket ticket = ticketAddRequest.ToTicket();
            await _ticketsRepository.AddTicket(ticket);
            return ticket.ToTicketResponse();
        }

        public async Task<List<TicketResponse>> GetAllTickets()
        {
            var tickets = await _ticketsRepository.GetAllTickets();

            return tickets
              .Select(t => t.ToTicketResponse()).ToList();
        }

        public async Task<TicketResponse?> GetTicketByTicketNumber(int? ticketNumber)
        {
            if (ticketNumber == null)
                return null;

            Ticket? ticket = await _ticketsRepository.GetTicketByTicketNumber(ticketNumber.Value);

            if (ticket == null)
                return null;

            return ticket.ToTicketResponse();
        }

    }
}
