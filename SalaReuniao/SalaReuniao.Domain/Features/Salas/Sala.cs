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
        public IList<Evento> Eventos { get; set; }

        public override void Validar()
        {
            if (NumeroLugares == 0)
                throw new SalaNumeroLugaresNaoInformado();
            if (NumeroLugares < 0)
                throw new SalaNumeroLugaresInvalido();
            for (int i = 0; i < Eventos.Count; i++)
            {
                for (int j = 0; j < Eventos.Count; j++)
                {
                    if (Eventos[i].DataInicio.DayOfYear == Eventos[j].DataInicio.DayOfYear && i != j)
                    {
                        if (Eventos[i].DataTermino > Eventos[j].DataInicio && Eventos[j].DataInicio >= Eventos[i].DataInicio)
                        {
                            throw new SalaEventosNoMesmoHorarioException();
                        }
                    }
                }
            }
        }
    }
}
