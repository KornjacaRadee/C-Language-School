using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    class ProfessorRepository : IProfessorRepository, IFilePersistence
    {
        private static List<Professor> professors = new List<Professor>();

        public void Add(Professor professor)
        {
            professors.Add(professor);
            Save();
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
            Professor professor = GetById(email);

            if (professor != null)
            {
                professor.User.IsActive = false;
            }

            Save();
        }

        public List<Professor> GetAll()
        {
            return professors;
        }

        public Professor GetById(string email)
        {
            return professors.Find(u => u.User.Email == email);
        }

        public void Update(string email, Professor updatedProfessor)
        {
            Save();
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
