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

        public Evento Adicionar(Evento sala)
        {
            sala.Validar();
            return _eventoRepositorio.Salvar(sala);
        }

        public void Deletar(Evento sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            _eventoRepositorio.Deletar(sala);
        }

        public Evento Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _eventoRepositorio.Carregar(id);
        }

        public IEnumerable<Evento> CarregarTodos()
        {
            return _eventoRepositorio.CarregarTodos();
        }

        public Evento Atualizar(Evento sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _eventoRepositorio.Atualizar(sala);
        }
    }
}
