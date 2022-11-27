using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    class LessonService : ILessonService
    {
        private ILessonRepository repostory;

        public LessonService()
        {
            repostory = new LessonRepository();
        }

        public List<Lesson> GetActiveLessons()
        {
            return repostory.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<Lesson> GetActiveLessonsById(string email)
        {
            return repostory.GetAll().Where(p => p.IsActive && p.Id.Contains(email)).ToList();
        }





        public List<Lesson> GetAll()
        {
            return repostory.GetAll();
        }


        public void Add(Lesson lesson)
        {
            repostory.Add(lesson);
        }

        public void Set(List<Lesson> lessons)
        {
            repostory.Set(lessons);
        }

        public void Update(string id, Lesson lesson)
        {
            repostory.Update(id, lesson);
        }

        public void Delete(string id)
        {
            repostory.Delete(id);
        }
    }
}

