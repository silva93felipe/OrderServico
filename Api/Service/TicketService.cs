using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace OrdemServico.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private const string ORDERS_KEY = "Orders";
        public TicketService(ITicketRepository ticketRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _distributedCache = distributedCache;
        }
        public async Task<IEnumerable<TicketResponse>> GetAll()
        {
            var ordersCache = await _distributedCache.GetStringAsync(ORDERS_KEY);   
            if(string.IsNullOrEmpty(ordersCache) == false){
                return  JsonConvert.DeserializeObject<IEnumerable<TicketResponse>>(ordersCache);
            }else{
                IEnumerable<Ticket> tickets = await _ticketRepository.GetAll();
                await _distributedCache.SetStringAsync($"{ORDERS_KEY}", JsonConvert.SerializeObject(tickets));
                return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
            }
        }
        public async Task Create(TicketRequet ticket)
        {
            Ticket newTicket = new(ticket.EquipamentoId, ticket.Observacao, ticket.SetorId);
            await _ticketRepository.Create(newTicket);
        }

        public async Task Delete(int id)
        {
            await _ticketRepository.Delete(id);
        }

        public async Task Encerrar(int id)
        {
            await _ticketRepository.Encerrar(id);
        }

        public async Task<TicketResponse?> GetById(int id)
        {  
            var ordersCache = await _distributedCache.GetStringAsync($"{ORDERS_KEY}_{id}");
            Ticket? ticket = null;
            if(string.IsNullOrEmpty(ordersCache) == false){
                return JsonConvert.DeserializeObject<TicketResponse>(ordersCache);
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