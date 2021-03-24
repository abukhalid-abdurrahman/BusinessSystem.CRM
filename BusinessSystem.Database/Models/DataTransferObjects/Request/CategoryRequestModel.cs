using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.Database.Models.DataTransferObjects.Request
{
    public class CategoryRequestModel
    {
        [Display(Name = "Partner Identification Number")]
        [Required(ErrorMessage = "Partner Partner Identification Number is required")]
        public int PartnerId { get; set; }
        
        [Display(Name = "Category Identification Number")]
        public int? CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }
    }
}