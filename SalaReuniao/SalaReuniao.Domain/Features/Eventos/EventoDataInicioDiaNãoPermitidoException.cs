﻿using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class EventoDataInicioDiaNãoPermitidoException : BusinessException
    {
        public EventoDataInicioDiaNãoPermitidoException() : base("Evento com data de inicio em dia não permitido.")
        {
        }
    }
}
