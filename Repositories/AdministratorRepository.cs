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
    class AdministratorRepository : IAdministratorRepository, IFilePersistence
    {
        private static List<Administrator> administrators = new List<Administrator>();

        public void Add(Administrator administrator)
        {
            administrators.Add(administrator);
            Save();
        }

        public void Add(List<Administrator> newAdministrators)
        {
            administrators.AddRange(newAdministrators);
            Save();
        }

        public void Set(List<Administrator> newAdministrators)
        {
            administrators = newAdministrators;
        }

        public void Delete(string email)
        {
            Administrator administrator = GetById(email);

            if (administrator != null)
            {
                administrator.User.IsActive = false;
            }

            Save();
        }

        public List<Administrator> GetAll()
        {
            return administrators;
        }

        public Administrator GetById(string email)
        {
            return administrators.Find(u => u.User.Email == email);
        }

        public void Update(string email, Administrator updatedAdministrator)
        {
            Save();
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.administratorsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, administrators);
            }
        }
    }
}

