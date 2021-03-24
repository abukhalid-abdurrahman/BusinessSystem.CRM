using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.Database.Models.DataTransferObjects.Response
{
    public class PartnerResponseModel
    {
        public int PartnerId { get; set; }

        [Display(Name = "Partner Description")]
        [Required(ErrorMessage = "Partner description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Partner Login")]
        [Required(ErrorMessage = "Partner login(phone number) is required")]
        [DataType(DataType.PhoneNumber)]
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
        
        [Display(Name = "Partner Image Url")]
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Partner image is required")]
        public string PartnerImageUrl { get; set; }

        
        [Display(Name = "Partner Status")]
        public bool Active { get; set; }
        
        [Display(Name = "Partner insert date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Partner insert date is required")]
        public DateTime InsertDate { get; set; }
    }
}
