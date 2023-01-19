using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SR50_2021_POP2022.Services;


namespace SR50_2021_POP2022.Models
{
    sealed class Data
    {
        private static readonly Data instance = new Data();
        public IUserService UserService { get; set; }
        public IProfessorService ProfessorService { get; set; }

        public IStudentService StudentService { get; set; }

        public ISchoolService SchoolService { get; set; }

        public ILessonService LessonService { get; set; }

        public IAdministratorService AdministratorService { get; set; }

        public IAddressService AddressService { get; set; }

        static Data() { }

        private Data()
        {
            UserService = new UserService();
            ProfessorService = new ProfessorService();
            StudentService = new StudentService();
            SchoolService = new SchoolService();
            LessonService = new LessonService();
            AdministratorService = new AdministratorService();
            AddressService = new AddressService();
        }

        public static Data Instance
        {
            get
            {
                return instance;
            }
        }

        public void Initialize()
        {
            Address address = new Address
            {
                City = "Novi Sad",
                Country = "Srbija",
                Street = "ulica1",
                StreetNumber = "22",
                Id = "1",
                IsActive = true
            };

            User user1 = new User()
            {
                FirstName = "Pera",
                LastName = "Peric",
                Email = "perakonj@gmail.com",
                JMBG = "121346",
                Password = "peki",
                Gender = EGender.MUSKO,
                Address = address,
                UserType = EUserType.ADMINISTRATOR,
                IsActive = true
            };

            List<string> jeziki = new List<string>();
            jeziki.Add("NORVESKI");
            jeziki.Add("ENGLESKI");

            School school1 = new School()
            {
                Id = "1",
                Name = "Dobra Skola",
                Address = address,
                Languages = jeziki,
                IsActive = true
            };


            User user2 = new User
            {
                Email = "profesor@gmail.com",
                FirstName = "mdsaika",
                LastName = "Midasdakic",
                JMBG = "1213461",
                Password = "zdasdasika",
                Gender = EGender.ZENSKO,
                UserType = EUserType.PROFESOR,
                IsActive = true,
                Address = address
            };

            User user3 = new User
            {
                Email = "student@gmail.com",
                FirstName = "mika",
                LastName = "Mikic",
                JMBG = "1312312321",
                Password = "zika",
                Gender = EGender.ZENSKO,
                UserType = EUserType.STUDENT,
                IsActive = true,
                Address = address
            };

            DateTime date1 = new DateTime(2016, 12, 31, 5, 10, 20);

            Professor profa1 = new Professor()
            {
                User = user2,
                languages = jeziki,
                school = school1
                
            };
            Student student1 = new Student
            {
                User = user3
            };

            Administrator administrator1 = new Administrator()
            {
                User = user3
            };

            Lesson lesson1 = new Lesson()
            {
                Id = "1",
                Professor = profa1,
                Date = date1,
                Duration = "30",    
                Student = student1,
                IsReserved = true,
                IsActive = true


            };

            



            //AddressService.Add(address);
            //UserService.Add(user1);
            //AdministratorService.Add(user3);
            //ProfessorService.Add(profa1);
            //StudentService.Add(student1);
            //LessonService.Add(lesson1);
            //SchoolService.Add(school1); // NE RADI IZ CREATE SCHOOL IZ NEKOG RAZLOGA??????
        }

        public void LoadData()
        {
            var users = LoadUsers();
            var professors = LoadProfessors();

            foreach (var professor in professors)
            {
                var user = users.Find(u => u.Email == professor.UserId);
                professor.User = user;
            }

            UserService.Set(users);
            ProfessorService.Set(professors);
        }

        private List<User> LoadUsers()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.usersFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<User>)formatter.Deserialize(stream);
            }
        }

        private List<Student> LoadStudents()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.studentsFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<Student>)formatter.Deserialize(stream);
            }
        }

        private List<School> LoadSchools()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<School>)formatter.Deserialize(stream);
            }
        }

        private List<Professor> LoadProfessors()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<Professor>)formatter.Deserialize(stream);
            }
        }

        private List<Lesson> LoadLessons()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.lessonsFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<Lesson>)formatter.Deserialize(stream);
            }
        }

        private List<Address> LoadAddresses()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.addressesFilePath, FileMode.Open, FileAccess.Read))
            {
                return (List<Address>)formatter.Deserialize(stream);
            }
        }
    }
}
