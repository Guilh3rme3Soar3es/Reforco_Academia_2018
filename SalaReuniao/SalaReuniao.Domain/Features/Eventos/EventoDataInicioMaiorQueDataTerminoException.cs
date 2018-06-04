using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoDataInicioMaiorQueDataTerminoException : BusinessException
    {
        public EventoDataInicioMaiorQueDataTerminoException() : base("Evento com data de inicio menor que a data de termino.")
        {
        }
    }
}
