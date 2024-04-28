namespace OrdemServico.ViewModel
{
    public record TicketResponse(bool ativo, int id, int equipamentoId, DateTime dataAbertura, string observacao, string status, int setorId);
}