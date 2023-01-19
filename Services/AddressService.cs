using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Services
{
    class AddressService : IAddressService
    {
        private IAddressRepository addressRepository;

        public AddressService()
        {
            addressRepository = new AddressRepository();
        }

        public Address GetById(string id)
        {
            return addressRepository.GetById(id);
        }

        public List<Address> GetAll()
        {
            return addressRepository.GetAll();
        }

        public List<Address> GetActiveAddresses()
        {
            return addressRepository.GetAll().Where(p => p.IsActive).ToList();
        }

        public Address GetActiveAddressById(string id)
        {
            return addressRepository.GetById(id);
        }
        public List<Address> GetActiveProfessorsOrderedById()
        {
            return addressRepository.GetAll().Where(p => p.IsActive).OrderBy(p => p.Id).ToList();
        }

        public void Add(Address address)
        {
            addressRepository.Add(address);


        }

        public void Set(List<Address> addresses)
        {
            addressRepository.Set(addresses);
        }

        public void Update(string id, Address address)
        {
            addressRepository.Update(id, address);
            
        }

        public void Delete(string id)
        {
            addressRepository.Delete(id);
        }




    }
}

