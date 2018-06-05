using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao.Domain.Features.Salas
{
    public interface ISalaRepositorio
    {
        Sala Salvar(Sala sala);
        Sala Atualizar(Sala sala);
        Sala Carregar(long id);
        IEnumerable<Sala> CarregarTodos();
        void Deletar(Sala sala);
    }
}
