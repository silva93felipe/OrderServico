
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;

namespace OrdemServico.UseCases
{
    public class GetTickets
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        private const string ORDERS_KEY = "Orders";
        public GetTickets(ITicketRepository ticketRepository, IDistributedCache distributedCache, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TicketResponse>> Execute(){
            var ordersCache = await _distributedCache.GetStringAsync(ORDERS_KEY);  
             IEnumerable<Ticket> tickets = []; 
            if(string.IsNullOrEmpty(ordersCache) == false){
                tickets =  JsonConvert.DeserializeObject<List<Ticket>>(ordersCache);
                return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
            }
            tickets = await _ticketRepository.GetAll();
            await _distributedCache.SetStringAsync($"{ORDERS_KEY}", JsonConvert.SerializeObject(tickets));
            return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
        }
    }
}