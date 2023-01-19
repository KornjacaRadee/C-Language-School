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
using SR50_2021_POP2022.Models;
using static System.Net.WebRequestMethods;

namespace SR50_2021_POP2022.Repositories
{
    class ProfessorRepository : IProfessorRepository, IFilePersistence
    {
        private static List<Professor> professors = new List<Professor>();

        public void Add(Professor professor)
        {
            professors.Add(professor);
            string lessons = "";
            if (professor.lessons != null)
            {


                foreach (var les in professor.lessons)
                {
                    lessons += les + ",";

                }
            }
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Profesori (Email, School, Languages,Lessons)
                    values (@Email, @School, @Languages, @Lessons)";

                command.Parameters.Add(new SqlParameter("Email", professor.User.Email));
                command.Parameters.Add(new SqlParameter("Lessons", lessons));
                command.Parameters.Add(new SqlParameter("School", professor.school.Id));
                command.Parameters.Add(new SqlParameter("Languages", "das,DSADSA"));


                command.ExecuteScalar();
            }
        }

        public void Add(List<Professor> newProfessors)
        {
            professors.AddRange(newProfessors);
            Save();
        }

        public void Set(List<Professor> newProfessors)
        {
            professors = newProfessors;
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

        public List<Professor> GetAll()
        {
            List<Professor> students = new List<Professor>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Profesori";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Profesori");

                foreach (DataRow row in ds.Tables["Profesori"].Rows)
                {
                    User userko = Data.Instance.UserService.GetUserById(row["Email"] as string);
                    string lessn = row["Lessons"] as string;
                    string[] jesenji = lessn.Split(',');
                    School skolica = Data.Instance.SchoolService.GetSchoolById(row["School"] as string)[0];
                    List<String> language = new List<String>();
                    string lang = row["Languages"] as string;
                    string[] langic = lessn.Split(',');
                    foreach (string lan in langic) {
                    language.Add(lan);
                    }

                    List<Lesson> lessonsi = new List<Lesson>();
                    if (jesenji.Count() == 0)
                    {
                        foreach (string id in jesenji)
                        {
                            lessonsi.Add(Data.Instance.LessonService.GetActiveLessonsById(id)[0]);


                        }
                    }

                    




                    Professor student = new Professor()
                    {
                        User = userko,
                        school = skolica,
                        lessons = lessonsi,
                        languages = language

                    };

                    students.Add(student);
                }
            }

            return students;
        }

        public Professor GetById(string email)
        {
            return GetAll().Find(u => u.User.Email == email);
        }

        public void Update(string email, Professor updatedProfessor)
        {
            Professor student = GetById(email);

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

                command.Parameters.Add(new SqlParameter("Email", updatedProfessor.User.Email));
                command.Parameters.Add(new SqlParameter("Password", updatedProfessor.User.Password));
                command.Parameters.Add(new SqlParameter("FirstName", updatedProfessor.User.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", updatedProfessor.User.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", updatedProfessor.User.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", updatedProfessor.User.Gender));
                command.Parameters.Add(new SqlParameter("UserType", updatedProfessor.User.UserType));
                command.Parameters.Add(new SqlParameter("Address", "updatedStudent.User.Address.Id"));
                command.Parameters.Add(new SqlParameter("IsActive", updatedProfessor.User.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, professors);
            }
        }
    }
}
