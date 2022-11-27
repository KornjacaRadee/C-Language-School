using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    class AdministratorService : IAdministratorService
    {
        private IAdministratorRepository administratorRepository;
        private IUserRepository userRepository;
        public AdministratorService()
        {
            administratorRepository = new AdministratorRepository();
            userRepository = new UserRepository();
        }

        public Administrator GetById(string email)
        {
            return administratorRepository.GetById(email);
        }

        public List<Administrator> GetAll()
        {
            return administratorRepository.GetAll();
        }

        public List<Administrator> GetActiveAdministrators()
        {
            return administratorRepository.GetAll().Where(p => p.User.IsActive).ToList();
        }

        public List<Administrator> GetActiveAdministratorsByEmail(string email)
        {
            return administratorRepository.GetAll().Where(p => p.User.IsActive && p.User.Email.Contains(email)).ToList();
        }
        public List<Administrator> GetActiveAdministratorsOrderedByEmail()
        {
            return administratorRepository.GetAll().Where(p => p.User.IsActive).OrderBy(p => p.User.Email).ToList();
        }

        public void Add(User user)
        {
            userRepository.Add(user);

            var administrator = new Administrator
            {
                User = user,
                UserId = user.Email

            };

            administratorRepository.Add(administrator);
        }

        public void Set(List<Administrator> administrators)
        {
            administratorRepository.Set(administrators);
        }

        public void Update(string email, Administrator administrator)
        {
            userRepository.Update(email, administrator.User);
            administratorRepository.Update(email, administrator);
        }

        public void Delete(string email)
        {
            userRepository.Delete(email);
            administratorRepository.Delete(email);
        }


    }
}

