using NDD.Nfe.Infra;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Infra.Data.Features.Students
{
    public class StudentRepository : IStudentRepository
    {
        private const string _insert = @"INSERT INTO student (
                                            age, 
                                            name, 
                                            average
                                        ) VALUES (
                                            @age, 
                                            @name, 
                                            @average
                                        )";

        private const string _update = @"UPDATE student SET 
                                            age = @age, 
                                            name = @name, 
                                            average = @average  
                                        WHERE id_student = @id_student";

        private readonly string _getById = @"SELECT * FROM student WHERE id_student = @id_student";

        private readonly string _getAll = @"SELECT * FROM student";

        private readonly string _delete = @"DELETE FROM student WHERE id_student = @id_student";

        public void Delete(Student entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            Db.Delete(_delete, new object[] { "@id_student", entity.Id });
        }

        public Student Get(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();

            return Db.Get(_getById, Make, new object[] { "@id_student", id });
        }

        public IList<Student> GetAll()
        {
            return Db.GetAll(_getAll, Make);
        }

        public Student Save(Student entity)
        {
            entity.Validate();

            entity.Id = Convert.ToInt32(Db.Insert(_insert, Take(entity)));

            return entity;
        }

        public Student Update(Student entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();

            entity.Validate();

            Db.Update(_update, Take(entity));

            return entity;
        }

        private object[] Take(Student student)
        {
            return new object[]
            {
                "@id_student", student.Id,
                "@age", student.Age,
                "@name", student.Name,
                "@average", student.Average
            };
        }

        private static Func<IDataReader, Student> Make = reader =>
           new Student
           {
               Id = Convert.ToInt32(reader["id_student"]),
               Age = Convert.ToInt32(reader["age"]),
               Name = reader["name"].ToString(),
               Average = Convert.ToDouble(reader["average"])
           };
    }
}
