using NDD.Nfe.Infra;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Evaluations;
using Prova3.Domain.Features.Results;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Infra.Data.Features.Results
{
    public class ResultRepository : IResultRepository
    {
        private const string _insert = @"INSERT INTO result (
                                            note, 
                                            evaluation_id, 
                                            student_id
                                        ) VALUES (
                                            @note, 
                                            @evaluation_id, 
                                            @student_id
                                        )";

        private const string _update = @"UPDATE result SET 
                                            note = @note, 
                                            evaluation_id = @student_id, 
                                            student_id = @student_id  
                                        WHERE id_result = @id_result";

        private readonly string _getById = @"SELECT * FROM result AS r INNER JOIN student AS s ON (s.id_student = r.student_id) INNER JOIN evaluation AS e ON (e.id_evaluation = r.evaluation_id) WHERE id_result = @id_result";

        private readonly string _getByEvaluation = @"SELECT * FROM result AS r INNER JOIN student AS s ON (s.id_student = r.student_id) INNER JOIN evaluation AS e ON (e.id_evaluation = r.evaluation_id) WHERE evaluation_id = @id_evaluation";

        private readonly string _getByStudent = @"SELECT * FROM result AS r INNER JOIN student AS s ON (s.id_student = r.student_id) INNER JOIN evaluation AS e ON (e.id_evaluation = r.evaluation_id) WHERE student_id = @id_student";

        private readonly string _getByEvaluationAndStudent = "SELECT * FROM result AS r INNER JOIN student AS s ON (s.id_student = r.student_id) INNER JOIN evaluation AS e ON (e.id_evaluation = r.evaluation_id) WHERE student_id = @id_student AND evaluation_id = @id_evaluation";

        private readonly string _getAll = @"SELECT * FROM result AS r INNER JOIN student AS s ON (s.id_student = r.student_id) INNER JOIN evaluation AS e ON (e.id_evaluation = r.evaluation_id)";

        private readonly string _delete = @"DELETE FROM result WHERE id_result = @id_result";

        public void Delete(Result entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            Db.Delete(_delete, new object[] { "@id_result", entity.Id });
        }

        public Result Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();

            return Db.Get(_getById, Make, new object[] { "@id_result", id });
        }

        public IList<Result> GetAll()
        {
            return Db.GetAll(_getAll, Make);
        }

        public Result Save(Result entity)
        {
            entity.Validate();

            entity.Id = Convert.ToInt32(Db.Insert(_insert, Take(entity)));

            return entity;
        }

        public Result Update(Result entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            entity.Validate();

            Db.Update(_update, Take(entity));

            return entity;
        }

        public IList<Result> GetByEvaluation(long idEvaluation)
        {
            if (idEvaluation <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_getByEvaluation, Make, new object[] { "@id_evaluation", idEvaluation });

        }

        public IList<Result> GetByStudent(long idStudent)
        {
            if (idStudent <= 0)
                throw new IdentifierUndefinedException();
            return Db.GetAll(_getByStudent, Make, new object[] { "@id_student", idStudent });
        }

        public IList<Result> GetByEvaluationAndStudent(Result result)
        {
            return Db.GetAll(_getByEvaluationAndStudent, Make, new object[] { "@id_student", result.Student.Id, "@id_evaluation", result.Evaluation.Id });
        }

        private object[] Take(Result result)
        {
            return new object[]
            {
                "@id_result", result.Id,
                "@note", result.Note,
                "@evaluation_id", result.Evaluation.Id,
                "@student_id", result.Student.Id
            };
        }

        

        private static Func<IDataReader, Result> Make = reader =>
           new Result
           {
               Id = Convert.ToInt32(reader["id_student"]),
               Note = Convert.ToInt32(reader["note"]),
               Evaluation = new Evaluation()
               {
                   Id = Convert.ToInt32(reader["id_evaluation"]),
                   Date = Convert.ToDateTime(reader["date"]),
                   Subject = Convert.ToString(reader["assunto"])
               },
               Student = new Student()
               {
                   Id = Convert.ToInt32(reader["id_student"]),
                   Age = Convert.ToInt32(reader["age"]),
                   Average = Convert.ToDouble(reader["average"]),
                   Name = Convert.ToString(reader["name"])
               }
           };
    }
}
