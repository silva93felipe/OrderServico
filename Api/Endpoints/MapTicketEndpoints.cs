using OrdemServico.Interfaces;
using OrdemServico.ViewModel;


namespace OrdemServico.Endpoints
{
    public static class MapTicketEndpoints 
    {
        public static void TicketEndpoints(this IEndpointRouteBuilder app){
            app.MapGet("/", async (ITicketService _ticketService)=> {
                IEnumerable<TicketResponse> tickets = await _ticketService.GetAll();
                return Results.Ok(tickets);
            });

            app.MapGet("/{id:int}", async (int id, ITicketService _ticketService) => {
                TicketResponse ticket = await _ticketService.GetById(id);
                
                if(ticket != null){
                  return Results.Ok(ticket);
                }
               return Results.NotFound();
            });

            app.MapPost("/encerrar/{id:int}", async (int id, ITicketService _ticketService) => {
                await _ticketService.Encerrar(id);

                return Results.NoContent();
            });

            app.MapPost("/", async (ITicketService _ticketService, TicketRequet ticket) => {
                await _ticketService.Create(ticket);
                return Results.Created();
            });

            app.MapDelete("/", async (int id, ITicketService _ticketService)=> {
                await _ticketService.Delete(id);
                return Results.NoContent();
            });
        }
    }
}