using Core.DomainObjects;
using Core.Messages;
using Core.Validators;
using FluentValidation;

namespace Avonale.Payment.Application.Commands;

public class PaymentCommand : Command 
{
    public decimal Price { get; private set; }

    public Card Card { get; private set; }

    public PaymentCommand(decimal price, Card card)
    {
        Price = price;
        Card = card;
    }

    public override bool IsValid()
    {
        ValidationResult = new PayValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class PayValidation : AbstractValidator<PaymentCommand>
{
    public PayValidation()
    {
        RuleFor(p => p.Price)
            .GreaterThan(100);

        RuleFor(p => p.Card)
            .SetValidator(new CardValidator());
    }
}