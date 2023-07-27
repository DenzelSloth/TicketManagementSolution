namespace TicketManagerTests
{
    public class TicketServiceTest
    {
        private readonly ITicketsService _ticketService;
        private readonly Mock<ITicketRepository> _ticketRepositoryMock;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public TicketServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _ticketRepositoryMock = new Mock<ITicketRepository>();
            _ticketRepository = _ticketRepositoryMock.Object;
            _ticketService = new TicketService(_ticketRepository);
            _testOutputHelper = testOutputHelper;
        }

        //When TicketAddRequest is null, throw ArgumentNullException
        [Fact]
        public async Task AddTicket_NullTicket()
        {
            //Arrange
            TicketAddRequest? ticketAddRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _ticketService.AddTicket(ticketAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When TicketNumber is null, throw ArgumentException
        [Fact]
        public async Task AddTicket_TicketNumberIsNull()
        {
            //Arrange
            TicketAddRequest? ticketAddRequest = _fixture.Build<TicketAddRequest>()
             .With(t => t.TicketNumber, null as int?)
             .Create();

            Ticket ticket = ticketAddRequest.ToTicket();

            //When PersonsRepository.AddPerson is called, it has to return the same "person" object
            _ticketRepositoryMock
             .Setup(t => t.AddTicket(It.IsAny<Ticket>()))
             .ReturnsAsync(ticket);

            //Act
            Func<Task> action = async () =>
            {
                await _ticketService.AddTicket(ticketAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When TicketNumber is duplicated, throw ArgumentException
        [Fact]
        public async Task AddTicket_DuplicateTicketNumber()
        {
            //Arrange
            TicketAddRequest? ticketAddRequestA = _fixture.Build<TicketAddRequest>()
            .With(t => t.TicketNumber, 1122)
            .Create();

            TicketAddRequest? ticketAddRequestB = _fixture.Build<TicketAddRequest>()
            .With(t => t.TicketNumber, 1122)
            .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _ticketService.AddTicket(ticketAddRequestA);
                await _ticketService.AddTicket(ticketAddRequestB);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When fields are valid, insert into list of tickets
        [Fact]
        public async Task AddTicket_ValidTicketFields()
        {
            Ticket ticket = _fixture.Build<Ticket>()
            .With(t => t.TicketNumber, 1234567)
            .Create();
            TicketResponse ticket_response_expected = ticket.ToTicketResponse();

            _ticketRepositoryMock.Setup(t => t.GetTicketByTicketNumber(1234567))
             .ReturnsAsync(ticket);

            //Act
            TicketResponse? ticket_response_from_get = await _ticketService.GetTicketByTicketNumber(ticket.TicketNumber);


            //Assert
            ticket_response_from_get!.FechaHora_Creacion.Should().NotBe(null);
            ticket_response_from_get.Should().Be(ticket_response_expected);

        }

        //When TicketNumber is null, throw ArgumentException
        [Fact]
        public async Task GetTicketByTicketNumber_TicketNumberIsNull()
        {
            //Arrange
            int? ticketNumber = null;

            //Act
            TicketResponse? ticketResponse = await _ticketService.GetTicketByTicketNumber(ticketNumber);

            //Assert
            ticketResponse.Should().BeNull();
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public async Task GetTicketByTicketNumber_TicketNumberToBeSuccessful()
        {
            //Arange
            Ticket ticket = _fixture.Build<Ticket>()
             .With(t => t.TicketNumber, 1234567)
             .Create();
            TicketResponse ticket_response_expected = ticket.ToTicketResponse();

            _ticketRepositoryMock.Setup(t => t.GetTicketByTicketNumber(1234567))
             .ReturnsAsync(ticket);

            //Act
            TicketResponse? ticket_response_from_get = await _ticketService.GetTicketByTicketNumber(ticket.TicketNumber);


            //Assert
            ticket_response_from_get.Should().Be(ticket_response_expected);
        }

        //Getting Tickets should return empty by default
        [Fact]
        public async Task GetAllTickets_Empty()
        {
            //Arrange
            var tickets = new List<Ticket>();
            _ticketRepositoryMock
             .Setup(t => t.GetAllTickets())
             .ReturnsAsync(tickets);

            //Act
            List<TicketResponse> tickets_from_get = await _ticketService.GetAllTickets();

            //Assert
            tickets_from_get.Should().BeEmpty();
        }

        //Gettin Tickets list as expected
        [Fact]
        public async Task GetAllTickets_WithAddedTickets()
        {
            //Arrange
            List<Ticket> tickets = new List<Ticket>() {
            _fixture.Build<Ticket>()
            .With(t => t.TicketNumber, 111)
            .Create(),

            _fixture.Build<Ticket>()
            .With(t => t.TicketNumber, 222)
            .Create(),

            _fixture.Build<Ticket>()
            .With(t => t.TicketNumber, 333)
            .Create()
            };

            List<TicketResponse> ticket_response_list_expected = tickets.Select(t => t.ToTicketResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (TicketResponse ticked_response_from_add in ticket_response_list_expected)
            {
                _testOutputHelper.WriteLine(ticked_response_from_add.ToString());
            }

            _ticketRepositoryMock.Setup(t => t.GetAllTickets()).ReturnsAsync(tickets);

            //Act
            List<TicketResponse> ticket_list_from_get = await _ticketService.GetAllTickets();

            _testOutputHelper.WriteLine("Result:");
            foreach (TicketResponse ticket_response_from_get in ticket_list_from_get)
            {
                _testOutputHelper.WriteLine(ticket_response_from_get.ToString());
            }

            //Assert
            ticket_list_from_get.Should().BeEquivalentTo(ticket_response_list_expected);
        }

    }
}
