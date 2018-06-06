using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Eventos
{
    public interface IEventoRepositorio
    {
        Evento Salvar(Evento evento);
        Evento Atualizar(Evento evento);
        Evento Carregar(long id);
        IEnumerable<Evento> CarregarTodos();
        void Deletar(Evento evento);
        IEnumerable<Evento> CarregarPorHorario(Evento evento);
        IEnumerable<Evento> CarregarPorFuncionario(long idFuncionario);
        IEnumerable<Evento> CarregarPorSala(long idSala);
    }
}
