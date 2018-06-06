using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoEmHorarioOcupadoException : BusinessException
    {
        public EventoEmHorarioOcupadoException() : base("Evento em horario ja reservado.")
        {
        }
    }
}
