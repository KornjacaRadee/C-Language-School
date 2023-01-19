using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR50_2021_POP2022.Models;

namespace SR50_2021_POP2022.Services
{
    interface IUserService
    {
        List<User> GetAll();
        List<User> GetActiveUsers();

        User GetUserById(string JMBG);
        User GetUserByJMBG(string JMBG);
        void Add(User user);
        void Set(List<User> users);
        void Update(string email, User user);
        void Delete(string email);
    }
}
