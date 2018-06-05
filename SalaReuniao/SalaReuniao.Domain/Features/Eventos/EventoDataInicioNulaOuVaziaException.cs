using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoFuincionarioNuloException : BusinessException
    {
        public EventoFuincionarioNuloException() : base("Evento com lista de funcionarios nula oi vazia.")
        {
        }
    }
}
