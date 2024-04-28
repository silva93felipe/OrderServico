using OrdemServico.Interfaces;

namespace OrdemServico.UseCases
{
    public class EncerrarTicket
    {
        private readonly ITicketRepository _ticketRepository;
        public EncerrarTicket(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Execute(int id){
            var ticket =  await _ticketRepository.GetById(id);
            if (ticket != null){
               ticket.EncerrarTicket(); 
               await _ticketRepository.Encerrar(ticket);
            }
        }
    }
}