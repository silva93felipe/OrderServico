using OrdemServico.ViewModel;

namespace OrdemServico.Interfaces
{
    public interface ITicketService
    {
        public Task<IEnumerable<TicketResponse>> GetAll();
        public Task<TicketResponse?> GetById(int id);
        public Task Delete(int id);
        public Task Create(TicketRequet ticket);
        public Task Encerrar(int id);
    }
}