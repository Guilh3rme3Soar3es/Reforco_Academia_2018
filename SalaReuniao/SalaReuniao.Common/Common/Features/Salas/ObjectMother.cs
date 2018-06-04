using SalaReuniao.Domain.Common;
using SalaReuniao.Domain.Features.Eventos;
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
        public static Sala GetNovaSalaOk(Evento evento)
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
                Eventos = new List<Evento> { evento }
            };
        }

        public static Sala GetSalaExistenteOk(Evento evento)
        {
            return new Sala
            {
                Id = 1,
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
                Eventos = new List<Evento> { evento }
            };
        }

        public static Sala GetSalaInvalidaComNumeroLugaresNaoInformado(Evento evento)
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                Eventos = new List<Evento> { evento }
            };
        }

        public static Sala GetSalaInvalidaComNumeroLugaresInvalido(Evento evento)
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = -1,
                Eventos = new List<Evento> { evento }
            };
        }

        public static Sala GetSalaInvalidaComEventosNoMesmoHorario(Evento primeiroEvento,Evento segundoEvento)
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
                Eventos = new List<Evento> { primeiroEvento, segundoEvento }
            };
        }
    }
}
