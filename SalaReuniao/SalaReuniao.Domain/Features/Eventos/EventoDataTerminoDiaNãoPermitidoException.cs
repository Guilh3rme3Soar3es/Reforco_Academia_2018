using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoDataTerminoDiaNãoPermitidoException : BusinessException
    {
        public EventoDataTerminoDiaNãoPermitidoException() : base("Evento com data de termino em dia não permitido.")
        {
        }
    }
}
