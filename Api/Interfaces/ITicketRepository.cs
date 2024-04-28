using OrdemServico.Model;

namespace OrdemServico.Interfaces
{
    public interface ITicketRepository
    {
        public Task<IEnumerable<Ticket>> GetAll();
        public Task<Ticket?> GetById(int id);
        public Task Delete(int id);
        public Task Delete(Ticket id);
        public Task Create(Ticket ticket);
        public Task Encerrar(int id);
        public Task Encerrar(Ticket id);
    }
}