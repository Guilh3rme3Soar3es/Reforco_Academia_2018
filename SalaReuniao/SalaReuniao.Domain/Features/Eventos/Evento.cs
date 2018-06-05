using SalaReuniao.Domain.Common;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public class Evento : Entidade
    {
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataTermino { get; set; }
        public Funcionario Funcionario { get; set; }
        public Sala Sala { get; set; }

        public override void Validar()
        {
            if (DataInicio.Hour < 8 || DataInicio.Hour > 20)
                throw new EventoDataInicioForaHorarioDoLimiteException();
            if (DataTermino.Hour < 8 || DataTermino.Hour > 20)
                throw new EventoDataTerminoHorarioForaDoLimiteException();
            if (DataInicio.DayOfWeek == DayOfWeek.Saturday || DataInicio.DayOfWeek == DayOfWeek.Sunday)
                throw new EventoDataInicioDiaNãoPermitidoException();
            if (DataTermino.DayOfWeek == DayOfWeek.Saturday || DataTermino.DayOfWeek == DayOfWeek.Sunday)
                throw new EventoDataTerminoDiaNãoPermitidoException();
            if (DataInicio < DateTime.Now)
                throw new EventoDataInicioInvalidaException();
            if (DataTermino < DateTime.Now)
                throw new EventoDataTerminoInvalidaException();
            if (DataInicio > DataTermino)
                throw new EventoDataInicioMaiorQueDataTerminoException();
            if (Funcionario == null)
                throw new EventoFuincionarioNuloException();
            if (Sala == null)
                throw new EventoSalaNulaException();

        }
    }
}
