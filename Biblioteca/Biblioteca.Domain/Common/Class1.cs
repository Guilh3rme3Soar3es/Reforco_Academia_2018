using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Common
{
    public abstract class Entity
    {
        public virtual long Id { get; set; }

        public abstract void Validate();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override bool Equals(object obj)
        {
            Entity entidade = obj as Entity;
            if (entidade == null)
                return false;
            else
                return Id.Equals(entidade.Id);
        }
    }
}
