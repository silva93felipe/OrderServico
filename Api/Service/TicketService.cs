using AutoMapper;
using Microsoft.OpenApi.Extensions;
using OrdemServico.Enum;
using OrdemServico.Interfaces;
using OrdemServico.Model;
using OrdemServico.ViewModel;
using System.Collections.Generic;

namespace OrdemServico.Service
{
    public class TicketService : ITicketService
    {
        private const string TICKET_STATUS_ATIVO = "1";
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        public async Task<IEnumerable<TicketResponse>> GetAll()
        {
            IEnumerable<Ticket> tickets = await _ticketRepository.GetAll();
            return _mapper.Map<IEnumerable<TicketResponse>>(tickets);
        }
        public async Task Create(TicketRequet ticket)
        {
            Ticket newTicket = new Ticket(ticket.EquipamentoId, ticket.Observacao, ticket.SetorId);
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
            var ticket = await _ticketRepository.GetById(id);
            if(ticket != null)
                return new TicketResponse(ticket.Id, ticket.Ativo, ticket.EquipamentoId, ticket.DataAbertura, ticket.Observacao, TICKET_STATUS_ATIVO, ticket.SetorId);

            return null;
        }
    }
}