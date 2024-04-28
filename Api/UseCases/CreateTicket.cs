using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;

namespace OrdemServico.UseCases
{
    public class CreateTicket
    {
        private readonly ITicketRepository _ticketRepository;
        public CreateTicket(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task Execute(TicketRequet ticket)
        {
            Ticket newTicket = new(ticket.EquipamentoId, ticket.Observacao, ticket.SetorId);
            await _ticketRepository.Create(newTicket);
        }
    }
}