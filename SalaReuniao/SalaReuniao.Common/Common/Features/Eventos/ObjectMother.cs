using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Common.Common.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Evento GetNovoEventoOk(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 14:00:00"),
                DataTermino = DateTime.Parse("24/03/2020 16:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoExistenteOk(Funcionario funcionario)
        {
            return new Evento
            {
                Id = 1,
                DataInicio = DateTime.Now.AddDays(+1),
                DataTermino = DateTime.Now.AddDays(+2),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataInicioHorarioForaDoLimite(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 22:00:00"),
                DataTermino = DateTime.Parse("25/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataTerminoHorarioForaDoLimite(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 19:00:00"),
                DataTermino = DateTime.Parse("24/03/2020 22:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataTerminoDiaNãoPermitido(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 19:00:00"),
                DataTermino = DateTime.Parse("29/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataInicioDiaNãoPermitido(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("29/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataInicioInvalida(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("29/05/2018 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataTerminoInvalida(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("29/05/2018 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComDataInicioMaiorQueDataTermino(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("31/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario> { funcionario }
            };
        }

        public static Evento GetEventoInvalidoComListaFuncionariosNula()
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("31/03/2020 20:00:00"),
                Funcionarios = null
            };
        }

        public static Evento GetEventoInvalidoComListaFuncionariosVazia()
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("31/03/2020 20:00:00"),
                Funcionarios = new List<Funcionario>()
            };
        }
    }
}
