using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
    [Serializable]
    class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public EGender Gender { get; set; }
        public EUserType UserType { get; set; }
        public Address Address { get; set; }

        public bool IsActive { get; set; }


        public User() { }

        public override string ToString()
        {
            return $"[User] {FirstName} {LastName}, {Email}";
        }
    }
}
