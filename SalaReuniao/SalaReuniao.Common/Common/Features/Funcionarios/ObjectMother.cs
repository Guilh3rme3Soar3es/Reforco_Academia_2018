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
        public static Funcionario RetorneNovoFuncionarioOk()
        {
            return new Funcionario
            {
                Cargo = "Gerente",
                Nome = "Joao da Silva",
                Ramal = "1254",
            };
        }

        public static Funcionario RetorneFuncionarioExistenteOk()
        {
            return new Funcionario
            {
                Id = 1,
                Cargo = "Gerente",
                Nome = "Joao da Silva",
                Ramal = "1254",
            };
        }

        public static Funcionario RetorneFuncionarioComNomeNaoInformado()
        {
            return new Funcionario
            {
                Nome = null,
                Cargo = "Programador",
                Ramal = "4567"
            };
        }

        public static Funcionario RetorneFuncionarioComNomeLongo()
        {
            return new Funcionario
            {
                Nome = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa",
                Cargo = "Joao da Silva",
                Ramal = "4567"
            };
        }

        public static Funcionario RetorneFuncionarioComCargoNaoInformado()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = null,
                Ramal = "4567"
            };
        }

        public static Funcionario RetorneFuncionarioComCargoLongo()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa",
                Ramal = "4567"
            };
        }

        public static Funcionario RetorneFuncionarioComRamalNaoInformado()
        {
            return new Funcionario
            {
                Nome = "Joao da Silva",
                Cargo = "Analista",
                Ramal = null
            };
        }

        public static Funcionario RetorneFuncionarioComRamalInvalido()
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
