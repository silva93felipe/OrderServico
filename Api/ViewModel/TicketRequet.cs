namespace OrdemServico.ViewModel
{
    public record TicketRequet( DateTime DataAbertura,  DateTime? DataFechamento, int EquipamentoId,  string Observacao, int SetorId );
}