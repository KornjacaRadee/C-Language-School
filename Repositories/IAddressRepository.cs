using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Repositories
{
    interface IAddressRepository
    {
        List<Address> GetAll();
        Address GetById(string id);
        void Add(Address address);
        void Add(List<Address> addresses);
        void Set(List<Address> addresses);
        void Update(string id, Address address);
        void Delete(string id);
    }
}
