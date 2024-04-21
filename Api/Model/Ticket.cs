using OrdemServico.Enum;

namespace OrdemServico.Model
{
    public class Ticket
    {
        public bool Ativo { get; private set; }
        public int Id { get; private set; }
        public DateTime DataAbertura { get; private set; }
        public DateTime UpdateAt { get; private set; }
        public DateTime? DataFechamento { get; private set; }
        public int EquipamentoId { get; private set; }
        public int SetorId { get; private set; }
        public string Observacao { get; private set; }
        public EStatusTicket Status {get; private set;}
        private Ticket()
        {
            
        }
        public Ticket(int equipamentoId, string observacao, int setorId)
        {
            Ativo = true;
            DataAbertura = DateTime.Now;
            UpdateAt = DateTime.Now;
            EquipamentoId = equipamentoId;
            Observacao = observacao;
            Status = EStatusTicket.ABERTO;
            SetorId = setorId;
        }
        public void EncerrarTicket(){
            DataFechamento = DateTime.Now;
            UpdateAt = DateTime.Now;
            Status = EStatusTicket.ENCERRADO;
        }

        public void CancelarTicket(){
            UpdateAt = DateTime.Now;
            Ativo = false;
            Status = EStatusTicket.CANCELADO;
        }
    }
}