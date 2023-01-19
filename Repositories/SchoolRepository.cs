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
    class SchoolRepository : ISchoolRepository, IFilePersistence
    {

        private static List<School> schools = new List<School>();

        public void Add(School school)
        {
            schools.Add(school);
            String languages = "";
            foreach (var lang in school.Languages)
            {
                languages +=   lang + ",";
            }
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.School (Id, Name, Address, Languages,IsActive)
                    output inserted.Id
                    values (@Id, @Name, @Address, @Languages,@IsActive)";

                command.Parameters.Add(new SqlParameter("Id", school.Id));
                command.Parameters.Add(new SqlParameter("Name", school.Name));
                command.Parameters.Add(new SqlParameter("Address", school.Address.Id));
                command.Parameters.Add(new SqlParameter("Languages", languages));
                command.Parameters.Add(new SqlParameter("IsActive", school.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Add(List<School> newSchool)
        {
            schools.AddRange(newSchool);
            Save();
        }

        public void Set(List<School> newSchools)
        {
            schools = newSchools;
        }

        public void Delete(string Id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.School set IsActive=0 where Id=@Id";

                command.Parameters.Add(new SqlParameter("Id", Id));
                command.ExecuteNonQuery();
            }
        }

        public List<School> GetAll()
        {
            List<School> schools = new List<School>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.School";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "School");

                foreach (DataRow row in ds.Tables["School"].Rows)
                {
                    Address adresa = Data.Instance.AddressService.GetActiveAddressById(row["Address"] as String);
                    string languageString = row["Languages"] as string;
                    List<string> languagesList = new List<string>(languageString.Split(','));
                    var user = new School
                    {

                        Id = row["Id"] as string,
                        Name = row["Name"] as string,
                        Languages = languagesList,
                        Address = adresa,
                        IsActive = (bool)row["IsActive"]
                    };

                    schools.Add(user);
                }
            }
            return schools;
        }

        public School GetById(string id)
        {
            return GetAll().Find(u => u.Id == id);
        }

        public void Update(string Id, School updatedSchool)
        {
            School school = GetById(Id);

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                String languages = "";
                foreach (var lang in updatedSchool.Languages)
                {
                    languages += lang + ",";
                }

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.School set 
                        Id = @Id,
                        Name = @Name,
                        Address = @Address,
                        Languages = @Languages,
                        IsActive = @IsActive
                        where Id=@Id";

                command.Parameters.Add(new SqlParameter("Id", updatedSchool.Id));
                command.Parameters.Add(new SqlParameter("Name", updatedSchool.Name));
                command.Parameters.Add(new SqlParameter("Address", updatedSchool.Address.Id));
                command.Parameters.Add(new SqlParameter("Languages", languages));
                command.Parameters.Add(new SqlParameter("IsActive", updatedSchool.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, schools);
            }
        }
    }
}
