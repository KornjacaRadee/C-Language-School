using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.CustomExceptions;
using SR50_2021_POP2022.Repositories;

namespace SR50_2021_POP2022.Services
{
    class UserService : IUserService
    {
        private IUserRepository repostory;

        public UserService()
        {
            repostory = new UserRepository();
        }

        public List<User> GetActiveUsers()
        {
            return repostory.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<User> GetUserByJMBG(string JMBG)
        {
            return repostory.GetAll().Where(p => p.IsActive && p.JMBG.Contains(JMBG)).ToList();
        }



        public List<User> GetAll()
        {
            return repostory.GetAll();
        }


        public void Add(User user)
        {
            repostory.Add(user);
        }

        public void Set(List<User> users)
        {
            repostory.Set(users);
        }

        public void Update(string email, User user)
        {
            repostory.Update(email, user);
        }

        public void Delete(string email)
        {
            repostory.Delete(email);
        }
    }
}
