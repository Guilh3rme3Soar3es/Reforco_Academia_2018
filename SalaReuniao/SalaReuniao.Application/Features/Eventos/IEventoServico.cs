using SalaReuniao.Domain.Features.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Features.Eventos
{
    public interface IEventoServico
    {
        Evento Add(Evento evento);
        Evento Update(Evento evento);
        Evento Get(long id);
        IEnumerable<Evento> GetAll();
        void Delete(Evento evento);
    }
}
