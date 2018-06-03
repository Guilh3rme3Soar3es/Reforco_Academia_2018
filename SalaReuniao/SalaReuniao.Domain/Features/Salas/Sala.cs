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
            for (int i = 0; i < Eventos.Count; i++)
            {
                for (int j = 0; j < Eventos.Count; j++)
                {
                    if (Eventos[i].DataInicio.DayOfYear == Eventos[j].DataInicio.DayOfYear)
                    {
                        if (Eventos[i].DataTermino > Eventos[j].DataInicio)
                        {
                            throw new Exception();
                        }
                    }
                }
            }
        }
    }
}
