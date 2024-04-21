using Dapper;
using Microsoft.Data.Sqlite;
using OrdemServico.Interfaces;
using OrdemServico.Model;

namespace OrdemServico.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IConfiguration _configuration;

        public TicketRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Create(Ticket ticket)
        {
            await using var connection = new SqliteConnection(_configuration.GetConnectionString("Dev"));
            
            await connection.ExecuteAsync(
                        @"INSERT INTO ticket (DataAbertura, DataFechamento, EquipamentoId, Observacao, Ativo, UpdateAt, Status, SetorId) 
                        VALUES(@DataAbertura, @DataFechamento, @EquipamentoId, @Observacao, @Ativo, @UpdateAt, @Status, @SetorId);", 
                        new { ticket.DataAbertura, ticket.DataFechamento, ticket.EquipamentoId, 
                            ticket.Observacao, ticket.Ativo, ticket.UpdateAt, ticket.Status, ticket.SetorId 
                        }
            );
        }

        public async Task Delete(int id)
        {
            var ticket = await GetById(id);
            if(ticket != null){
                ticket.CancelarTicket();
                await using var connection = new SqliteConnection(_configuration.GetConnectionString("Dev"));
                await connection.ExecuteAsync(@"UPDATE ticket SET Status = @Status, 
                                                UpdateAt = @UpdateAt, Ativo = @Ativo
                                                WHERE Id = @Id;", 
                                                new { ticket.Status, ticket.UpdateAt, ticket.Ativo, ticket.Id });
            }            
        }

        public async Task Encerrar(int id)
        {
            var ticket =  await GetById(id);
            if(ticket != null){
                ticket.EncerrarTicket();
                await using var connection = new SqliteConnection(_configuration.GetConnectionString("Dev"));
                await connection.ExecuteAsync(@"UPDATE ticket SET Status = @Status, 
                                                DataFechamento = @DataFechamento, 
                                                UpdateAt = @UpdateAt
                                                WHERE id = @Id;", 
                                                new { ticket.Status, ticket.DataFechamento, ticket.UpdateAt, ticket.Id });
            }
        }   

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            await using var connection = new SqliteConnection(_configuration.GetConnectionString("Dev"));
            
            return await connection.QueryAsync<Ticket>("SELECT * FROM ticket WHERE Ativo AND Status = 1;");
        }

        public async Task<Ticket?> GetById(int id)
        {
            await using var connection = new SqliteConnection(_configuration.GetConnectionString("Dev"));
            
            return await connection.QueryFirstOrDefaultAsync<Ticket>("SELECT * FROM ticket WHERE id = @id AND Status = 1 AND Ativo;", new { id });            
        }
    }
}