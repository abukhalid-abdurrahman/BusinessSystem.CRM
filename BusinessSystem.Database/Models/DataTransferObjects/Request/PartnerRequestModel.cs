using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BusinessSystem.Database.Models.DataTransferObjects.Request
{
    public class PartnerRequestModel
    {
        [Display(Name = "Partner Identification Number")]
        [Required(ErrorMessage = "Partner Partner Identification Number is required")]
        public int PartnerId { get; set; }
        
        [Display(Name = "Partner Description")]
        [Required(ErrorMessage = "Partner description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Partner Login")]
        [Required(ErrorMessage = "Partner login is required")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Display(Name = "Partner Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Partner password is required")]
        public string Password { get; set; }

        [Display(Name = "Partner Username")]
        [Required(ErrorMessage = "Partner username(full Name) is required")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        
        [Display(Name = "Partner Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Partner Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Display(Name = "Partner Image")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Partner image is required")]
        public IFormFile PartnerImage { get; set; }
    }
}
