using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Exceptions;

namespace Prova3.Application.Features.Results
{
    public class ResultService : IResultService
    {
        private IResultRepository _resultRepository;

        public ResultService(IResultRepository studentRepository)
        {
            _resultRepository = studentRepository;
        }
        public Result Add(Result entity)
        {
            entity.Validate();
            if (_resultRepository.GetByEvaluationAndStudent(entity).Count > 0)
                throw new ResultWithSameStudentAndEvaluationAlreadyExistsException();
            return _resultRepository.Save(entity);
        }

        public void Delete(Result entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            _resultRepository.Delete(entity);
        }

        public Result Get(int id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return _resultRepository.Get(id);
        }

        public IList<Result> GetAll()
        {
            return _resultRepository.GetAll();
        }

        public Result Update(Result entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            entity.Validate();
            _resultRepository.Update(entity);
            return entity;
        }
    }
}
