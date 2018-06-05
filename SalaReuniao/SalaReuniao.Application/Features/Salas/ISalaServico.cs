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
        Sala Adicionar(Sala sala);
        Sala Atualizar(Sala sala);
        Sala Carregar(long id);
        IEnumerable<Sala> CarregarTodos();
        void Deletar(Sala sala);
    }
}
