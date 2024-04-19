using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Entity.Users
{
    public class UserData
    {
        [Key]
        public string FIN { get; set; }
        public string Patronymic { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Region { get; set; }
        public byte[] Photo { get; set; }

        public string BloodType { get; set; }
        public string Eyecolor { get; set; }
        public string SosialStatus { get; set; }
        public string Policedept { get; set; }
        public string Series { get; set; }
        public string Seria { get; set; }
        public string IssueDate { get; set; }


  

    }
}
