using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Evaluations
{
    public interface IEvaluationRepository
    {
        Evaluation Save(Evaluation entity);
        Evaluation Update(Evaluation entity);
        Evaluation Get(long id);
        IList<Evaluation> GetAll();
        void Delete(Evaluation entity);
    }
}
