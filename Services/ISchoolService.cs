using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    interface ISchoolService
    {
        List<School> GetAll();
        List<School> GetActiveSchools();
        List<School> GetSchoolById(string id);
        void Add(School school);
        void Set(List<School> schools);
        void Update(string id, School school);
        void Delete(string id);
    }
}
