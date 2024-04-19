using CheckFIN;
using Mechanic.Data.Data;
using Mechanic.Data.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.FinCheckService.Service
{
    public class CheckFINService : ICheckFIN
    {
        private readonly ISoapFinService _soapFinService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CheckFINService(ISoapFinService soapFinService, AppDbContext context, UserManager<AppUser> userManager)
        {
            _soapFinService = soapFinService;
            _context = context;
            _userManager = userManager;
        }

        //public async Task<List<AppUser>> CheckInccorectUserFins()
        //{
        //    var list = await _userManager.Users.Include(u => u.UserData)
        //        .Where(u => u.UserRoles.FirstOrDefault().Role.Name != UserRoles.quest.ToString())
        //        .Where(u => u.UserData == null)
        //        .ToListAsync();

        //    return list;
        //}

        public async Task<UserData> Find(string FIN)
        {
            if (FIN.Length != 7) return null;
            FIN = FIN.ToLower();
            var data = await _context.UserFINs.Where(f => f.FIN.ToLower() == FIN).FirstOrDefaultAsync();

            if (data != null) return data;

            var requestData = await _soapFinService.getPersonalInfoByPinNewAsync(FIN, "true");
            if (requestData.pin == null) return null;

            UserData userData = new UserData()
            {
                FIN = FIN,
                Patronymic = requestData.Patronymic,
                Name = requestData.Name,
                Surname = requestData.Surname,
                Address = requestData.Adress?.place,
                Gender = requestData.gender,
                BirthDate = requestData.birthDate,
                Region = requestData.region,
                Photo = requestData.photo,
                IssueDate = requestData.issueDate,
                BloodType = requestData.bloodtype,
                Eyecolor = requestData.eyecolor,
                SosialStatus = requestData.sosialStatus,
                Policedept = requestData.policedept,
                Series = requestData.series,
                Seria = requestData.Seria,
            };

            _context.UserFINs.Add(userData);
            var a = _context.SaveChanges();

            return userData;
        }
    }
  }

