using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    interface IAdministratorService
    {
        List<Administrator> GetAll();
        Administrator GetById(string email);
        List<Administrator> GetActiveAdministrators();
        List<Administrator> GetActiveAdministratorsByEmail(string email);
        List<Administrator> GetActiveAdministratorsOrderedByEmail();
        void Add(User administrator);
        void Set(List<Administrator> administrators);
        void Update(string email, Administrator administrator);
        void Delete(string email);
    }
}
