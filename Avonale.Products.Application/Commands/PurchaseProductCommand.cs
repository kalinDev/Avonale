using Core.DomainObjects;
using Core.Messages;
using Core.Validators;
using FluentValidation;

namespace Avonale.Products.Application.Commands;

public class PurchaseProductCommand : Command
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Card Card { get; private set; }

    public PurchaseProductCommand(Guid productId, int quantity, Card card)
    {
        ProductId = productId;
        Quantity = quantity;
        Card = card;
    }

    public override bool IsValid()
    {
        ValidationResult = new PurchaseProductCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class PurchaseProductCommandValidation : AbstractValidator<PurchaseProductCommand>
{
    public PurchaseProductCommandValidation()
    {
        RuleFor(p => p.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("The id of product is not valid");
        
        RuleFor(p => p.Quantity)
            .GreaterThan(0);

        RuleFor(p => p.Card)
            .SetValidator(new CardValidator());

    }

}