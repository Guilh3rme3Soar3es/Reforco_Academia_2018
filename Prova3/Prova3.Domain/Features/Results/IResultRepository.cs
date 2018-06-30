using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Results
{
    public interface IResultRepository
    {
        Result Save(Result entity);
        Result Update(Result entity);
        Result Get(long id);
        IList<Result> GetAll();
        void Delete(Result entity);

        IList<Result> GetByEvaluation(long idEvaluation);
        IList<Result> GetByStudent(long idStudent);
        IList<Result> GetByEvaluationAndStudent(Result result);

    }
}
