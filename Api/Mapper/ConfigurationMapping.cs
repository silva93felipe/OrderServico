using AutoMapper;
using OrdemServico.Model;
using OrdemServico.ViewModel;

namespace OrdemServico.Mapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Ticket, TicketResponse>()
                .ReverseMap();
        }
    }
}