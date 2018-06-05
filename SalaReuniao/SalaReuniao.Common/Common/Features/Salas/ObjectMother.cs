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
        public static Sala RetorneNovaSalaOk()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
            };
        }

        public static Sala RetorneSalaExistenteOk()
        {
            return new Sala
            {
                Id = 1,
                Nome = Nome.TREINAMENTO,
                NumeroLugares = 32,
            };
        }

        public static Sala RetorneSalaInvalidaComNumeroLugaresNaoInformado()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
            };
        }

        public static Sala RetorneSalaInvalidaComNumeroLugaresInvalido()
        {
            return new Sala
            {
                Nome = Nome.TREINAMENTO,
                NumeroLugares = -1,
            };
        }
    }
}
