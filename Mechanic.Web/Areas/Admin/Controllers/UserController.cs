using Mechanic.Data.Data;
using Mechanic.Data.Entity.Contracts;
using Mechanic.Data.Entity.Enums;
using Mechanic.Data.Entity.Users;
using Mechanic.FinCheckService.Service;
using Mechanic.Service.Interfaces;
using Mechanic.Web.Areas.Admin.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Mechanic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ICheckFIN _finService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        public UserController(ICheckFIN finService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,AppDbContext context,IUserService userService)
        {
            _finService = finService;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string role)
        {
            var data = await _userService.GetAllByRole(role);

            List<UserModelVM> users = new List<UserModelVM>();
            foreach (var item in data)
            {
                var usermodel = new UserModelVM
                {
                    FIN = item.UserDataFIN,
                    PhoneNumber = item.PhoneNumber,
                    FullName = item.FullName
                };
                users.Add(usermodel);
            }
            UserIndexVM model = new UserIndexVM()
            {
                Users = users,
                Role = role
            };
            return View(model);
        }

        //public async Task <IActionResult> Index(string role)
        //{
        //    var data = await _userService.GetAllByRole(role);

        //}

        [HttpGet]
        public async Task<IActionResult> GetFin(string fin)
        {
            var data = await _finService.Find(fin);


            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    string username = GenerateUniqueUserName(model.Name, model.Surname);
                    string password = GeneratePassword();

                  
                    var newUser = new AppUser
                    {
                        UserName = username,
                        FullName = model.Name + " " + model.Surname,
                        IsActive = true 
                    };

                   
                    var result = await _userManager.CreateAsync(newUser, password);

                    if (result.Succeeded)
                    {
                      
                        var role = Enum.GetName(typeof(UserRoles), model.SelectedRole);
                        var roleexist = await _roleManager.RoleExistsAsync(role);
                        if (!roleexist)
                        {
                            await _roleManager.CreateAsync(new AppRole() { Name = role });
                        }
                        
                        await _userManager.AddToRoleAsync(newUser, role);
                        //if (await _roleManager.RoleExistsAsync(role))
                        //{
                        //    await _userManager.AddToRoleAsync(newUser, role);
                        //}

                       
                        return Json(new { username = newUser.UserName, password });
                    }
                    else
                    {
                        
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", "User creation failed. Please try again later.");
                }
            }

          
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMechanizator()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMechanizator(CreateMechanisatorVM model)
        {
            //if (string.IsNullOrWhiteSpace(model.FIN) || string.IsNullOrWhiteSpace(model.PhoneNumber) || string.IsNullOrWhiteSpace(model.DriverLicense))
            //{
            //    ModelState.AddModelError(string.Empty, "Bütün xanaları doldurun");
            //    return View();
            //}
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserDataFIN == model.FIN);
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber || u.UserDataFIN == model.FIN || u.DriverLincense == model.DriverLicense);
            if (existingUser is not null)
            {
                if (existingUser.UserDataFIN == model.FIN)
                {
                    ModelState.AddModelError(string.Empty, "Qeyd etdiyiniz FIN artıq mövcüddur");
                }
                if (existingUser.PhoneNumber == model.PhoneNumber)
                {
                    ModelState.AddModelError(string.Empty, "Qeyd etdiyiniz telefon nömrəsi mövcüddur");
                }
                if(existingUser.DriverLincense == model.DriverLicense)
                {
                    ModelState.AddModelError(string.Empty, "Qeyd etdiyiniz sürücülük vəsiqəsi mövcüddur");
                }
                return View();
            }
            var user = new AppUser
            {
                PhoneNumber = model.PhoneNumber,
                DriverLincense = model.DriverLicense,
                CategoryDriverLicence = model.Category,
                IssueDateDriverLicence = model.IssueDate.ToUniversalTime(),
                ExpirationDateDriverLincence = model.ExpirationDate.ToUniversalTime()
            };
            var userdata = await _finService.Find(model.FIN);
            if(userdata == null)
            {
                ModelState.AddModelError(string.Empty, "Qeyd olunan  FIN'e aid melumat tapilmadi.");
                return View();
            }
            user.UserDataFIN = userdata.FIN;
            //user.FullName = userdata.Name + "" + userdata.Surname;
            var englishFirstName = ChangeCharacterToEnglish(userdata.Name);
            var englishLastName = ChangeCharacterToEnglish(userdata.Surname);
            user.FullName = englishFirstName + " " + englishLastName;
            //user.UserName = englishFirstName + englishLastName;
            user.UserName = GenerateUniqueUserName(englishFirstName, englishLastName);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                var httpClientHandler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential("mechanic_int", "nY4vewit")
                };
                var httpClient = new HttpClient(httpClientHandler);
                var url = $"http://192.168.11.6:180/AGLERP/hs/mechanic_int/getContracts/{user.UserDataFIN}";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var contracts = JsonConvert.DeserializeObject<List<Contract>>(content);

                    if (contracts.Count > 0)
                    {
                        var firstContract = contracts[0];
                        var contractEntity = new Contract
                        {
                            RN = firstContract.RN,
                            RB = firstContract.RB,
                            MuqavileNomresi = firstContract.MuqavileNomresi,
                            BorcQalib = firstContract.BorcQalib
                        };
                       _context.Contracts.Add(contractEntity);
                        await _context.SaveChangesAsync();

                        user.ContractId = contractEntity.Id;
                        await _userManager.UpdateAsync(user);
                    }
                }
                var roler = await _roleManager.RoleExistsAsync(UserRoles.mechanizator.ToString());
                if (!roler)
                {
                    await _roleManager.CreateAsync(new AppRole() { Name = UserRoles.mechanizator.ToString() });
                }
                await _userManager.AddToRoleAsync(user, UserRoles.mechanizator.ToString());
                string password = GeneratePassword();
                return Json(new { user.UserName, password });
            }
            return View();
        }

  

        //private string GenerateUserName(string name,string surname)
        //{
        //    string username = name.Substring(0,1) + surname;
        //    return username.ToLower();
        //}

        private string GenerateUniqueUserName(string name, string surname)
        {
            // Kullanıcı adının başlangıcını oluştur
            string username = name.Substring(0, 1) + surname;

            // Kullanıcı adının benzersiz olup olmadığını kontrol etmek için bir takma ad oluştur
            string tempUsername = username.ToLower();

            // Benzersiz bir kullanıcı adı oluşturulana kadar rastgele sayılar ekleyin
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999); // İstediğiniz aralığı ayarlayabilirsiniz

            // Benzersiz kullanıcı adı oluşturulana kadar döngüyü çalıştırın
            while (IsUserNameExists(tempUsername + randomNumber))
            {
                randomNumber = random.Next(1000, 9999);
            }

            // Son benzersiz kullanıcı adını döndürün
            return tempUsername + randomNumber;
        }


        private bool IsUserNameExists(string username)
        {
            return false;
        }


        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder();
            for (int i = 0; i < 8; i++) 
            {
                password.Append(chars[random.Next(chars.Length)]);
            }
            return password.ToString();
        }

        //private string ChangeCharacterToEnglish(string text)
        //{
        //    text = text.ToLower();
        //    text = text.Replace("ç", "c");
        //    text = text.Replace("ş", "s");
        //    text = text.Replace("ğ", "g");
        //    text = text.Replace("ü", "u");
        //    text = text.Replace("ı", "i");
        //    text = text.Replace("ö", "o");
        //    text = text.Replace("ə", "a");
        //    text = text.Replace(" ", ""); // Boşlukları kaldır
        //    return text;
        //}


        private string ChangeCharacterToEnglish(string text)
        {
            text = text.Replace("ç", "c").Replace("Ç", "C")
                       .Replace("ş", "s").Replace("Ş", "S")
                       .Replace("ğ", "g").Replace("Ğ", "G")
                       .Replace("ü", "u").Replace("Ü", "U")
                       .Replace("ı", "i").Replace("İ", "I")
                       .Replace("ö", "o").Replace("Ö", "O")
                       .Replace("ə", "a").Replace("Ə", "A");

            text = Regex.Replace(text, @"[^a-zA-Z0-9]", "");

            return text;
        }

    }
}
