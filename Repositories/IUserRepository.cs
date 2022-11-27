using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Repositories
{
    interface IUserRepository
    {
        List<User> GetAll();
        User GetById(string email);

        User GetByJMBG(string JMBG);
        void Add(User user);
        void Add(List<User> users);
        void Set(List<User> users);
        void Update(string email, User user);
        void Delete(string email);
    }
}
