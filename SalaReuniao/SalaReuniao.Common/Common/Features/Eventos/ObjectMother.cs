using SalaReuniao.Domain.Features.Eventos;
using SalaReuniao.Domain.Features.Funcionarios;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Common.Common.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Evento RetorneNovoEventoOk(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 14:00:00"),
                DataTermino = DateTime.Parse("24/03/2020 16:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoExistenteOk(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                Id = 1,
                DataInicio = DateTime.Parse("24/03/2020 14:00:00"),
                DataTermino = DateTime.Parse("24/03/2020 16:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataInicioHorarioForaDoLimite(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 22:00:00"),
                DataTermino = DateTime.Parse("25/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataTerminoHorarioForaDoLimite(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 19:00:00"),
                DataTermino = DateTime.Parse("24/03/2020 22:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataTerminoDiaNãoPermitido(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("24/03/2020 19:00:00"),
                DataTermino = DateTime.Parse("29/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataInicioDiaNãoPermitido(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("29/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataInicioInvalida(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("29/05/2018 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataTerminoInvalida(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("29/05/2018 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComDataInicioMaiorQueDataTermino(Funcionario funcionario, Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("31/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("30/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComFuncionarioNulo(Sala sala)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("31/03/2020 20:00:00"),
                Funcionario = null,
                Sala = sala
            };
        }

        public static Evento RetorneEventoInvalidoComSalaNula(Funcionario funcionario)
        {
            return new Evento
            {
                DataInicio = DateTime.Parse("30/03/2020 20:00:00"),
                DataTermino = DateTime.Parse("31/03/2020 20:00:00"),
                Funcionario = funcionario,
                Sala = null
            };
        }
    }
}
