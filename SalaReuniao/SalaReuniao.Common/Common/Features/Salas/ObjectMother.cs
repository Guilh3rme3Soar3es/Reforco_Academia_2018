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
        public static Sala GetNovaSalaOk()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
            };
        }

        public static Sala GetSalaExistenteOk()
        {
            return new Sala
            {
                Id = 1,
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
            };
        }

        public static Sala GetSalaInvalidaComNumeroLugaresNaoInformado()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
            };
        }

        public static Sala GetSalaInvalidaComNumeroLugaresInvalido()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = -1,
            };
        }
    }
}
