using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public interface ISalaRepositorio
    {
        Sala Save(Sala sala);
        Sala Update(Sala sala);
        Sala Get(long id);
        IEnumerable<Sala> GetAll();
        void Delete(Sala sala);
    }
}
