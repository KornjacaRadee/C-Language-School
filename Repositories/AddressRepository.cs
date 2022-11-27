using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Repositories
{
    internal class AddressRepository : IAddressRepository, IFilePersistence
    {
        private static List<Address> addresses = new List<Address>();

        public void Add(Address address)
        {
            addresses.Add(address);
            Save();
        }

        public void Add(List<Address> newAddresses)
        {
            addresses.AddRange(newAddresses);
            Save();
        }

        public void Set(List<Address> newAddresses)
        {
            addresses = newAddresses;
        }

        public void Delete(string id)
        {
            Address address = GetById(id);

            if (address != null)
            {
                address.IsActive = false;
            }

            Save();
        }

        public List<Address> GetAll()
        {
            return addresses;
        }

        public Address GetById(string id)
        {
            return addresses.Find(u => u.Id == id);
        }

        public void Update(string id, Address updatedAddress)
        {
            Save();
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.addressesFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, addresses);
            }
        }
    }
}
