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
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        IList<Funcionario> Funcionarios { get; set; }

        public override void Validar()
        {
            if(DataInicio.Hour < 8 || DataInicio.Hour > 18)
            {
                throw new Exception();
            }
            if (DataTermino.Hour < 8 || DataTermino.Hour > 18)
            {
                throw new Exception();
            }
        }
    }
}
