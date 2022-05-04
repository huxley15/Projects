using System.ComponentModel.DataAnnotations;
using WoodPlanning.Models;

namespace WoodPlanning.ViewModels
{
    //step 17.6
    public class RegisterViewModel:LoginViewModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        public EmploymentStatus EmploymentStatus { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        [Required]
        public Dependents Dependents { get; set; }

        [Required]
        public Services Services { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}
