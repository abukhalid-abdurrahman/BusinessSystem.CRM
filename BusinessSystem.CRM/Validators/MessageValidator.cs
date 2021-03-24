using BusinessSystem.Database.Models.BusinessObjects;
using FluentValidation;

namespace BusinessSystem.CRM.Validators
{
    public class MessageValidator : AbstractValidator<MessagingEntity>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Message).NotNull().MaximumLength(3000).MinimumLength(1);
            RuleFor(x => x.RecipientId).NotNull().GreaterThan(0);
            RuleFor(x => x.SenderId).NotNull().GreaterThan(0);
        }
    }
}