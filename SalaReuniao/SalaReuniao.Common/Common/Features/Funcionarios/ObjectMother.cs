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
        public static Funcionario GetNovoFuncionarioOk()
        {
            return new Funcionario
            {
                Cargo = "Gerente",
                Nome = "Joao da Silva",
                Ramal = "1254",
            };
        }

        public static Funcionario GetFuncionarioExistenteOk()
        {
            return new Funcionario
            {
                Id = 1,
                Cargo = "Gerente",
                Nome = "Joao da Silva",
                Ramal = "1254",
            };
        }

        public static Funcionario GetFuncionarioComNomeNaoInformado()
        {
            return new Funcionario
            {
                Nome = null,
                Cargo = "Programador",
                Ramal = "4567"
            };
        }

        public static Funcionario GetFuncionarioComNomeLongo()
        {
            return new Funcionario
            {
                Nome = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa",
                Cargo = "Joao da Silva",
                Ramal = "4567"
            };
        }

        public static Funcionario GetFuncionarioComCargoNaoInformado()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = null,
                Ramal = "4567"
            };
        }

        public static Funcionario GetFuncionarioComCargoLongo()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa",
                Ramal = "4567"
            };
        }

        public static Funcionario GetFuncionarioComRamalNaoInformado()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = "Analista",
                Ramal = null
            };
        }

        public static Funcionario GetFuncionarioComRamalInvalido()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = "Analista",
                Ramal = "123"
            };
        }
    }
}
