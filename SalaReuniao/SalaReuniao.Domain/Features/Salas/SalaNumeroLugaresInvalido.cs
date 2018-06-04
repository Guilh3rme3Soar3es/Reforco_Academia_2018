using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public class SalaNumeroLugaresInvalido : BusinessException
    {
        public SalaNumeroLugaresInvalido() : base("Sala com numero de lugares invalido.")
        {
        }
    }
}
