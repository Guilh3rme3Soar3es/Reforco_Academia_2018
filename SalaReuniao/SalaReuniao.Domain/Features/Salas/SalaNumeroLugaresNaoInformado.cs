using SalaReuniao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public class SalaNumeroLugaresNaoInformado : BusinessException
    {
        public SalaNumeroLugaresNaoInformado() : base("Sala com numero de lugares nao informado.")
        {
        }
    }
}
