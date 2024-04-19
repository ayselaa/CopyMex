using Mechanic.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Entity.Contracts
{
    public class Contract :BaseEntity
    {
        public string RN { get; set; }
        public string RB { get; set; }
        public string MuqavileNomresi { get; set; }
        public int BorcQalib { get; set; }

        //public string NumberOfContract { get; set; }
        //public string TypeOfContract { get; set; }
        //public DateTime SignInContractYear { get; set; }
        //public DateTime StartContractTime { get; set; }
        //public int CountOfMonth { get; set; }
        //public DateTime EndOfContract { get; set; }
        //public int CountOfDay { get; set; }


    } 
}
