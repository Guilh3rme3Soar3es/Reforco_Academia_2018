using SalaReuniao.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Funcionarios
{
    public class Funcionario : Entidade
    {
        public string nome { get; set; }
        public string cargo { get; set; }
        public string ramal { get; set; }


        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
