using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;


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
            
                // TODO
               IEnumerable<Ticket> tickets =  JsonConvert.DeserializeObject<List<Ticket>>(ordersCache);
                return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
            }else{
                IEnumerable<Ticket> tickets = await _ticketRepository.GetAll();
                return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
            }
        }
        public async Task Create(TicketRequet ticket)
        {
            Ticket newTicket = new Ticket(ticket.EquipamentoId, ticket.Observacao, ticket.SetorId);
            await _ticketRepository.Create(newTicket);
            await _distributedCache.SetStringAsync(ORDERS_KEY, JsonConvert.SerializeObject(newTicket));
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
            Ticket ticket = await _ticketRepository.GetById(id);
            if(ticket != null)
                return _mapper.Map<TicketResponse>(ticket);
                //return new TicketResponse(ticket.Id, ticket.Ativo, ticket.EquipamentoId, ticket.DataAbertura, ticket.Observacao, TICKET_STATUS_ATIVO, ticket.SetorId);

            return null;
        }
    }
}