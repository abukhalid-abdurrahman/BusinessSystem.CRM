using BusinessSystem.Database.Models.DataTransferObjects.Request;
using FluentValidation;

namespace BusinessSystem.CRM.Validators
{
    public class RemoveUserValidator : AbstractValidator<RemovePartnerRequestModel>
    {
        public RemoveUserValidator()
        {
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}