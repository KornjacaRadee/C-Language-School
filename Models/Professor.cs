using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
[Serializable]
    class Professor
    {
        [NonSerialized]
        private User user;

        public User User { get => user; set => user = value; }
        public string UserId { get; set; }

        public School school { get; set; }

        public List<string> languages { get; set; }

        public List<Lesson> lessons { get; set; }

        public override string ToString()
        {
            return $"[Professor] {User.FirstName}, {User.LastName}, {User.Email}";
        }
    }
}
