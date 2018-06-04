using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoDataInicioForaHorarioDoLimiteException : BusinessException
    {
        public EventoDataInicioForaHorarioDoLimiteException() : base("Evento com data inicio em horario não permitido.")
        {
        }
    }
}
