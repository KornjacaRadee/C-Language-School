using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(string email);
        void Add(Student student);
        void Add(List<Student> students);
        void Set(List<Student> students);
        void Update(string email, Student student);
        void Delete(string email);
    }
}
