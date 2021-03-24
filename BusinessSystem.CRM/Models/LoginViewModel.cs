using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.CRM.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Please enter your login")] 
        public string Login { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password")] 
        public string Password { get; set; }
    }
}