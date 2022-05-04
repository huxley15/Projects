using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WoodPlanning.Models
{
    //step 3.2
    public class ClientContext:IdentityDbContext<Client> //step 7.6
    {
        //step 8.6
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        
        //step 4.2
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Client>().HasData(
        //        new Client()
        //        {
        //            Id = "kyle",
        //            PasswordHash = "Passw0rd",
        //            FirstName = "Kyle",
        //            LastName = "Wood",
        //            Email = "kyle@email.com",
        //            PhoneNumber = "1234567",
        //            StreetAddress = "123 Main Street",
        //            City = "Redmond",
        //            State = "WA",
        //            PostalCode = 98052,
        //            EmploymentStatus = EmploymentStatus.Employee,
        //            MaritalStatus = MaritalStatus.Single,
        //            Dependents = Dependents.No,
        //            Services = Services.Combo
        //        },
        //        new Client()
        //        {
        //            Id = "dalia",
        //            PasswordHash = "Passw0rd",
        //            FirstName = "Dalia",
        //            LastName = "Wood",
        //            Email = "dalia@email.com",
        //            PhoneNumber = "8675309",
        //            StreetAddress = "123 Main Street",
        //            City = "Aptos",
        //            State = "CA",
        //            PostalCode = 95003,
        //            EmploymentStatus = EmploymentStatus.SelfEmployed,
        //            MaritalStatus = MaritalStatus.Married,
        //            Dependents = Dependents.No,
        //            Services = Services.Combo
        //        }
        //        );
        //}
    }
}
