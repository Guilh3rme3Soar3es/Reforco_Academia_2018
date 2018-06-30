using Prova3.Domain.Features.Evaluations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Application.Features.Evaluations
{
    public interface IEvaluationService
    {
        Evaluation Add(Evaluation entity);
        Evaluation Update(Evaluation entity);
        Evaluation Get(int id);
        IList<Evaluation> GetAll();
        void Delete(Evaluation entity);
    }
}
