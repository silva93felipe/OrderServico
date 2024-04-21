using Microsoft.AspNetCore.Diagnostics;
using OrdemServico.Model;
using OrdemServico.Enum;
using Xunit;

namespace OrderServico.Model
{
    public class TicketTest
    {
        [Fact]
        public void Ticket_QuandoAbertoDeveTerStatusAberto()
        {
            int equipamentoId = 1;
            string observacao = "A tela está piscando.";
            int setorId = 1;

            Ticket newTicket = new Ticket(equipamentoId, observacao, setorId);

            Assert.Equal(EStatusTicket.ABERTO,  newTicket.Status);
        }

        [Fact]
        public void Ticket_QuandoFechadoDeveTerStatusFechado()
        {
            int equipamentoId = 1;
            string observacao = "A tela está piscando.";
            int setorId = 1;

            Ticket newTicket = new Ticket(equipamentoId, observacao, setorId);
            newTicket.EncerrarTicket();

            Assert.Equal(EStatusTicket.ENCERRADO,  newTicket.Status);
        }
    }
}