using Microsoft.AspNetCore.Mvc;
//step 9.6
using Microsoft.AspNetCore.Identity;
using WoodPlanning.Models;
using System.Threading.Tasks;
//step 18.6
using WoodPlanning.ViewModels;

namespace WoodPlanning.Controllers
{
    public class AccountController : Controller
    {
        //step 10.6
        private SignInManager<Client> _signinManager;
        private UserManager<Client> _userManager;
        public AccountController(SignInManager<Client> signInManager, UserManager<Client> userManager)
        {
            _signinManager = signInManager;
            _userManager = userManager;
        }

        //step 11.6
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        //step 19.6
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(loginView.UserName, loginView.Password, loginView.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Failed to login");
            return View();
        }

        //step 20.6
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                Client newclient = new Client()
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    UserName = registerViewModel.UserName,
                    PhoneNumber = registerViewModel.PhoneNumber.ToString(),
                    Email = registerViewModel.Email,
                    StreetAddress = registerViewModel.StreetAddress,
                    City = registerViewModel.City,
                    State = registerViewModel.State,
                    PostalCode = registerViewModel.PostalCode,
                    EmploymentStatus = registerViewModel.EmploymentStatus,
                    MaritalStatus = registerViewModel.MaritalStatus,
                    Dependents = registerViewModel.Dependents,
                    Services = registerViewModel.Services,
                    Comments = registerViewModel.Comments
                };
                var result = await _userManager.CreateAsync(newclient, registerViewModel.Password);
                if (result.Succeeded)
                {
                    //step 39.6
                    var addedUser = await _userManager.FindByNameAsync(newclient.UserName);
                    if (addedUser.UserName == "Admin")
                    {
                        await _userManager.AddToRoleAsync(addedUser, "Admin");
                        await _userManager.AddToRoleAsync(addedUser, "Client");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(addedUser, "Client");
                    }

                    //step 20.6
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        //step 12.6
        //this method can also be synchronous
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //step 13.6
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
