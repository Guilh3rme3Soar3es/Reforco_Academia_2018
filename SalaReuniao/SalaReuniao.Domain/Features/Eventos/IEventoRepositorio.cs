using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public interface IEventoRepositorio
    {
        Evento Save(Evento evento);
        Evento Update(Evento evento);
        Evento Get(long id);
        IEnumerable<Evento> GetAll();
        void Delete(Evento evento);
    }
}
