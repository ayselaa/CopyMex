using Mechanic.Data.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.FinCheckService.Service
{
    public interface ICheckFIN
    {
        Task<UserData> Find(string FIN);
        //Task<List<AppUser>> CheckInccorectUserFins();
    }
}
