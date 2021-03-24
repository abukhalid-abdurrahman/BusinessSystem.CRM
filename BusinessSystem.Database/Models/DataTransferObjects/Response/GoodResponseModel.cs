using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.Database.Models.DataTransferObjects.Response
{
    public class GoodResponseModel
    {
        public int GoodId { get; set; }

        [Display(Name = "Good Description")]
        [Required(ErrorMessage = "Good description is required")]
        [DataType(DataType.MultilineText)]
        public string GoodDescription { get; set; }

        [Display(Name = "Good Name")]
        [Required(ErrorMessage = "Good Name is required")]
        [DataType(DataType.Text)]
        public string GoodName { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "Good status is required")]
        public bool Active { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Good image is required")]
        [DataType(DataType.ImageUrl)]
        public string GoodImageUrl { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category is required")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }
        public int PartnerId { get; set; }

        [Display(Name = "Good Owner")]
        [Required(ErrorMessage = "Owner is required")]
        [DataType(DataType.Text)]
        public string PartnerName { get; set; }
        
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}