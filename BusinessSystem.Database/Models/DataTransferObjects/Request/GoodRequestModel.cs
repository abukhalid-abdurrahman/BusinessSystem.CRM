using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BusinessSystem.Database.Models.DataTransferObjects.Request
{
    public class GoodRequestModel
    {
        [Display(Name = "Good Identification Number")]
        public int? GoodId { get; set; }

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

        [Display(Name = "Good Image")]
        [Required(ErrorMessage = "Good image is required")]
        [DataType(DataType.Upload)]
        public IFormFile GoodImage { get; set; }

        [Display(Name = "Category Identification Number")]
        [Required(ErrorMessage = "Category Identification Number is required")]
        public int CategoryId { get; set; }

        [Display(Name = "Partner Identification Number")]
        [Required(ErrorMessage = "Partner Partner Identification Number is required")]
        public int PartnerId { get; set; }

        [Display(Name = "Good creation date")]
        [DataType(DataType.Date)]
        public DateTime InsertDate { get; set; }
    }
}