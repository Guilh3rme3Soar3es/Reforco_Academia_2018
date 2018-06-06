using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public class SalaRelacionadaComEventoException : BusinessException
    {
        public SalaRelacionadaComEventoException() : base("Sala ralacionada com um evento.")
        {
        }
    }
}
