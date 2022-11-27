using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    interface IAddressService
    {
        List<Address> GetAll();
        Address GetById(string id);
        List<Address> GetActiveAddresses();
        List<Address> GetActiveAddressesById(string id);
        List<Address> GetActiveProfessorsOrderedById();
        void Add(Address address);
        void Set(List<Address> addresses);
        void Update(string id, Address address);
        void Delete(string id);
    }
}
