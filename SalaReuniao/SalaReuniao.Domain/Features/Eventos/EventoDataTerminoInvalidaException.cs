using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoDataTerminoInvalidaException : BusinessException
    {
        public EventoDataTerminoInvalidaException() : base("Evento com data de termino menor que a data atual.")
        {
        }
    }
}
