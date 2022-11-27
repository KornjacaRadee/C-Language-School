using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    interface ILessonRepository
    {
        List<Lesson> GetAll();
        Lesson GetById(string id);

        void Add(Lesson lesson);
        void Add(List<Lesson> lessons);
        void Set(List<Lesson> lessons);
        void Update(string id, Lesson lesson);
        void Delete(string id);
    }
}
