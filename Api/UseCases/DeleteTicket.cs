using OrdemServico.Interfaces;
using OrdemServico.Model;

namespace OrdemServico.UseCases
{
    public class DeleteTicket
    {
        private readonly ITicketRepository _ticketRepository;
        public DeleteTicket(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Execute(int id)
        {
            Ticket ticketDb = await _ticketRepository.GetById(id);
            if(ticketDb != null)
                ticketDb.CancelarTicket();

            await _ticketRepository.Delete(ticketDb);
        } 

    }
}