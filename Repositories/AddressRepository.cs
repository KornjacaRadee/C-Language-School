using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SR50_2021_POP2022.Repositories
{
    internal class AddressRepository : IAddressRepository, IFilePersistence
    {
        private static List<Address> addresses = new List<Address>();

        public void Add(Address address)
        {
            addresses.Add(address);
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Addresses (Id, Street, StreetNumber, City, Country, IsActive)
                    output inserted.Id
                    values (@Id, @Street, @StreetNumber, @City, @Country, @IsActive)";

                command.Parameters.Add(new SqlParameter("Id", address.Id));
                command.Parameters.Add(new SqlParameter("Street", address.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", address.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", address.City));
                command.Parameters.Add(new SqlParameter("Country", address.Country));
                command.Parameters.Add(new SqlParameter("IsActive", address.IsActive));

                command.ExecuteScalar();
            }
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
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Addresses set IsActive=0 where Id=@Id";

                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Addresses";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Addresses");

                foreach (DataRow row in ds.Tables["Addresses"].Rows)
                {
                    var address = new Address
                    {

                        Id = row["Id"] as string,
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                        IsActive = (bool)row["IsActive"]
                    };

                    addresses.Add(address);
                }
            }
            return addresses;
        }

        public Address GetById(string id)
        {
            return GetAll().Find(u => u.Id == id);
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
