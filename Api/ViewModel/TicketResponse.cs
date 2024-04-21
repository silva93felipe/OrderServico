namespace OrdemServico.ViewModel
{
    public record TicketResponse(int id, bool ativo, int equipamentoId, DateTime dataAbertura, string observacao, string status, int setorId);
}