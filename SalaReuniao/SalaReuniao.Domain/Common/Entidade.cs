using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Common
{
    public abstract class Entidade
    {
        public virtual long Id { get; set; }

        public abstract void Validar();
    }
}
