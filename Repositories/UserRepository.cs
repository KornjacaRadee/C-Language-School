using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.CustomExceptions;
using System;

namespace SR50_2021_POP2022.Repositories
{
    class UserRepository : IUserRepository, IFilePersistence
    {
        private static List<User> users;

        public UserRepository()
        {
            users = new List<User>();
        }

        public void Add(User user)
        {
            users.Add(user);
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Users (Email, Password, FirstName, LastName, Jmbg, Gender, UserType,Address,IsActive)
                    output inserted.Email
                    values (@Email, @Password, @FirstName, @LastName, @JMBG, @Gender, @UserType ,@Address, @IsActive)";

                command.Parameters.Add(new SqlParameter("Email", user.Email));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", user.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));
                command.Parameters.Add(new SqlParameter("Address", user.Address.Id));
                command.Parameters.Add(new SqlParameter("IsActive", user.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Add(List<User> newUsers)
        {
            users.AddRange(newUsers);
            Save();
        }

        public void Set(List<User> newUsers)
        {
            users = newUsers;
        }

        public void Delete(string email)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Users set IsActive=0 where Email=@email";

                command.Parameters.Add(new SqlParameter("email", email));
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Users";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");

                foreach (DataRow row in ds.Tables["Users"].Rows)
                {
                    Address adresa = Data.Instance.AddressService.GetActiveAddressById(row["Address"] as String);
                    var user = new User
                    {

                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        Address = adresa,
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"]
                    };

                    users.Add(user);
                }
            }
                return users;
        }
        public User GetById(string email)
        {
            return GetAll().Find(u => u.Email == email);
        }

        public User GetByJMBG(string JMBG)
        {
            return GetAll().Find(u => u.JMBG == JMBG && u.IsActive);
        }



        public void Update(string email, User updatedUser)
        {
            User user = GetById(email);

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Users set 
                        Email = @Email,
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Password = @Password,
                        JMBG = @Jmbg,
                        Gender = @Gender,
                        UserType = @UserType,
                        Address = @Address,
                        IsActive = @IsActive
                        where email=@Email";

                command.Parameters.Add(new SqlParameter("Email", user.Email));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", user.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));
                command.Parameters.Add(new SqlParameter("Address", "user.Address.Id"));
                command.Parameters.Add(new SqlParameter("IsActive", user.IsActive));

                command.ExecuteScalar();
            }
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.usersFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, users);
            }
        }
    }
}
