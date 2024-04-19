using Mechanic.Data.Entity.Users;
using Mechanic.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _userManager.Users.Where(m => m.Id == id)
                .Include(m => m.UserRoles)
                .ThenInclude(m => m.Role)
                .Include(m => m.UserData)
                .FirstOrDefaultAsync();
        }

        public async Task<List<AppUser>> GetAllByRole(string role)
        {
            if (string.IsNullOrEmpty(role)) return new List<AppUser>();

            return await _userManager.Users.Include(u => u.UserRoles)
                 .ThenInclude(ur => ur.Role)
                 .Include(u => u.UserData)
                 .Where(u => u.UserRoles.Any(u => u.Role.NormalizedName == role.ToUpper()))
                 .ToListAsync();
        }
    }
}
