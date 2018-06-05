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
        Evento Adicionar(Evento evento);
        Evento Atualizar(Evento evento);
        Evento Carregar(long id);
        IEnumerable<Evento> CarregarTodos();
        void Deletar(Evento evento);
    }
}
