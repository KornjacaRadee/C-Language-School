using SR50_2021_POP2022.CustomExceptions;
using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;

namespace SR50_2021_POP2022.Repositories
{
    class LessonRepository : ILessonRepository, IFilePersistence
    {
        private static List<Lesson> lessons;

        public LessonRepository()
        {
            lessons = new List<Lesson>();
        }

        public void Add(Lesson lesson)
        {
            lessons.Add(lesson);
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Lesson (Id, Professor, Date, Duration, Student, IsReserved, IsActive)
                    output inserted.Id
                    values (@Id, @Professor, @Date, @Duration, @Student, @IsReserved, @IsActive)";

                command.Parameters.Add(new SqlParameter("Id", lesson.Id));
                command.Parameters.Add(new SqlParameter("Professor", lesson.Professor.User.Email));
                command.Parameters.Add(new SqlParameter("Date", lesson.Date.ToString()));
                command.Parameters.Add(new SqlParameter("Duration", lesson.Duration));
                command.Parameters.Add(new SqlParameter("Student", lesson.Student.User.Email));
                command.Parameters.Add(new SqlParameter("IsReserved", lesson.IsReserved));
                command.Parameters.Add(new SqlParameter("IsActive", lesson.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Add(List<Lesson> newLessons)
        {
            lessons.AddRange(newLessons);
            Save();
        }

        public void Set(List<Lesson> newLessons)
        {
            lessons = newLessons;
        }

        public void Delete(string id)
        {
            Lesson lesson = GetById(id);

            if (lesson != null)
            {
                lesson.IsActive = false;
            }
            else
            {
                throw new UserNotFoundException();
            }

            Save();
        }

        public List<Lesson> GetAll()
        {
            List<Lesson> lessons = new List<Lesson>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Lesson";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Lesson");

                foreach (DataRow row in ds.Tables["Lesson"].Rows)
                {
                    Professor professor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail((row["Professor"] as String));
                    Student student = Data.Instance.StudentService.GetActiveStudentsByEmail((row["Student"] as String));
                    String my_date_string = row["Date"] as string;

                    var user = new Lesson
                    {
                        Id = row["Id"] as string,
                        Professor = professor,
                        Student = student,
                        Date = Convert.ToDateTime(my_date_string),
                        Duration = row["Duration"] as string,
                        IsReserved = (bool)row["IsReserved"],
                        IsActive = (bool)row["IsActive"]
                    };

                    lessons.Add(user);
                }
            }
            return lessons;
        }

        public Lesson GetById(string id)
        {
            return lessons.Find(u => u.Id == id);
        }




        public void Update(string id, Lesson updatedLesson)
        {
            Lesson lesson = GetById(id);

            if (lesson != null)
            {
                lesson.Id = updatedLesson.Id;
                lesson.Professor = updatedLesson.Professor;
                lesson.Date= updatedLesson.Date;
                lesson.Duration = updatedLesson.Duration;
                lesson.Student = updatedLesson.Student;
                lesson.IsReserved = updatedLesson.IsReserved;
                lesson.IsActive = updatedLesson.IsActive;
            }
            Save();
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.lessonsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, lessons);
            }
        }
    }
}

