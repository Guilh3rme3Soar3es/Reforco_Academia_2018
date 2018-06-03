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
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Ramal { get; set; }


        public override void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                throw new FuncionarioNomeNuloOuVazioException();
            if (Nome.Length > 100)
                throw new FuncionarioEstouroDeLarguraDoNomeException();
            if (String.IsNullOrEmpty(Cargo))
                throw new FuncionarioCargoNuloOuVazioException();
            if (Cargo.Length > 50)
                throw new FuncionarioEstouroDeLarguraDeCargoException();
            if (String.IsNullOrEmpty(Ramal))
                throw new FuncionarioRamalNuloOuVazioException();
            if (Ramal.Length != 4)
                throw new FuncionarioRamalInvalidoException();
        }
    }
}
