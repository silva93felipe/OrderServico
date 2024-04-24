using OrdemServico.Endpoints;
using OrdemServico.Interfaces;
using OrdemServico.Mapper;
using OrdemServico.Repository;
using OrdemServico.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ConfigurationMapping));
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddStackExchangeRedisCache(options => {
    options.InstanceName = "cache_orders";
    options.Configuration = "localhost:6379";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGroup("/ticket").TicketEndpoints();

app.Run();