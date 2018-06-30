using Prova3.Application.Features.Results;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Application.Features.Evaluations
{
    public class EvaluationService : IEvaluationService
    {
        private IEvaluationRepository _evaluationRepository;
        private IResultRepository _resultRepository;

        public EvaluationService(IEvaluationRepository evaluationRepository, IResultRepository resultRepository)
        {
            _evaluationRepository = evaluationRepository;
            _resultRepository = resultRepository;
        }

        public Evaluation Add(Evaluation entity)
        {
            entity.Validate();
            entity = _evaluationRepository.Save(entity);
            return entity;
        }

        public void Delete(Evaluation entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            entity.Results = _resultRepository.GetByEvaluation(entity.Id).ToList();
            foreach (var result in entity.Results)
            {
                _resultRepository.Delete(result);
            }
            _evaluationRepository.Delete(entity);
        }

        public Evaluation Get(int id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            Evaluation evaluation = _evaluationRepository.Get(id);
            if (evaluation != null)
            {
                evaluation.Results = _resultRepository.GetByEvaluation(evaluation.Id).ToList();
            }
            return evaluation;
        }

        public IList<Evaluation> GetAll()
        {
            IList<Evaluation> evaluations = _evaluationRepository.GetAll();
            foreach (var evaluation in evaluations)
            {
                evaluation.Results = _resultRepository.GetByEvaluation(evaluation.Id).ToList();
            }
            return evaluations;
        }

        public Evaluation Update(Evaluation entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            entity.Validate();
            _evaluationRepository.Update(entity);
            return entity;
        }
    }
}
