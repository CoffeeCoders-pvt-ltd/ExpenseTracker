using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}