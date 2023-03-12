using Core.DomainObjects;
using FluentValidation;

namespace Core.Validators;

public class CardValidator  : AbstractValidator<Card>
{
    public CardValidator()
    {
        RuleFor(c => c.Holder)
            .Custom((name, context) =>
            {
                if (string.IsNullOrEmpty(name) || !name.Contains(" "))
                {
                    context.AddFailure("The name should be a valid composed name");
                }
            });

        RuleFor(c => c.Brand)
            .NotEmpty();

        RuleFor(c => c.Number)
            .Must(HaveValidCardNumber)
            .WithMessage("The provided Card Number is not valid");

        RuleFor(c => c.Cvv)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(4);

        RuleFor(c => c.ExpireAt)
            .GreaterThan(DateTime.Now);
    }
    
    protected static bool HaveValidCardNumber(string cardNumber)
        => CardNumber.Validate(cardNumber);

}