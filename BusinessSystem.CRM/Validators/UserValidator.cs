using BusinessSystem.Database.Models.DataTransferObjects.Request;
using FluentValidation;

namespace BusinessSystem.CRM.Validators
{
    public class UserValidator : AbstractValidator<PartnerRequestModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Description).NotEmpty();
            
            RuleFor(x => x.Password).MaximumLength(10);
            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}