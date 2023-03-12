using Core.DomainObjects;
using FluentValidation;

namespace Core.Validators;

public class CardValidator  : AbstractValidator<Card>
{
    public CardValidator()
    {
        RuleFor(c => c.Holder)
            .Matches(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")
            .WithMessage("The name should be a valid composed name.");

        RuleFor(c => c.Brand)
            .NotEmpty();

        RuleFor(c => c.Cvv)
            .NotEmpty()
            .Length(3);

        RuleFor(c => c.ExpireAt)
            .GreaterThan(DateTime.Now);
    }
}