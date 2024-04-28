using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;

namespace OrdemServico.UseCases
{
    public class GetTicketById
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository ;
        private const string ORDERS_KEY = "Orders";
        public GetTicketById(IDistributedCache distributedCache, IMapper mapper, ITicketRepository ticketRepository)
        {
            _distributedCache = distributedCache;
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketResponse?> Execute(int id){
            var ordersCache = await _distributedCache.GetStringAsync($"{ORDERS_KEY}_{id}");
            
            Ticket? ticket = null;
            if(string.IsNullOrEmpty(ordersCache) == false){
                ticket =  JsonConvert.DeserializeObject<Ticket>(ordersCache);
                return _mapper.Map<TicketResponse>(ticket);
            }
            ticket = await _ticketRepository.GetById(id);
            if(ticket != null){
                await _distributedCache.SetStringAsync($"{ORDERS_KEY}_{id}", JsonConvert.SerializeObject(ticket));
                return _mapper.Map<TicketResponse>(ticket);
            }

            return null;
        }
    }
}