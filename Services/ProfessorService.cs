using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;

namespace SR50_2021_POP2022.Services
{
    class ProfessorService : IProfessorService
    {
        private IProfessorRepository professorRepository;
        private IUserRepository userRepository;

        public ProfessorService()
        {
            professorRepository = new ProfessorRepository();
            userRepository = new UserRepository();
        }

        public Professor GetById(string email)
        {
            return professorRepository.GetById(email);
        }

        public List<Professor> GetAll()
        {
            return professorRepository.GetAll();
        }

        public List<Professor> GetActiveProfessors()
        {
            return professorRepository.GetAll().Where(p => p.User.IsActive).ToList();
        }

        public List<Professor> GetActiveProfessorsByEmail(string email)
        {
            return professorRepository.GetAll().Where(p => p.User.IsActive && p.User.Email.Contains(email)).ToList();
        }
        public List<Professor> GetActiveProfessorsOrderedByEmail()
        {
            return professorRepository.GetAll().Where(p => p.User.IsActive).OrderBy(p => p.User.Email).ToList();
        }

        public void Add(Professor professor)
        {
            userRepository.Add(professor.User);
            professorRepository.Add(professor);
        }

        public void Set(List<Professor> professors)
        {
            professorRepository.Set(professors);
        }

        public void Update(string email, Professor professor)
        {
            userRepository.Update(email, professor.User);
            professorRepository.Update(email, professor);
        }

        public void Delete(string email)
        {
            userRepository.Delete(email);
            professorRepository.Delete(email);
        }

        public List<User> ListAllStudents()
        {
            throw new NotImplementedException();
        }
    }
}
