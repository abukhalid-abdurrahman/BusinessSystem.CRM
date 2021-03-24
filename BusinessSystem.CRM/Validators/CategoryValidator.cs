using BusinessSystem.Database.Models.DataTransferObjects.Request;
using FluentValidation;

namespace BusinessSystem.CRM.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryRequestModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).MaximumLength(15);
            RuleFor(x => x.CategoryName).NotEmpty();

            RuleFor(x => x.PartnerId).GreaterThan(0).NotNull();
            RuleFor(x => x.PartnerId).GreaterThan(0);
        }
    }
}