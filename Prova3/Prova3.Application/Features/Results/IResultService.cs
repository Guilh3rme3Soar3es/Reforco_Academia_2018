using Prova3.Domain.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Application.Features.Results
{
    public interface IResultService
    {
        Result Add(Result entity);
        Result Update(Result entity);
        Result Get(int id);
        IList<Result> GetAll();
        void Delete(Result entity);
    }
}
