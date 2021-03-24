using BusinessSystem.Database.Models.DataTransferObjects.Request;
using FluentValidation;

namespace BusinessSystem.CRM.Validators
{
    public class GoodValidator : AbstractValidator<GoodRequestModel>
    {
        public GoodValidator()
        {
            RuleFor(x => x.GoodImage).NotNull();
            
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotNull();
            
            RuleFor(x => x.PartnerId).GreaterThan(0);
            RuleFor(x => x.PartnerId).NotNull();

            RuleFor(x => x.GoodDescription).MinimumLength(5);
            RuleFor(x => x.GoodDescription).MaximumLength(500);
            RuleFor(x => x.GoodDescription).NotNull();
            
            RuleFor(x => x.GoodName).MinimumLength(3);
            RuleFor(x => x.GoodName).MaximumLength(15);
            RuleFor(x => x.GoodName).NotNull();
        }
    }
}