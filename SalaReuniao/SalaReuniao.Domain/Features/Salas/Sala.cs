using SalaReuniao.Domain.Common;
using SalaReuniao.Domain.Features.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public class Sala : Entidade
    {
        public Nome Nome { get; set; }
        public int NumeroLugares { get; set; }

        public override void Validar()
        {
            if (NumeroLugares == 0)
                throw new SalaNumeroLugaresNaoInformado();
            if (NumeroLugares < 0)
                throw new SalaNumeroLugaresInvalido();
        }
    }
}
