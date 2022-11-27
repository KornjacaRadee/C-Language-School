using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    class StudentRepository : IStudentRepository, IFilePersistence
    {
        private static List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            students.Add(student);
            Save();
        }
        public void Add(List<Student> newStudents)
        {
            students.AddRange(newStudents);
            Save();
        }

        public void Delete(string email)
        {
            Student student = GetById(email);

            if (student != null)
            {
                student.User.IsActive = false;
            }
            Save();
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public Student GetById(string email)
        {
            return students.Find(u => u.User.Email == email);
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.studentsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, students);
            }
        }

        public void Set(List<Student> newStudents)
        {
            students = newStudents;
        }

        public void Update(string email, Student student)
        {
            Save();
        }
    }
}
