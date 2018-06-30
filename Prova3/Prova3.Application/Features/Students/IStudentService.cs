using Prova3.Domain.Features.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova3.Application.Features.Students
{
    public interface IStudentService
    {
        Student Add(Student entity);
        Student Update(Student entity);
        Student Get(int id);
        IList<Student> GetAll();
        void Delete(Student entity);
    }
}
