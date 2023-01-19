using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SR50_2021_POP2022
{
    class Config
    {
        public static readonly string usersFilePath = @"../../Resources/users.bin";
        public static readonly string professorsFilePath = @"../../Resources/professors.bin";
        public static readonly string studentsFilePath = @"../../Resources/students.bin";
        public static readonly string schoolsFilePath = @"../../Resources/schools.bin";
        public static readonly string lessonsFilePath = @"../../Resources/lessons.bin";
        public static readonly string administratorsFilePath = @"../../Resources/administrators.bin";
        public static readonly string addressesFilePath = @"../../Resources/addresses.bin";
        public static readonly string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kimim\OneDrive\Documents\programiranje\SR50-2021-POP2022\SR50-2021-POP2022\baseData.mdf;Integrated Security=True;Connect Timeout=30";

    }
}
