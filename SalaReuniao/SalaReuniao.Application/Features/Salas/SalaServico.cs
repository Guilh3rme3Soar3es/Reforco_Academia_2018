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

        public Sala Adicionar(Sala sala)
        {
            sala.Validar();
            return _salaRepositorio.Salvar(sala);
        }

        public void Deletar(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            _salaRepositorio.Deletar(sala);
        }

        public Sala Carregar(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _salaRepositorio.Carregar(id);
        }

        public IEnumerable<Sala> CarregarTodos()
        {
            return _salaRepositorio.CarregarTodos();
        }

        public Sala Atualizar(Sala sala)
        {
            if (sala.Id <= 0)
                throw new IdentifierUndefinedException();
            sala.Validar();
            return _salaRepositorio.Atualizar(sala);
        }
    }
}
