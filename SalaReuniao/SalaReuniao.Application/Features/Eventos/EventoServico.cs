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

        public Evento Adicionar(Evento evento)
        {
            evento.Validar();
            if (_eventoRepositorio.CarregarPorHorario(evento).Count() > 0)
                throw new EventoEmHorarioOcupadoException();
            return _eventoRepositorio.Salvar(evento);
        }

        public void Deletar(Evento evento)
        {
            if (evento.Id <= 0)
                throw new IdentifierUndefinedException();
            _eventoRepositorio.Deletar(evento);
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

        public Evento Atualizar(Evento evento)
        {
            if (evento.Id <= 0)
                throw new IdentifierUndefinedException();
            evento.Validar();
            if (_eventoRepositorio.CarregarPorHorario(evento).Count() > 0)
                throw new EventoEmHorarioOcupadoException();
            return _eventoRepositorio.Atualizar(evento);
        }

        public IEnumerable<Evento> CarregarPorFuncionarios(long idFuncionario)
        {
            if (idFuncionario <= 0)
                throw new IdentifierUndefinedException();
            return _eventoRepositorio.CarregarPorFuncionario(idFuncionario);
        }

        public IEnumerable<Evento> CarregarPorSala(long idSala)
        {
            if (idSala <= 0)
                throw new IdentifierUndefinedException();
            return _eventoRepositorio.CarregarPorSala(idSala);
        }
    }
}
