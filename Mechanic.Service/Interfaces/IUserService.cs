using Mechanic.Data.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Service.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> GetById(string id);
        Task<List<AppUser>> GetAllByRole(string role);
    }
}
