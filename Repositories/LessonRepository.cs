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
            Save();
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

