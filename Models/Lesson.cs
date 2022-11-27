using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022.Models
{
    [Serializable]
    class Lesson
    {
        public string Id { get; set; }
        public Professor Professor { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public Student Student { get; set; }
        public bool IsReserved { get; set; }

        public bool IsActive { get; set; }


        public Lesson() { }

        public override string ToString()
        {
            return $"[Lesson] {Id} {Professor}, {Duration}";
        }
    }
}

