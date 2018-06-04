using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Features.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Application.Features.Salas
{
    public class SalaServico : ISalaServico
    {
        private ISalaRepositorio _salaRepositorio;

        public SalaServico(ISalaRepositorio salaRepositorio)
        {
            _salaRepositorio = salaRepositorio;
        }

        public Sala Add(Sala sala)
        {
            sala.Validar();
            return _salaRepositorio.Save(sala);
        }

        public void Delete(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            //if (_loanService.GetByBook(book.Id).Count() > 0)
            //    throw new BookWithRelatedLoanException();
            _salaRepositorio.Delete(sala);
        }

        public Sala Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _salaRepositorio.Get(id);
        }

        public IEnumerable<Sala> GetAll()
        {
            return _salaRepositorio.GetAll();
        }

        public Sala Update(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _salaRepositorio.Update(sala);
        }
    }
}
