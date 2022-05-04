//step 16.6
using System.ComponentModel.DataAnnotations;

namespace WoodPlanning.ViewModels
{
    //step 15.6
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
