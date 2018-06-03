using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Eventos;

namespace SalaReuniao.Application.Features.Eventos
{
    public class EventoServico : IEventoServico
    {
        private IEventoRepositorio _eventoRepositorio;

        public EventoServico(IEventoRepositorio eventoRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
        }

        public Evento Add(Evento sala)
        {
            sala.Validar();
            return _eventoRepositorio.Save(sala);
        }

        public void Delete(Evento sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            //if (_loanService.GetByBook(book.Id).Count() > 0)
            //    throw new BookWithRelatedLoanException();
            _eventoRepositorio.Delete(sala);
        }

        public Evento Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _eventoRepositorio.Get(id);
        }

        public IEnumerable<Evento> GetAll()
        {
            return _eventoRepositorio.GetAll();
        }

        public Evento Update(Evento sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _eventoRepositorio.Update(sala);
        }
    }
}
