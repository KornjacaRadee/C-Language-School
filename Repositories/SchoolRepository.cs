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
    class SchoolRepository : ISchoolRepository, IFilePersistence
    {

        private static List<School> schools = new List<School>();

        public void Add(School school)
        {
            schools.Add(school);
            Save();
        }

        public void Add(List<School> newSchool)
        {
            schools.AddRange(newSchool);
            Save();
        }

        public void Set(List<School> newSchools)
        {
            schools = newSchools;
        }

        public void Delete(string Id)
        {
            School school = GetById(Id);

            if (school != null)
            {
                school.IsActive = false;
            }

            Save();
        }

        public List<School> GetAll()
        {
            return schools;
        }

        public School GetById(string id)
        {
            return schools.Find(u => u.Id == id);
        }

        public void Update(string Id, School updatedSchool)
        {
            Save();
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, schools);
            }
        }
    }
}
