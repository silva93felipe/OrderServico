using System;

namespace OrdemServico.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string message) : base(message) { }
    }
}