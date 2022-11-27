using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
    
    [Serializable]
    class Address
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
 
        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City}, {Country}";
        }
    }
}
