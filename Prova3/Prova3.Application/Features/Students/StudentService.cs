using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prova3.Domain.Features.Students;
using Prova3.Domain.Exceptions;
using Prova3.Domain.Features.Results;

namespace Prova3.Application.Features.Students
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;
        private IResultRepository _resultRepository;

        public StudentService(IStudentRepository studentRepository, IResultRepository resultRepository)
        {
            _studentRepository = studentRepository;
            _resultRepository = resultRepository;
        }
        public Student Add(Student entity)
        {
            entity.Validate();
            return _studentRepository.Save(entity);
        }

        public void Delete(Student entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            entity.Results = _resultRepository.GetByStudent(entity.Id);
            foreach (var result in entity.Results)
            {
                _resultRepository.Delete(result);
            }
            _studentRepository.Delete(entity);
        }

        public Student Get(int id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            Student student = _studentRepository.Get(id);
            if (student != null)
            {
                student.Results = _resultRepository.GetByStudent(student.Id);
            }
            return student;
        }

        public IList<Student> GetAll()
        {
            IList<Student> students = _studentRepository.GetAll();
            foreach (var student in students)
            {
                student.Results = _resultRepository.GetByStudent(student.Id);
            }
            return students;
        }

        public Student Update(Student entity)
        {
            if (entity.Id <= 0)
                throw new IdentifierUndefinedException();
            entity.Validate();
            _studentRepository.Update(entity);
            return entity;
        }
    }
}
