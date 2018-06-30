using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Domain.Features.Students
{
    public interface IStudentRepository
    {
        Student Save(Student entity);
        Student Update(Student entity);
        Student Get(long id);
        IList<Student> GetAll();
        void Delete(Student entity);
    }
}
