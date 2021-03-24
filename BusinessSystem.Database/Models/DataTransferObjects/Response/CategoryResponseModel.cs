using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.Database.Models.DataTransferObjects.Response
{
    public class CategoryResponseModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }
        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}