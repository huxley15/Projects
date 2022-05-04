using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WoodPlanning.Models
{
    //step 1.1
    public enum Services
    {
        TaxPlanning,
        TaxPrep,
        PortfolioConstruction,
        Combo
    }

    public enum EmploymentStatus
    {
        Employee,
        SelfEmployed,
        Both,
        Retired
    }

    public enum MaritalStatus
    {
        Single,
        Married
    }

    public enum Dependents
    {
        Yes,
        No
    }
    //step 3.6 ish
    public class Client:IdentityUser
    {
        
        //step 1.3
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
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
