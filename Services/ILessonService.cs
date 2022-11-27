using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    interface ILessonService
    {
        List<Lesson> GetAll();
        List<Lesson> GetActiveLessons();
        void Add(Lesson lesson);
        void Set(List<Lesson> lessons);
        void Update(string id, Lesson lesson);
        void Delete(string id);
    }
}
