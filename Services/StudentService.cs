using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;

namespace SR50_2021_POP2022.Services
{
    class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        private IUserRepository userRepository;

        public StudentService()
        {
            studentRepository = new StudentRepository();
            userRepository = new UserRepository();
        }

        public void Delete(string email)
        {
            userRepository.Delete(email);
            studentRepository.Delete(email);
        }

        public List<Student> GetActiveStudents()
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive).ToList();
        }

        public List<Student> GetActiveStudentsByEmail(string email)
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive && p.User.Email.Contains(email)).ToList();
        }

        public List<Student> GetActiveStudentsByJMBG(string email)
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive && p.User.JMBG.Contains(email)).ToList();
        }

        public List<Student> GetActiveStudentsOrderedByEmail()
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive).OrderBy(p => p.User.Email).ToList();
        }

        public void Add(User user)
        {
            userRepository.Add(user);

            var student = new Student
            {
                User = user,
                UserId = user.Email

            };

            studentRepository.Add(student);
        }

        public List<Student> GetAll()
        {
            return studentRepository.GetAll();
        }

        public Student GetById(string email)
        {
            return studentRepository.GetById(email);
        }

        public List<User> ListAllStudents()
        {
            throw new NotImplementedException();
        }

        public void Set(List<Student> students)
        {
            studentRepository.Set(students);
        }

        public void Update(string email, Student student)
        {
            userRepository.Update(email, student.User);
            studentRepository.Update(email, student);
        }
    }
}
