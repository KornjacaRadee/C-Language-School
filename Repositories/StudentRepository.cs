using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    class StudentRepository : IStudentRepository, IFilePersistence
    {
        private static List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            students.Add(student);
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Student (Email, Lessons)
                    values (@Email, @Lessons)";

                command.Parameters.Add(new SqlParameter("Email", student.User.Email));
                command.Parameters.Add(new SqlParameter("Lessons", "dasdsadsadsa"));

                command.ExecuteScalar();
            }
            
        }
        public void Add(List<Student> newStudents)
        {
            students.AddRange(newStudents);
            Save();
        }

        public void Delete(string email)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Users set IsActive=0 where Email=@email";

                command.Parameters.Add(new SqlParameter("email", email));
                command.ExecuteNonQuery();
            }



        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Student";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Student");

                foreach (DataRow row in ds.Tables["Student"].Rows)
                {
                    User userko = Data.Instance.UserService.GetUserById(row["Email"] as string);
                    
                    
                    
                    Student student = new Student()
                    {
                        User = userko,
                        
                    };

                    students.Add(student);
                }
            }
                
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

        public void Update(string email, Student updatedStudent)
        {
            Student student = GetById(email);

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Users set 
                        Email = @Email,
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Password = @Password,
                        JMBG = @Jmbg,
                        Gender = @Gender,
                        UserType = @UserType,
                        Address = @Address,
                        IsActive = @IsActive
                        where email=@Email";

                command.Parameters.Add(new SqlParameter("Email", updatedStudent.User.Email));
                command.Parameters.Add(new SqlParameter("Password", updatedStudent.User.Password));
                command.Parameters.Add(new SqlParameter("FirstName", updatedStudent.User.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", updatedStudent.User.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", updatedStudent.User.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", updatedStudent.User.Gender));
                command.Parameters.Add(new SqlParameter("UserType", updatedStudent.User.UserType));
                command.Parameters.Add(new SqlParameter("Address", "updatedStudent.User.Address.Id"));
                command.Parameters.Add(new SqlParameter("IsActive", updatedStudent.User.IsActive));

                command.ExecuteScalar();
            }
        }
    }
}
