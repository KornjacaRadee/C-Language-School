using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
    [Serializable]
    internal class School
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<string> Languages { get; set; }

        public bool IsActive { get; internal set; }

        public School() { }

        public override string ToString()
        {
            return $"[Skola] {Name}";
        }
    }
}
