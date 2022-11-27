using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    class SchoolService : ISchoolService
    {
        private ISchoolRepository repostory;

        public SchoolService()
        {
            repostory = new SchoolRepository();
        }

        public List<School> GetActiveSchools()
        {
            return repostory.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<School> GetSchoolById(string Id)
        {
            return repostory.GetAll().Where(p => p.IsActive && p.Id.Contains(Id)).ToList();
        }



        public List<School> GetAll()
        {
            return repostory.GetAll();
        }


        public void Add(School school)
        {
            repostory.Add(school);
        }

        public void Set(List<School> schools)
        {
            repostory.Set(schools);
        }

        public void Update(string id, School school)
        {
            repostory.Update(id, school);
        }

        public void Delete(string id)
        {
            repostory.Delete(id);
        }
    }
}
