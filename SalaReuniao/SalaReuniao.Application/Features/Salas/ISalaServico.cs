using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Features.Salas
{
    public interface ISalaServico
    {
        Sala Add(Sala sala);
        Sala Update(Sala sala);
        Sala Get(long id);
        IEnumerable<Sala> GetAll();
        void Delete(Sala sala);
    }
}
