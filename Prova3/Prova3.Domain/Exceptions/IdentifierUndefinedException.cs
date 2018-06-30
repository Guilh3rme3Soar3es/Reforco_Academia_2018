using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Exceptions
{
    public class IdentifierUndefinedException : Exception
    {

        public IdentifierUndefinedException() : base("O Id não foi informado.")
        {

        }

    }
}
