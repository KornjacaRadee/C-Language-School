using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Repositories
{
    interface IAdministratorRepository
    {
        List<Administrator> GetAll();
        Administrator GetById(string email);
        void Add(Administrator administrator);
        void Add(List<Administrator> administrator);
        void Set(List<Administrator> administrator);
        void Update(string email, Administrator administrator);
        void Delete(string email);
    }
}
