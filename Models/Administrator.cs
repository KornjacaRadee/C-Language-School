using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
    [Serializable]
    class Administrator
    {
        [NonSerialized]
        private User user;

        public User User { get => user; set => user = value; }
        public string UserId { get; set; }

        public override string ToString()
        {
            return $"[Administrator] {User.FirstName}, {User.LastName}, {User.Email}";
        }
    }
}
