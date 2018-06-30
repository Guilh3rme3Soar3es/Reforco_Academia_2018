using NDD.Nfe.Infra;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Infra.Data.Features.Evaluations
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private const string _insert = @"INSERT INTO evaluation (
                                            date, 
                                            assunto
                                        ) VALUES (
                                            @date, 
                                            @assunto 
                                        )";

        private const string _update = @"UPDATE evaluation SET 
                                            date = @date, 
                                            assunto = @assunto  
                                        WHERE id_evaluation = @id_evaluation";

        private readonly string _getById = @"SELECT * FROM evaluation WHERE id_evaluation = @id_evaluation";

        private readonly string _getAll = @"SELECT * FROM evaluation";

        private readonly string _delete = @"DELETE FROM evaluation WHERE id_evaluation = @id_evaluation";

        public void Delete(Evaluation entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            Db.Delete(_delete, new object[] { "@id_evaluation", entity.Id });
        }

        public Evaluation Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();

            return Db.Get(_getById, Make, new object[] { "@id_evaluation", id });
        }

        public IList<Evaluation> GetAll()
        {
            return Db.GetAll(_getAll, Make);
        }

        public Evaluation Save(Evaluation entity)
        {
            entity.Validate();

            entity.Id = Convert.ToInt32(Db.Insert(_insert, Take(entity)));

            return entity;
        }

        public Evaluation Update(Evaluation entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            entity.Validate();

            Db.Update(_update, Take(entity));

            return entity;
        }

        private object[] Take(Evaluation evaluation)
        {
            return new object[]
            {
                "@id_evaluation", evaluation.Id,
                "@date", evaluation.Date,
                "@assunto", evaluation.Subject
            };
        }



        private static Func<IDataReader, Evaluation> Make = reader =>
           new Evaluation
           {
               Id = Convert.ToInt32(reader["id_evaluation"]),
               Date = Convert.ToDateTime(reader["date"]),
               Subject = Convert.ToString(reader["assunto"])
           };
    }
}
